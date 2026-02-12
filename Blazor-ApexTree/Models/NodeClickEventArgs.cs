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

	/// <summary>
	/// The timestamp when the event occurred.
	/// </summary>
	public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
