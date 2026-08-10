namespace ApexTree;

/// <summary>
/// The direction the tree grows from the root.
/// </summary>
public enum Direction
{
	/// <summary>
	/// Root at the top with children below it (the default).
	/// </summary>
	Top,

	/// <summary>
	/// Root at the bottom with children above it.
	/// </summary>
	Bottom,

	/// <summary>
	/// Root on the left with children to its right.
	/// </summary>
	Left,

	/// <summary>
	/// Root on the right with children to its left.
	/// </summary>
	Right,

	/// <summary>
	/// Root at the centre with each depth on a ring radiating outward. Combine with
	/// <see cref="ApexTree.LayoutType.Cluster"/> for a dendrogram that pins every leaf to the
	/// outer ring. Requires ApexTree core 2.0.0 or later.
	/// </summary>
	Radial
}
