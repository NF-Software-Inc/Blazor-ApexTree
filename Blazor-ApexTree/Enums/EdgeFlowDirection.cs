namespace ApexTree;

/// <summary>
/// Which way the dashes travel along an active-path edge. Requires ApexTree core 2.0.0 or later.
/// </summary>
public enum EdgeFlowDirection
{
	/// <summary>
	/// Root toward leaf (the default).
	/// </summary>
	ToChild,

	/// <summary>
	/// Leaf toward root.
	/// </summary>
	ToParent
}