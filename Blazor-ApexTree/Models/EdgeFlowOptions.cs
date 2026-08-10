namespace ApexTree;

/// <summary>
/// Styling for the animated "active path": a marching dash that flows along the edges from the root
/// to one or more nodes.
/// </summary>
/// <remarks>
/// Requires ApexTree core 2.0.0 or later. Set the path with
/// <see cref="ApexTree{TItem}.SetActivePath(IEnumerable{string})"/> and clear it with
/// <see cref="ApexTree{TItem}.ClearActivePath"/>. The animation is skipped (edges are highlighted
/// but static) when the user has asked for reduced motion.
/// </remarks>
public class EdgeFlowOptions
{
	/// <summary>
	/// Stroke color of the flowing edges. Default: a soft indigo (<c>#5C6BC0</c>).
	/// </summary>
	public string? Color { get; set; }

	/// <summary>
	/// Stroke width of the flowing edges, in pixels. Default: <c>2</c>.
	/// </summary>
	public int? Width { get; set; }

	/// <summary>
	/// Dash travel speed in pixels per second. Default: <c>60</c>.
	/// </summary>
	public int? Speed { get; set; }

	/// <summary>
	/// Length of each dash, in pixels. Default: <c>8</c>.
	/// </summary>
	public int? DashLength { get; set; }

	/// <summary>
	/// Gap between dashes, in pixels. Default: <c>6</c>.
	/// </summary>
	public int? GapLength { get; set; }

	/// <summary>
	/// Which way the dashes travel. Default: <see cref="EdgeFlowDirection.ToChild"/>.
	/// </summary>
	public EdgeFlowDirection? Direction { get; set; }

	/// <summary>
	/// Automatically flow the path to whichever node is focused, so
	/// <see cref="ApexTree{TItem}.Focus(string)"/> also lights its lineage. Default:
	/// <see langword="false"/>.
	/// </summary>
	public bool? FollowFocus { get; set; }
}
