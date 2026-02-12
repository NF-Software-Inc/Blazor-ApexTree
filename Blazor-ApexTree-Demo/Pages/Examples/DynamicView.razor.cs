using ApexTree;
using Microsoft.AspNetCore.Components;

namespace Blazor_ApexTree_Demo.Pages.Examples;

/// <summary>
/// Demonstrates dynamically changing the tree layout direction.
/// </summary>
public partial class DynamicView : ComponentBase
{
	private ApexTree<string>? TreeRef;
	private DataNode<string>? ParentNode;
	private Direction CurrentDirection = Direction.Top;

	private readonly ApexTreeOptions Options = new()
	{
		EnableToolbar = true,
		EnableExpandCollapse = true,
		Width = 100,
		WidthUnits = LengthUnits.Percent,
		Height = 65,
		HeightUnits = LengthUnits.Viewport
	};

	/// <inheritdoc/>
	protected override void OnInitialized()
	{
		ParentNode = new()
		{
			Id = "headquarters",
			Data = "Headquarters",
			Children =
			[
				new()
				{
					Id = "north",
					Data = "North Region",
					Children =
					[
						new() { Id = "ny", Data = "New York" },
						new() { Id = "boston", Data = "Boston" },
					]
				},
				new()
				{
					Id = "south",
					Data = "South Region",
					Children =
					[
						new() { Id = "miami", Data = "Miami" },
						new() { Id = "atlanta", Data = "Atlanta" },
						new() { Id = "dallas", Data = "Dallas" },
					]
				},
				new()
				{
					Id = "west",
					Data = "West Region",
					Children =
					[
						new() { Id = "sf", Data = "San Francisco" },
						new() { Id = "la", Data = "Los Angeles" },
						new() { Id = "seattle", Data = "Seattle" },
					]
				},
			]
		};
	}

	private async Task ChangeDirection(Direction direction)
	{
		if (TreeRef == null) return;

		CurrentDirection = direction;
		await TreeRef.ChangeLayout(direction);
	}
}
