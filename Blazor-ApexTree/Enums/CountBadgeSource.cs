namespace ApexTree;

/// <summary>
/// What the always-on count badge counts. Requires ApexTree core 2.0.0 or later.
/// </summary>
public enum CountBadgeSource
{
	/// <summary>
	/// Every descendant beneath the node, at any depth (the default).
	/// </summary>
	Descendants,

	/// <summary>
	/// Direct children only.
	/// </summary>
	Children,

	/// <summary>
	/// A number read from the node's own data, keyed by
	/// <see cref="ApexTreeOptions.CountBadgeDataKey"/>.
	/// </summary>
	Data
}