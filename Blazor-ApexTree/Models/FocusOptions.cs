namespace ApexTree;

/// <summary>
/// Focus ("spotlight") mode: dim everything outside a node's lineage and visible subtree, and
/// frame that subtree.
/// </summary>
/// <remarks>
/// Requires ApexTree core 2.0.0 or later. Drive it from C# with
/// <see cref="ApexTree{TItem}.Focus(string)"/> and <see cref="ApexTree{TItem}.ClearFocus"/>, or let
/// the user click by setting <see cref="ClickToFocus"/>. Escape also clears it.
/// </remarks>
public class FocusOptions
{
	/// <summary>
	/// Spotlight a node when it is clicked. Default: <see langword="false"/>, so focus is
	/// programmatic only.
	/// </summary>
	public bool? ClickToFocus { get; set; }

	/// <summary>
	/// How strongly to dim everything outside the focused lineage, from 0 (invisible) to 1 (no
	/// dimming). Default: <c>0.7</c>.
	/// </summary>
	public double? DimOpacity { get; set; }
}
