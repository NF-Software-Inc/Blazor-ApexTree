namespace ApexTree;

/// <summary>
/// Semantic zoom (level of detail): swap what each node renders based on how large it appears on
/// screen, so a zoomed-out tree stays readable instead of turning into unreadable full cards.
/// </summary>
/// <remarks>
/// Requires ApexTree core 2.0.0 or later. Node geometry is fixed across tiers: the layout is
/// computed once and crossing a threshold only changes what is drawn inside each node's box, so
/// nothing moves and the camera is untouched. Thresholds are measured in on-screen pixels of node
/// width, not zoom percentages, so they behave the same at any container size.
/// </remarks>
public class SemanticZoomOptions
{
	/// <summary>
	/// Enable level-of-detail switching. Default: <see langword="false"/>.
	/// </summary>
	public bool? Enabled { get; set; }

	/// <summary>
	/// Below this apparent node width in pixels, nodes drop from the full card to a compact
	/// name-and-role plate. Default: <c>90</c>.
	/// </summary>
	public int? CompactBelow { get; set; }

	/// <summary>
	/// Below this apparent node width in pixels, nodes drop again to a colored slab carrying just
	/// the name. Default: <c>42</c>.
	/// </summary>
	public int? DotBelow { get; set; }
}
