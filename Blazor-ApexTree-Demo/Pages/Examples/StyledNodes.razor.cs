using ApexTree;
using Microsoft.AspNetCore.Components;

namespace Blazor_ApexTree_Demo.Pages.Examples;

/// <summary>
/// Demonstrates per-node styling with different colors and borders using DataNodeOptions.
/// </summary>
public partial class StyledNodes : ComponentBase
{
	private DataNode<string>? ParentNode;

	private readonly ApexTreeOptions Options = new()
	{
		EnableToolbar = true,
		EnableExpandCollapse = true,
		Width = 100,
		WidthUnits = LengthUnits.Percent,
		Height = 70,
		HeightUnits = LengthUnits.Viewport,
		NodeWidth = 160,
		NodeHeight = 50
	};

	private static readonly DataNodeOptions CeoStyle = new()
	{
		NodeBGColor = "#E3F2FD",
		NodeBGColorHover = "#BBDEFB",
		BorderColor = "#1565C0",
		BorderColorHover = "#0D47A1",
		BorderWidth = 2,
		FontColor = "#1565C0",
		FontWeight = 700
	};

	private static readonly DataNodeOptions EngineeringStyle = new()
	{
		NodeBGColor = "#E8F5E9",
		NodeBGColorHover = "#C8E6C9",
		BorderColor = "#2E7D32",
		BorderColorHover = "#1B5E20",
		FontColor = "#2E7D32"
	};

	private static readonly DataNodeOptions MarketingStyle = new()
	{
		NodeBGColor = "#FFF3E0",
		NodeBGColorHover = "#FFE0B2",
		BorderColor = "#E65100",
		BorderColorHover = "#BF360C",
		FontColor = "#E65100"
	};

	private static readonly DataNodeOptions HrStyle = new()
	{
		NodeBGColor = "#F3E5F5",
		NodeBGColorHover = "#E1BEE7",
		BorderColor = "#7B1FA2",
		BorderColorHover = "#4A148C",
		FontColor = "#7B1FA2"
	};

	private static readonly DataNodeOptions FinanceStyle = new()
	{
		NodeBGColor = "#FCE4EC",
		NodeBGColorHover = "#F8BBD0",
		BorderColor = "#C62828",
		BorderColorHover = "#B71C1C",
		FontColor = "#C62828"
	};

	/// <inheritdoc/>
	protected override void OnInitialized()
	{
		ParentNode = new()
		{
			Id = "ceo",
			Data = "CEO",
			Options = CeoStyle,
			Children =
			[
				new()
				{
					Id = "engineering",
					Data = "Engineering",
					Options = EngineeringStyle,
					Children =
					[
						new() { Id = "frontend", Data = "Frontend", Options = EngineeringStyle },
						new() { Id = "backend", Data = "Backend", Options = EngineeringStyle },
						new() { Id = "devops", Data = "DevOps", Options = EngineeringStyle },
					]
				},
				new()
				{
					Id = "marketing",
					Data = "Marketing",
					Options = MarketingStyle,
					Children =
					[
						new() { Id = "content", Data = "Content", Options = MarketingStyle },
						new() { Id = "seo", Data = "SEO", Options = MarketingStyle },
					]
				},
				new()
				{
					Id = "hr",
					Data = "Human Resources",
					Options = HrStyle,
					Children =
					[
						new() { Id = "recruiting", Data = "Recruiting", Options = HrStyle },
						new() { Id = "training", Data = "Training", Options = HrStyle },
					]
				},
				new()
				{
					Id = "finance",
					Data = "Finance",
					Options = FinanceStyle,
					Children =
					[
						new() { Id = "accounting", Data = "Accounting", Options = FinanceStyle },
						new() { Id = "payroll", Data = "Payroll", Options = FinanceStyle },
					]
				},
			]
		};
	}
}
