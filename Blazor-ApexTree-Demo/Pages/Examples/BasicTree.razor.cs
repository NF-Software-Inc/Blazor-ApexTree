using ApexTree;
using Microsoft.AspNetCore.Components;

namespace Blazor_ApexTree_Demo.Pages.Examples;

/// <summary>
/// Provides an example of basic functionality for ApexTree and its Blazor implementation.
/// </summary>
public partial class BasicTree : ComponentBase
{
	private DataNode<string>? ParentNode;

	private readonly ApexTreeOptions Options = new()
	{
		EnableExpandCollapse = true,
		EnableToolbar = true,
		Width = 100,
		WidthUnits = LengthUnits.Percent,
		Height = 70,
		HeightUnits = LengthUnits.Viewport,
	};

	/// <inheritdoc/>
	protected override void OnInitialized()
	{
		ParentNode = new()
		{
			Id = "ceo",
			Data = "CEO",
			Children =
			[
				new()
				{
					Id = "cto",
					Data = "CTO",
					Children =
					[
						new() { Id = "dev-lead", Data = "Dev Lead" },
						new() { Id = "sr-eng", Data = "Sr. Engineer" },
						new() { Id = "qa-lead", Data = "QA Lead" },
					]
				},
				new()
				{
					Id = "cfo",
					Data = "CFO",
					Children =
					[
						new() { Id = "accountant", Data = "Accountant" },
						new() { Id = "analyst", Data = "Financial Analyst" },
					]
				},
				new()
				{
					Id = "coo",
					Data = "COO",
					Children =
					[
						new() { Id = "ops-mgr", Data = "Operations Mgr" },
						new() { Id = "logistics", Data = "Logistics" },
					]
				},
			]
		};
	}
}
