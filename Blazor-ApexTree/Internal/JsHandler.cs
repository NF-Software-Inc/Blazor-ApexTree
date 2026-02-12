using Microsoft.JSInterop;

namespace ApexTree.Internal;

/// <summary>
/// Provides internal-only <see cref="JSInvokableAttribute"/> callbacks
/// </summary>
/// <typeparam name="TItem"></typeparam>
internal sealed class JsHandler<TItem> : IDisposable
{
	private readonly ApexTree<TItem> ChartReference;
	internal DotNetObjectReference<JsHandler<TItem>> ObjectReference;

	internal JsHandler(ApexTree<TItem> chartReference)
	{
		ObjectReference = DotNetObjectReference.Create(this);
		ChartReference = chartReference;
	}

	/// <inheritdoc/>
	public void Dispose()
	{
		ObjectReference.Dispose();
	}

	internal void UpdateObjectReference()
	{
		ObjectReference.Dispose();
		ObjectReference = DotNetObjectReference.Create(this);
	}

	/// <summary>
	/// Called from JavaScript when a tree node is clicked.
	/// </summary>
	/// <param name="nodeId">The ID of the clicked node.</param>
	[JSInvokable]
	public async Task OnNodeClicked(string nodeId)
	{
		if (ChartReference.OnNodeClick.HasDelegate)
		{
			await ChartReference.OnNodeClick.InvokeAsync(new NodeClickEventArgs
			{
				NodeId = nodeId,
				Timestamp = DateTime.UtcNow
			});
		}
	}

	/// <summary>
	/// Called from JavaScript when a tree node is hovered.
	/// </summary>
	/// <param name="nodeId">The ID of the hovered node.</param>
	[JSInvokable]
	public async Task OnNodeHovered(string nodeId)
	{
		if (ChartReference.OnNodeHover.HasDelegate)
		{
			await ChartReference.OnNodeHover.InvokeAsync(new NodeHoverEventArgs
			{
				NodeId = nodeId,
				Timestamp = DateTime.UtcNow
			});
		}
	}

	/// <summary>
	/// Called from JavaScript when a tree node is expanded.
	/// </summary>
	/// <param name="nodeId">The ID of the expanded node.</param>
	[JSInvokable]
	public async Task OnNodeExpanded(string nodeId)
	{
		if (ChartReference.OnNodeExpand.HasDelegate)
		{
			await ChartReference.OnNodeExpand.InvokeAsync(new NodeExpandEventArgs
			{
				NodeId = nodeId,
				Timestamp = DateTime.UtcNow
			});
		}
	}

	/// <summary>
	/// Called from JavaScript when a tree node is collapsed.
	/// </summary>
	/// <param name="nodeId">The ID of the collapsed node.</param>
	[JSInvokable]
	public async Task OnNodeCollapsed(string nodeId)
	{
		if (ChartReference.OnNodeCollapse.HasDelegate)
		{
			await ChartReference.OnNodeCollapse.InvokeAsync(new NodeCollapseEventArgs
			{
				NodeId = nodeId,
				Timestamp = DateTime.UtcNow
			});
		}
	}
}
