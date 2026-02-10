namespace ApexTree;

/// <summary>
/// Provides data for the node click event.
/// </summary>
public class NodeClickEventArgs
{
	/// <summary>
	/// The ID of the clicked node, matching <see cref="DataNode{TItem}.Id"/>.
	/// </summary>
	public string? NodeId { get; set; }
}
