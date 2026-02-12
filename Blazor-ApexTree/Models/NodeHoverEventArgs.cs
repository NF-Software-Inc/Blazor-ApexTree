namespace ApexTree;

/// <summary>
/// Provides data for the node hover event.
/// </summary>
public class NodeHoverEventArgs
{
	/// <summary>
	/// The ID of the hovered node, matching <see cref="DataNode{TItem}.Id"/>.
	/// </summary>
	public string? NodeId { get; set; }

	/// <summary>
	/// The timestamp when the event occurred.
	/// </summary>
	public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
