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

    /// <summary>
    /// Callback invoked when the set of selected nodes changes. Requires
    /// <see cref="ApexTreeOptions.EnableSelection"/> to be set.
    /// </summary>
    [Parameter]
    public EventCallback<NodeSelectionEventArgs> OnSelectionChange { get; set; }

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
            else if (UnderlyingType == typeof(OrgNodeData))
            {
                // Leave NodeTemplate null on purpose: the core library renders its built-in
                // org-chart card from the OrgNodeData payload (contentKey "data").
            }
            else
                throw new ArgumentException("Must provide a node template when TItem is not string, Image, or OrgNodeData.", nameof(Options.NodeTemplate));
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
    /// Zooms the chart by the specified factor.
    /// </summary>
    /// <param name="factor">The zoom factor. Use values greater than 1 to zoom in (e.g. 1.2) and less than 1 to zoom out (e.g. 0.8).</param>
    public async Task Zoom(double factor)
    {
        await JsRuntime.InvokeVoidAsync("blazorApextree.Zoom", Id, factor);
    }

    /// <summary>
    /// Resets the pan/zoom baseline to the current viewBox. Useful after programmatic viewBox changes
    /// so subsequent pan/zoom interactions start from the new position.
    /// </summary>
    public async Task ResetPanZoomBaseAsync()
    {
        await JsRuntime.InvokeVoidAsync("blazorApextree.ResetPanZoomBase", Id);
    }

    /// <summary>
    /// Downloads the current tree as an SVG file.
    /// </summary>
    public async Task ExportToSvg()
    {
        await JsRuntime.InvokeVoidAsync("blazorApextree.ExportToSvg", Id);
    }

    /// <summary>
    /// Replaces the tree data and re-renders without recreating the ApexTree instance.
    /// </summary>
    /// <param name="data">The new root node to render.</param>
    public async Task Construct(DataNode<TItem> data)
    {
        await JsRuntime.InvokeVoidAsync("blazorApextree.Construct", Id, JsonSerializer.Serialize(data, ChartSerializer.DefaultOptions));
    }

    /// <summary>
    /// Diffs the tree against a new dataset and animates the difference instead of rebuilding:
    /// surviving nodes spring to their new positions, new ids grow in, and departed ones retract.
    /// Collapse state, selection, focus and expanded cards all survive.
    /// </summary>
    /// <remarks>
    /// Prefer this over <see cref="Construct(DataNode{TItem})"/> and <see cref="RebuildChart"/> for
    /// data changes: both of those redraw from scratch. Requires ApexTree core 2.0.0 or later.
    /// </remarks>
    /// <param name="data">The new root node to reconcile to.</param>
    public async Task UpdateData(DataNode<TItem> data)
    {
        await JsRuntime.InvokeVoidAsync("blazorApextree.UpdateData", Id, JsonSerializer.Serialize(data, ChartSerializer.DefaultOptions));
    }

    /// <summary>
    /// Expands every node in the tree. Requires ApexTree core 2.0.0 or later.
    /// </summary>
    public async Task ExpandAll()
    {
        await JsRuntime.InvokeVoidAsync("blazorApextree.ExpandAll", Id);
    }

    /// <summary>
    /// Collapses every node in the tree. Requires ApexTree core 2.0.0 or later.
    /// </summary>
    public async Task CollapseAll()
    {
        await JsRuntime.InvokeVoidAsync("blazorApextree.CollapseAll", Id);
    }

    /// <summary>
    /// Expands the tree down to the given depth and collapses everything deeper.
    /// Requires ApexTree core 2.0.0 or later.
    /// </summary>
    /// <param name="depth">The depth to expand to, with the root at 0.</param>
    public async Task ExpandToDepth(int depth)
    {
        await JsRuntime.InvokeVoidAsync("blazorApextree.ExpandToDepth", Id, depth);
    }

    /// <summary>
    /// Expands a node and everything beneath it. Requires ApexTree core 2.0.0 or later.
    /// </summary>
    /// <param name="id">The HTML id of the node to expand.</param>
    public async Task ExpandSubtree(string id)
    {
        await JsRuntime.InvokeVoidAsync("blazorApextree.ExpandSubtree", Id, id);
    }

    /// <summary>
    /// Collapses a node and everything beneath it. Requires ApexTree core 2.0.0 or later.
    /// </summary>
    /// <param name="id">The HTML id of the node to collapse.</param>
    public async Task CollapseSubtree(string id)
    {
        await JsRuntime.InvokeVoidAsync("blazorApextree.CollapseSubtree", Id, id);
    }

    /// <summary>
    /// Spotlights a node: dims everything outside its lineage and visible subtree, and springs the
    /// camera to frame it. Requires ApexTree core 2.0.0 or later.
    /// </summary>
    /// <remarks>
    /// See <see cref="FocusOptions"/> to let the user do this by clicking, and to tune the dim.
    /// </remarks>
    /// <param name="id">The HTML id of the node to spotlight.</param>
    public async Task Focus(string id)
    {
        await JsRuntime.InvokeVoidAsync("blazorApextree.Focus", Id, id);
    }

    /// <summary>
    /// Clears the spotlight and restores the full view. Requires ApexTree core 2.0.0 or later.
    /// </summary>
    public async Task ClearFocus()
    {
        await JsRuntime.InvokeVoidAsync("blazorApextree.ClearFocus", Id);
    }

    /// <summary>
    /// Gets the spotlighted node id, or <see langword="null"/> when nothing is focused.
    /// Requires ApexTree core 2.0.0 or later.
    /// </summary>
    public async Task<string?> GetFocusedNodeId()
    {
        return await JsRuntime.InvokeAsync<string?>("blazorApextree.GetFocusedNodeId", Id);
    }

    /// <summary>
    /// Flows an animated dash along the edges from the root to each of the given nodes.
    /// Requires ApexTree core 2.0.0 or later.
    /// </summary>
    /// <remarks>
    /// See <see cref="EdgeFlowOptions"/> for colour, speed and dash styling.
    /// </remarks>
    /// <param name="ids">The HTML ids of the target nodes.</param>
    public async Task SetActivePath(IEnumerable<string> ids)
    {
        await JsRuntime.InvokeVoidAsync("blazorApextree.SetActivePath", Id, ids.ToArray());
    }

    /// <summary>
    /// Clears the active path. Requires ApexTree core 2.0.0 or later.
    /// </summary>
    public async Task ClearActivePath()
    {
        await JsRuntime.InvokeVoidAsync("blazorApextree.ClearActivePath", Id);
    }

    /// <summary>
    /// Gets the node ids currently on the active path. Requires ApexTree core 2.0.0 or later.
    /// </summary>
    public async Task<string[]> GetActivePath()
    {
        return await JsRuntime.InvokeAsync<string[]>("blazorApextree.GetActivePath", Id) ?? [];
    }

    /// <summary>
    /// Expands a node's card in place to reveal its detail section. This is separate from expanding
    /// the node's children. Requires ApexTree core 2.0.0 or later.
    /// </summary>
    /// <remarks>
    /// Enable <see cref="ApexTreeOptions.AutoNodeHeight"/> so the card can grow to fit the detail.
    /// </remarks>
    /// <param name="id">The HTML id of the node whose card to expand.</param>
    public async Task ExpandCard(string id)
    {
        await JsRuntime.InvokeVoidAsync("blazorApextree.ExpandCard", Id, id);
    }

    /// <summary>
    /// Collapses a node's card back to its summary. Requires ApexTree core 2.0.0 or later.
    /// </summary>
    /// <param name="id">The HTML id of the node whose card to collapse.</param>
    public async Task CollapseCard(string id)
    {
        await JsRuntime.InvokeVoidAsync("blazorApextree.CollapseCard", Id, id);
    }

    /// <summary>
    /// Toggles a node's card between summary and detail. Requires ApexTree core 2.0.0 or later.
    /// </summary>
    /// <param name="id">The HTML id of the node whose card to toggle.</param>
    public async Task ToggleCard(string id)
    {
        await JsRuntime.InvokeVoidAsync("blazorApextree.ToggleCard", Id, id);
    }

    /// <summary>
    /// Replaces the set of expanded cards in one pass. Requires ApexTree core 2.0.0 or later.
    /// </summary>
    /// <param name="ids">The HTML ids of the nodes whose cards should be expanded.</param>
    public async Task SetExpandedCards(IEnumerable<string> ids)
    {
        await JsRuntime.InvokeVoidAsync("blazorApextree.SetExpandedCards", Id, ids.ToArray());
    }

    /// <summary>
    /// Gets the ids of the nodes whose cards are expanded. Requires ApexTree core 2.0.0 or later.
    /// </summary>
    public async Task<string[]> GetExpandedCards()
    {
        return await JsRuntime.InvokeAsync<string[]>("blazorApextree.GetExpandedCards", Id) ?? [];
    }

    /// <summary>
    /// Centres the camera on a node, keeping the current zoom level.
    /// Requires ApexTree core 2.0.0 or later.
    /// </summary>
    /// <param name="id">The HTML id of the node to centre on.</param>
    public async Task CenterOnNode(string id)
    {
        await JsRuntime.InvokeVoidAsync("blazorApextree.CenterOnNode", Id, id);
    }

    /// <summary>
    /// Destroys the chart and recreates it. Useful when the dataset or options have changed.
    /// </summary>
    /// <remarks>
    /// For a data-only change prefer <see cref="UpdateData(DataNode{TItem})"/>, which animates the
    /// difference instead of redrawing from scratch.
    /// </remarks>
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
