namespace ApexTree;

/// <summary>
/// Expandable cards: a node's card can expand in place to reveal a detail section. This is separate
/// from expanding a node's children.
/// </summary>
/// <remarks>
/// Requires ApexTree core 2.0.0 or later. The built-in org card grows a chevron whenever its
/// <see cref="OrgNodeData"/> carries any detail field (<see cref="OrgNodeData.Stats"/>,
/// <see cref="OrgNodeData.Progress"/>, <see cref="OrgNodeData.Actions"/> or
/// <see cref="OrgNodeData.Details"/>). Drive it from C# with
/// <see cref="ApexTree{TItem}.ToggleCard(string)"/> and friends.
///
/// Pair this with <see cref="ApexTreeOptions.AutoNodeHeight"/>: without measured heights the card
/// still toggles but the revealed detail may clip, since the node's box cannot grow.
/// </remarks>
public class CardExpansionOptions
{
	/// <summary>
	/// Toggle a card when its body is clicked, not just its chevron. Default:
	/// <see langword="false"/>. The chevron and the C# methods work either way.
	/// </summary>
	public bool? ClickToExpand { get; set; }
}
