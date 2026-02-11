namespace ApexTree;

/// <summary>
/// Provides data for the node collapse event.
/// </summary>
public class NodeCollapseEventArgs
{
	/// <summary>
	/// The ID of the collapsed node, matching <see cref="DataNode{TItem}.Id"/>.
	/// </summary>
	public string? NodeId { get; set; }

	/// <summary>
	/// The timestamp when the event occurred.
	/// </summary>
	public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
