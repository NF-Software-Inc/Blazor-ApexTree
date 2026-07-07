namespace ApexTree;

/// <summary>
/// Shape of the connecting lines drawn between parent and child nodes.
/// </summary>
public enum EdgeStyle
{
	/// <summary>
	/// Smooth cubic Bezier curve from parent to child.
	/// </summary>
	Curved,

	/// <summary>
	/// Right-angle elbows with rounded corners (default; the traditional org-chart look).
	/// </summary>
	Orthogonal,

	/// <summary>
	/// A direct line from the parent anchor to the child anchor.
	/// </summary>
	Straight
}
