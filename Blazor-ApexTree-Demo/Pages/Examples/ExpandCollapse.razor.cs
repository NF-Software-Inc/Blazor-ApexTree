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
	private readonly List<string> ParentNodeIds = [];

	private readonly ApexTreeOptions Options = new()
	{
		EnableExpandCollapse = true,
		EnableToolbar = true,
		Width = 100,
		WidthUnits = LengthUnits.Percent,
		Height = 65,
		HeightUnits = LengthUnits.Viewport,
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

		// Collect IDs of nodes that have children (can be collapsed/expanded)
		CollectParentIds(ParentNode);
	}

	private void CollectParentIds(DataNode<string> node)
	{
		if (node.Children is { Count: > 0 })
		{
			if (node.Id != null)
				ParentNodeIds.Add(node.Id);

			foreach (var child in node.Children)
				CollectParentIds(child);
		}
	}

	private async Task CollapseAll()
	{
		if (TreeRef == null) return;

		foreach (var id in ParentNodeIds)
			await TreeRef.CollapseNode(id);
	}

	private async Task ExpandAll()
	{
		if (TreeRef == null) return;

		foreach (var id in ParentNodeIds)
			await TreeRef.ExpandNode(id);
	}
}
