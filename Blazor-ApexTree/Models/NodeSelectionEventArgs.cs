namespace ApexTree;

/// <summary>
/// Event data raised when the set of selected nodes changes (requires
/// <see cref="ApexTreeOptions.EnableSelection"/> to be set).
/// </summary>
public class NodeSelectionEventArgs
{
	/// <summary>
	/// The ids of the currently selected nodes.
	/// </summary>
	public IEnumerable<string> SelectedIds { get; set; } = Array.Empty<string>();

	/// <summary>
	/// The time the selection change occurred (UTC).
	/// </summary>
	public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
