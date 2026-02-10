using ApexTree;
using Microsoft.AspNetCore.Components;

namespace Blazor_ApexTree_Demo.Pages.Examples;

/// <summary>
/// Demonstrates custom node templates with different font families and image avatars.
/// </summary>
public partial class CustomTemplate : ComponentBase
{
	private DataNode<Image>? ParentNode;

	private readonly ApexTreeOptions Options = new()
	{
		EnableToolbar = true,
		EnableExpandCollapse = true,
		Width = 100,
		WidthUnits = LengthUnits.Percent,
		Height = 70,
		HeightUnits = LengthUnits.Viewport,
		NodeWidth = 180,
		NodeHeight = 100,
		FontFamily = "Georgia, serif",
		NodeTemplate = "(content) => { return `<div style='display: flex; flex-direction: column; justify-content: center; align-items: center; height: 100%; padding: 8px; gap: 4px;'><img style='width: 44px; height: 44px; border-radius: 50%; border: 2px solid #5C6BC0;' src='${content.url}' /><div style='font-family: Georgia, serif; font-size: 13px; font-weight: 700; color: #333;'>${content.name}</div></div>`; }",
	};

	/// <inheritdoc/>
	protected override void OnInitialized()
	{
		ParentNode = new()
		{
			Id = "alice",
			Data = new Image("Alice Johnson", "https://randomuser.me/api/portraits/lego/1.jpg"),
			Children =
			[
				new()
				{
					Id = "bob",
					Data = new Image("Bob Williams", "https://randomuser.me/api/portraits/lego/2.jpg"),
					Children =
					[
						new() { Id = "dave", Data = new Image("Dave Chen", "https://randomuser.me/api/portraits/lego/5.jpg") },
						new() { Id = "eve", Data = new Image("Eve Martinez", "https://randomuser.me/api/portraits/lego/6.jpg") },
					]
				},
				new()
				{
					Id = "carol",
					Data = new Image("Carol Davis", "https://randomuser.me/api/portraits/lego/3.jpg"),
					Children =
					[
						new() { Id = "frank", Data = new Image("Frank Wilson", "https://randomuser.me/api/portraits/lego/7.jpg") },
					]
				},
				new()
				{
					Id = "grace",
					Data = new Image("Grace Lee", "https://randomuser.me/api/portraits/lego/4.jpg"),
				},
			]
		};
	}
}
