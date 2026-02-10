using ApexTree;
using Microsoft.AspNetCore.Components;

namespace Blazor_ApexTree_Demo.Pages.Examples;

/// <summary>
/// Demonstrates the OnNodeClick event callback for handling click events on tree nodes.
/// </summary>
public partial class NodeClick : ComponentBase
{
	private DataNode<string>? ParentNode;
	private string? ClickedNodeId;
	private string? ClickedNodeData;
	private int ClickCount;
	private readonly Dictionary<string, string> NodeDataMap = [];

	private readonly ApexTreeOptions Options = new()
	{
		EnableToolbar = true,
		EnableExpandCollapse = true,
		Width = 100,
		WidthUnits = LengthUnits.Percent,
		Height = 60,
		HeightUnits = LengthUnits.Viewport,
	};

	/// <inheritdoc/>
	protected override void OnInitialized()
	{
		ParentNode = new()
		{
			Id = "president",
			Data = "President",
			Children =
			[
				new()
				{
					Id = "vp-tech",
					Data = "VP Technology",
					Children =
					[
						new() { Id = "arch", Data = "Architect" },
						new() { Id = "lead-dev", Data = "Lead Developer" },
						new() { Id = "dba", Data = "DBA" },
					]
				},
				new()
				{
					Id = "vp-sales",
					Data = "VP Sales",
					Children =
					[
						new() { Id = "regional-mgr", Data = "Regional Manager" },
						new() { Id = "account-exec", Data = "Account Executive" },
					]
				},
				new()
				{
					Id = "vp-hr",
					Data = "VP Human Resources",
					Children =
					[
						new() { Id = "recruiter", Data = "Recruiter" },
						new() { Id = "benefits", Data = "Benefits Admin" },
					]
				},
			]
		};

		// Build lookup map for node ID -> data
		BuildNodeDataMap(ParentNode);
	}

	private void BuildNodeDataMap(DataNode<string> node)
	{
		if (node.Id != null && node.Data != null)
			NodeDataMap[node.Id] = node.Data;

		if (node.Children != null)
		{
			foreach (var child in node.Children)
				BuildNodeDataMap(child);
		}
	}

	private void HandleNodeClick(NodeClickEventArgs args)
	{
		ClickedNodeId = args.NodeId;
		ClickedNodeData = NodeDataMap.GetValueOrDefault(args.NodeId ?? "", "Unknown");
		ClickCount++;
	}
}
