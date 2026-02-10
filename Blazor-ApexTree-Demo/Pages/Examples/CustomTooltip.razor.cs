using ApexTree;
using Microsoft.AspNetCore.Components;

namespace Blazor_ApexTree_Demo.Pages.Examples;

/// <summary>
/// Demonstrates custom tooltips with HTML templates and styling options.
/// </summary>
public partial class CustomTooltip : ComponentBase
{
	private DataNode<string>? ParentNode;

	private readonly ApexTreeOptions Options = new()
	{
		EnableToolbar = true,
		EnableTooltip = true,
		Width = 100,
		WidthUnits = LengthUnits.Percent,
		Height = 70,
		HeightUnits = LengthUnits.Viewport,
		TooltipTemplate = "(content) => { return `<div style='padding: 10px; font-size: 13px;'><strong>${content}</strong><br/><span style='color: #666;'>Hover for details</span></div>`; }",
		TooltipBGColor = "#FAFAFA",
		TooltipBorderColor = "#5C6BC0",
		TooltipFontColor = "#333333",
		TooltipFontSize = 13,
		TooltipMinWidth = 120,
		TooltipMaxWidth = 250,
		TooltipPadding = 0,
		TooltipOffset = 12,
	};

	/// <inheritdoc/>
	protected override void OnInitialized()
	{
		ParentNode = new()
		{
			Id = "board",
			Data = "Board of Directors",
			Children =
			[
				new()
				{
					Id = "exec",
					Data = "Executive Team",
					Children =
					[
						new() { Id = "strategy", Data = "Strategy" },
						new() { Id = "innovation", Data = "Innovation" },
					]
				},
				new()
				{
					Id = "advisory",
					Data = "Advisory Board",
					Children =
					[
						new() { Id = "legal", Data = "Legal Counsel" },
						new() { Id = "compliance", Data = "Compliance" },
						new() { Id = "audit", Data = "Audit" },
					]
				},
				new()
				{
					Id = "committees",
					Data = "Committees",
					Children =
					[
						new() { Id = "finance-comm", Data = "Finance Committee" },
						new() { Id = "risk-comm", Data = "Risk Committee" },
					]
				},
			]
		};
	}
}
