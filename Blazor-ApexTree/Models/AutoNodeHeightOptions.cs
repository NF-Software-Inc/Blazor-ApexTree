namespace ApexTree;

/// <summary>
/// Measure each node's content and give it its own height, instead of every node sharing the
/// global <see cref="DataNodeOptions.NodeHeight"/>.
/// </summary>
/// <remarks>
/// Requires ApexTree core 2.0.0 or later. Cartesian directions only: grouped-leaf stacks and
/// <see cref="Direction.Radial"/> keep a uniform height. Measurement needs a browser, so it is
/// skipped during prerendering and the global height is used until the component renders client
/// side. This is also what lets an expanded card grow, so enable it when using
/// <see cref="ApexTreeOptions.CardExpansion"/>.
/// </remarks>
public class AutoNodeHeightOptions
{
	/// <summary>
	/// Enable per-node measurement. Default: <see langword="false"/>.
	/// </summary>
	public bool? Enabled { get; set; }

	/// <summary>
	/// Floor for a measured height, in pixels. Nodes never measure shorter than this.
	/// </summary>
	public int? MinHeight { get; set; }

	/// <summary>
	/// Ceiling for a measured height, in pixels. Content taller than this clips rather than growing
	/// the node.
	/// </summary>
	public int? MaxHeight { get; set; }

	/// <summary>
	/// Extra pixels added to every measured height, for breathing room the template does not
	/// account for itself.
	/// </summary>
	public int? ExtraHeight { get; set; }
}
