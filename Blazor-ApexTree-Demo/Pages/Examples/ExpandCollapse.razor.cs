using ApexTree;
using Microsoft.AspNetCore.Components;

namespace Blazor_ApexTree_Demo.Pages.Examples;

/// <summary>
/// Demonstrates programmatic expand and collapse functionality.
/// </summary>
public partial class ExpandCollapse : ComponentBase
{
	private ApexTree<string>? TreeRef;
	private DataNode<string>? ParentNode;

	private readonly ApexTreeOptions Options = new()
	{
		EnableExpandCollapse = true,
		EnableToolbar = true,
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
			Id = "company",
			Data = "Acme Corp",
			Children =
			[
				new()
				{
					Id = "product",
					Data = "Product",
					Children =
					[
						new() { Id = "design", Data = "Design" },
						new() { Id = "research", Data = "Research" },
						new() { Id = "testing", Data = "Testing" },
					]
				},
				new()
				{
					Id = "sales",
					Data = "Sales",
					Children =
					[
						new() { Id = "domestic", Data = "Domestic" },
						new() { Id = "international", Data = "International" },
					]
				},
				new()
				{
					Id = "support",
					Data = "Support",
					Children =
					[
						new() { Id = "tier1", Data = "Tier 1" },
						new() { Id = "tier2", Data = "Tier 2" },
						new() { Id = "tier3", Data = "Tier 3" },
					]
				},
			]
		};

	}

	// One core call each since apextree 2.0. This previously walked the tree collecting every
	// parent id and then issued one interop call per node, so each node reflowed separately.
	private async Task CollapseAll()
	{
		if (TreeRef != null)
			await TreeRef.CollapseAll();
	}

	private async Task ExpandAll()
	{
		if (TreeRef != null)
			await TreeRef.ExpandAll();
	}
}
