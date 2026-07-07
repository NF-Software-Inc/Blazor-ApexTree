namespace ApexTree;

/// <summary>
/// Controls how edge (connector) colors are determined.
/// </summary>
public enum EdgeColorMode
{
	/// <summary>
	/// All edges use the global edge color.
	/// </summary>
	Default,

	/// <summary>
	/// Each edge inherits the border color of the child node it connects into.
	/// </summary>
	Node
}
