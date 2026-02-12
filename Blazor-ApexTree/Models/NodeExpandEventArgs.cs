namespace ApexTree;

/// <summary>
/// Provides data for the node expand event.
/// </summary>
public class NodeExpandEventArgs
{
	/// <summary>
	/// The ID of the expanded node, matching <see cref="DataNode{TItem}.Id"/>.
	/// </summary>
	public string? NodeId { get; set; }

	/// <summary>
	/// The timestamp when the event occurred.
	/// </summary>
	public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
