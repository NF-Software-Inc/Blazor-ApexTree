using ApexTree;
using Microsoft.AspNetCore.Components;

namespace Blazor_ApexTree_Demo.Pages.Examples;

/// <summary>
/// Provides an example of basic functionality for ApexTree and its Blazor implementation.
/// </summary>
public partial class BasicTree : ComponentBase
{
	private DataNode<Image>? ParentNode;
	private int Ids;

	private readonly ApexTreeOptions Options = new()
	{
		EnableExpandCollapse = true,
		EnableToolbar = true,
		Width = 100,
		WidthUnits = LengthUnits.Percent,
		Height = 88,
		HeightUnits = LengthUnits.Viewport,
		NodeHeight = 75,
		NodeTemplate = "(content) => { if (content.url.length > 0) return `<div style='display: flex; flex-direction: column; justify-content: center; align-items: center; height: 100%;'><img style='width: 50px; height: 50px; border-radius: 50%;' src='${content.url}' /><div class='has-text-centered is-size-4 is-tiny'>${content.name}</div></div>`; else return `<div style='display: flex; flex-direction: column; justify-content: center; align-items: center; height: 100%;'><span style='width: 50px; height: 50px; border-radius: 50%; line-height: 1.25;' class='material-icons is-size-2 has-text-centered b-1 has-border-solid'>person</span><div class='has-text-centered is-size-4 is-tiny'>${content.name}</div></div>`; }"
	};

	/// <inheritdoc/>
	protected override void OnInitialized()
	{
		ParentNode = new()
		{
			Id = GetNextId(),
			Data = new Image("Margret Swanson", "https://randomuser.me/api/portraits/lego/1.jpg"),
			Children =
			[
				new()
				{
					Id = GetNextId(),
					Data = new Image("John Doe", "https://randomuser.me/api/portraits/lego/2.jpg")
				},
				new()
				{
					Id = GetNextId(),
					Data = new Image("Jane Doe", "https://randomuser.me/api/portraits/lego/3.jpg"),
					Children =
					[
						new()
						{
							Id = GetNextId(),
							Data = new Image("Sam Doe", "https://randomuser.me/api/portraits/lego/5.jpg")
						},
						new()
						{
							Id = GetNextId(),
							Data = new Image("Sally Doe", "https://randomuser.me/api/portraits/lego/6.jpg")
						}
					]
				},
				new()
				{
					Id = GetNextId(),
					Data = new Image("Sam Smith", "https://randomuser.me/api/portraits/lego/4.jpg")
				}
			]
		};
	}

	private string GetNextId() => (Ids++).ToString();
}
