using ApexTree.Internal;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.Json;

namespace ApexTree;

/// <summary>
/// Main component to create an Apex tree in Blazor.
/// </summary>
/// <typeparam name="TItem">The data type of the items to display in the tree.</typeparam>
public partial class ApexTree<TItem> : ComponentBase, IAsyncDisposable
{
    /// <summary>
    /// The main node to display in the chart. Add all child items to this node.
    /// </summary>
    [EditorRequired]
    [Parameter]
    public DataNode<TItem> Parent { get; set; }

    /// <summary>
    /// The options to customize the chart with.
    /// </summary>
    /// <remarks>
    /// Each instance of this component must have its own options object.
    /// </remarks>
    [Parameter]
	public ApexTreeOptions Options { get; set; } = new ApexTreeOptions();

    /// <summary>
    /// Callback invoked when a node in the tree is clicked.
    /// </summary>
    [Parameter]
    public EventCallback<NodeClickEventArgs> OnNodeClick { get; set; }

    /// <summary>
    /// Callback invoked when a node in the tree is hovered.
    /// </summary>
    [Parameter]
    public EventCallback<NodeHoverEventArgs> OnNodeHover { get; set; }

    /// <summary>
    /// Callback invoked when a node in the tree is expanded.
    /// </summary>
    [Parameter]
    public EventCallback<NodeExpandEventArgs> OnNodeExpand { get; set; }

    /// <summary>
    /// Callback invoked when a node in the tree is collapsed.
    /// </summary>
    [Parameter]
    public EventCallback<NodeCollapseEventArgs> OnNodeCollapse { get; set; }

	[Inject]
	private IJSRuntime JsRuntime { get; init; } = default!;

    private readonly string Id = Guid.NewGuid().ToHtmlId().ToString("N");
    private static bool IsLibraryLoaded;
    private static bool IsLicenseSet;
    private bool IsChartLoaded;
    private bool ParentSet;

    private Type UnderlyingType = typeof(string);
    private ElementReference ChartContainer;
	private JsHandler<TItem>? Handler;

	/// <inheritdoc/>
	protected override void OnInitialized()
    {
        UnderlyingType = Nullable.GetUnderlyingType(typeof(TItem)) ?? typeof(TItem);
    }

    /// <inheritdoc/>
    protected async override Task OnAfterRenderAsync(bool firstRender)
    {
		// Load JavaScript library on first render
		if (firstRender && IsLibraryLoaded == false)
        {
            _ = await JsLoader.LoadAsync(JsRuntime);
            IsLibraryLoaded = true;
		}

		// Set license if configured and not already set
		if (IsLibraryLoaded && ApexTreeLicense.HasLicense && !IsLicenseSet)
		{
			try
			{
				await JsRuntime.InvokeVoidAsync("blazorApextree.SetLicense", ApexTreeLicense.LicenseKey);
				IsLicenseSet = true;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error setting ApexTree license: {ex.Message}");
			}
		}

		// Create chart when library is loaded and parent node is set
		if (IsLibraryLoaded && IsChartLoaded == false && ParentSet)
        {
            ParentSet = false;
            IsChartLoaded = true;
			Handler = new JsHandler<TItem>(this);

			await JsRuntime.InvokeVoidAsync("blazorApextree.CreateChart", ChartContainer, Id, JsonSerializer.Serialize(Options, ChartSerializer.DefaultOptions), Parent, Handler.ObjectReference);
        }
    }

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        if (Options.NodeTemplate == null)
        {
            if (UnderlyingType == typeof(string))
                Options.NodeTemplate = "(content) => { return `<div style='display: flex; justify-content: center; align-items: center; text-align: center; height: 100%;'>${content}</div>`; }";
            else if (UnderlyingType == typeof(Image))
                Options.NodeTemplate = "(content) => { return `<div style='display: flex; flex-direction: column; justify-content: center; align-items: center; height: 100%;'><img style='width: 50px; height: 50px; border-radius: 50%;' src='${content.url}' /><div>${content.name}</div></div>`; }";
            else
                throw new ArgumentException("Must provide a node template when TItem is not string or Image.", nameof(Options.NodeTemplate));
        }
    }

    /// <inheritdoc/>
    public async override Task SetParametersAsync(ParameterView parameters)
    {
        if (parameters.TryGetValue(nameof(Parent), out DataNode<TItem>? parent) && parent != null)
            ParentSet = true;

        await base.SetParametersAsync(parameters);
    }

    /// <summary>
    /// Collapses the specified node.
    /// </summary>
    /// <param name="id">The HTML id of the node to collapse.</param>
    public async Task CollapseNode(string id)
	{
		await JsRuntime.InvokeVoidAsync("blazorApextree.CollapseNode", Id, id);
	}

    /// <summary>
    /// Expands the specified node.
    /// </summary>
    /// <param name="id">he HTML id of the node to expand.</param>
    public async Task ExpandNode(string id)
	{
        await JsRuntime.InvokeVoidAsync("blazorApextree.ExpandNode", Id, id);
    }

    /// <summary>
    /// Changes the layout of the chart.
    /// </summary>
    /// <param name="direction">The updated direction of the layout to apply.</param>
    public async Task ChangeLayout(Direction direction)
	{
        await JsRuntime.InvokeVoidAsync("blazorApextree.ChangeLayout", Id, JsonSerializer.Serialize(direction, ChartSerializer.DefaultOptions));
    }

    /// <summary>
    /// Updates the chart to fit to the current viewport.
    /// </summary>
    public async Task FitScreen()
	{
        await JsRuntime.InvokeVoidAsync("blazorApextree.FitScreen", Id);
    }

    /// <summary>
    /// Rerenders the chart.
    /// </summary>
    /// <param name="keepOldPosition">Undocumented.</param>
    public async Task Render(bool keepOldPosition = false)
	{
        await JsRuntime.InvokeVoidAsync("blazorApextree.Render", Id, keepOldPosition);
    }

    /// <summary>
    /// Destroys the chart and recreates it. Useful when the dataset or options have changed.
    /// </summary>
    public async Task RebuildChart()
    {
        await JsRuntime.InvokeVoidAsync("blazorApextree.DeleteChart", Id);

		Handler?.UpdateObjectReference();

        await JsRuntime.InvokeVoidAsync("blazorApextree.CreateChart", ChartContainer, Id, JsonSerializer.Serialize(Options, ChartSerializer.DefaultOptions), JsonSerializer.Serialize(Parent, ChartSerializer.DefaultOptions), Handler?.ObjectReference);
    }

    /// <summary>
    /// Gets the width CSS value from Options.
    /// </summary>
    private string GetWidth()
    {
        return ChartSerializer.GetMeasurement(Options.Width, Options.WidthUnits, true) ?? "400px";
    }

    /// <summary>
    /// Gets the height CSS value from Options.
    /// </summary>
    private string GetHeight()
    {
        return ChartSerializer.GetMeasurement(Options.Height, Options.HeightUnits, false) ?? "400px";
    }

    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        await JsRuntime.InvokeVoidAsync("blazorApextree.DeleteChart", Id);
		Handler?.Dispose();
    }
}
