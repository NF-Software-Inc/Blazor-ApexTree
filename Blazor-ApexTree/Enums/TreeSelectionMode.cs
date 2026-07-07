namespace ApexTree;

/// <summary>
/// Node selection behaviour. Leave <see cref="ApexTreeOptions.EnableSelection"/> unset (null) to
/// disable selection entirely.
/// </summary>
public enum TreeSelectionMode
{
	/// <summary>
	/// Only one node can be selected at a time.
	/// </summary>
	Single,

	/// <summary>
	/// Multiple nodes can be selected (Ctrl/Cmd-click).
	/// </summary>
	Multi
}
