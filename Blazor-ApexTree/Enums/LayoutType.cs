namespace ApexTree;

/// <summary>
/// How the layout places leaf nodes. Requires ApexTree core 2.0.0 or later.
/// </summary>
public enum LayoutType
{
	/// <summary>
	/// Tidy tree: each node sits one rank below its parent (the default).
	/// </summary>
	Tree,

	/// <summary>
	/// Dendrogram: every leaf is pinned to the outermost rank, so all leaves line up.
	/// Most striking combined with <see cref="Direction.Radial"/>.
	/// </summary>
	Cluster
}