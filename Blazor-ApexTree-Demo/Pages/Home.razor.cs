using Microsoft.AspNetCore.Components;

namespace Blazor_ApexTree_Demo.Pages;

public partial class Home : ComponentBase
{
	/// <param name="IsNew">Marks a demo of a feature that arrived in 11.0.0, so the badge can flag it.</param>
	private record ExampleInfo(string Title, string Description, string Icon, string Url, bool IsNew = false);

	/// <summary>
	/// Every routed demo page, grouped by what it teaches. Titles match the sidebar labels so a card
	/// and its nav entry name the same thing. Keep this in sync with NavMenu.razor and the smoke
	/// test's route list when adding a page: the grid is the only way in from the landing page.
	/// </summary>
	private readonly (string Heading, ExampleInfo[] Items)[] Groups =
	[
		("Getting started",
		[
			new("Basic", "A string-based organizational chart with default settings.", "account_tree", "basic"),
			new("Custom Template", "Custom node templates with different font families and image avatars.", "text_fields", "custom-template"),
			new("Styled Nodes", "Per-node styling with different colors and borders for each department.", "palette", "styled-nodes"),
		]),
		("Layout",
		[
			new("Dynamic View", "Switch the layout direction dynamically between all four orientations.", "swap_horiz", "dynamic-view"),
			new("Radial & Dendrogram", "Lay the tree out as a radial fan, or as a dendrogram with every leaf aligned at the same depth.", "hub", "radial", IsNew: true),
		]),
		("Node content",
		[
			new("Org Card", "The built-in org card: name, title, avatar, tags and a department accent color.", "contact_page", "org-card"),
			new("Expandable Cards", "Expand a card in place to reveal stats, a progress meter and actions, separately from expanding its children.", "open_in_full", "expandable-cards", IsNew: true),
			new("External Labels", "Labels placed outside the node box, with a collision strategy for when they overlap.", "label", "external-labels"),
			new("Tooltip", "Tooltips with custom HTML templates and styling options.", "chat_bubble_outline", "custom-tooltip"),
		]),
		("Interaction",
		[
			new("Expand / Collapse", "Interactive expand and collapse controls with programmatic buttons.", "unfold_more", "expand-collapse"),
			new("Tree Events", "Handle every tree event: click, hover, expand and collapse, with real-time feedback.", "mouse", "node-click"),
			new("Selection", "Select nodes and round-trip the change back to C# through OnSelectionChange.", "check_box", "selection"),
			new("Focus Mode", "Focus a subtree to dim everything outside it, then clear the focus to restore the full tree.", "center_focus_strong", "focus-mode", IsNew: true),
			new("Active Path", "Highlight the path from the root down to a node with SetActivePath, and read it back.", "route", "active-path", IsNew: true),
			new("Batch Verbs & Counts", "ExpandAll, CollapseAll, ExpandToDepth and ExpandSubtree, with hidden-descendant counts on the buttons.", "playlist_add_check", "batch-verbs", IsNew: true),
		]),
		("Data & display",
		[
			new("Live Data Updates", "UpdateData animates the difference between two datasets instead of redrawing the chart.", "autorenew", "live-data", IsNew: true),
			new("Semantic Zoom", "Node content that simplifies as you zoom out, so a large tree stays readable.", "zoom_in", "semantic-zoom", IsNew: true),
			new("Theme & Search", "The built-in dark theme, search box and breadcrumb trail.", "manage_search", "theme-search"),
			new("Localization & RTL", "Translated interface messages and a full right-to-left mirror of the layout.", "translate", "localization"),
		]),
	];
}
