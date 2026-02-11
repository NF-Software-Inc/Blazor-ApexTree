using ApexTree;
using Microsoft.AspNetCore.Components;

namespace Blazor_ApexTree_Demo.Pages.Examples;

/// <summary>
/// Demonstrates event callbacks: OnNodeClick, OnNodeHover, OnNodeExpand, and OnNodeCollapse.
/// </summary>
public partial class NodeClick : ComponentBase
{
	private DataNode<string>? ParentNode;
	
	// Click event tracking
	private string? LastClickedNode;
	private string? LastClickedData;
	private int ClickCount;
	
	// Hover event tracking
	private string? LastHoveredNode;
	private string? LastHoveredData;
	private int HoverCount;
	
	// Expand event tracking
	private string? LastExpandedNode;
	private string? LastExpandedData;
	private int ExpandCount;
	
	// Collapse event tracking
	private string? LastCollapsedNode;
	private string? LastCollapsedData;
	private int CollapseCount;
	
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

	/// <summary>
	/// Extracts the original node ID from the ApexTree-generated ID.
	/// ApexTree adds prefixes/suffixes to node IDs, so we need to find the original ID.
	/// </summary>
	private string? ExtractNodeData(string? nodeId)
	{
		if (string.IsNullOrEmpty(nodeId))
			return "Unknown";

		// Try direct lookup first
		if (NodeDataMap.TryGetValue(nodeId, out var data))
			return data;

		// ApexTree may add prefixes/suffixes, so check if our ID is contained in the nodeId
		foreach (var kvp in NodeDataMap)
		{
			if (nodeId.Contains(kvp.Key))
				return kvp.Value;
		}

		return "Unknown";
	}

	private void HandleNodeClick(NodeClickEventArgs args)
	{
		LastClickedNode = args.NodeId;
		LastClickedData = ExtractNodeData(args.NodeId);
		ClickCount++;
	}

	private void HandleNodeHover(NodeHoverEventArgs args)
	{
		LastHoveredNode = args.NodeId;
		LastHoveredData = ExtractNodeData(args.NodeId);
		HoverCount++;
	}

	private void HandleNodeExpand(NodeExpandEventArgs args)
	{
		LastExpandedNode = args.NodeId;
		LastExpandedData = ExtractNodeData(args.NodeId);
		ExpandCount++;
	}

	private void HandleNodeCollapse(NodeCollapseEventArgs args)
	{
		LastCollapsedNode = args.NodeId;
		LastCollapsedData = ExtractNodeData(args.NodeId);
		CollapseCount++;
	}
}
