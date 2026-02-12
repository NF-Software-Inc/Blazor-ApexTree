using Microsoft.AspNetCore.Components;

namespace Blazor_ApexTree_Demo.Pages;

public partial class Home : ComponentBase
{
	private record ExampleInfo(string Title, string Description, string Icon, string Url);

	private readonly ExampleInfo[] Examples =
	[
		new("Basic Tree", "A simple string-based organizational chart with default settings.", "account_tree", "/basic"),
		new("Custom Template", "Custom node templates with different font families and image avatars.", "text_fields", "/custom-template"),
		new("Styled Nodes", "Per-node styling with different colors and borders for each department.", "palette", "/styled-nodes"),
		new("Expand / Collapse", "Interactive expand and collapse controls with programmatic buttons.", "unfold_more", "/expand-collapse"),
		new("Custom Tooltip", "Tooltips with custom HTML templates and styling options.", "chat_bubble_outline", "/custom-tooltip"),
		new("Tree Events", "Handle all tree events: click, hover, expand, and collapse with real-time feedback.", "mouse", "/node-click"),
		new("Dynamic View Change", "Switch the tree layout direction dynamically between all four orientations.", "swap_horiz", "/dynamic-view"),
	];
}
