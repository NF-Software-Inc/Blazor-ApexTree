namespace ApexTree;

/// <summary>
/// Horizontal placement of an external node label relative to the node.
/// </summary>
public enum ExternalLabelAlign
{
	/// <summary>
	/// Label is centered on the node horizontally (default).
	/// </summary>
	Center,

	/// <summary>
	/// Label sits to the left of the node (text is right-anchored).
	/// </summary>
	Left,

	/// <summary>
	/// Label sits to the right of the node (text is left-anchored).
	/// </summary>
	Right
}
