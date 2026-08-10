# Blazor-ApexTree

[![MIT](https://img.shields.io/github/license/NF-Software-Inc/Blazor-ApexTree)](https://github.com/NF-Software-Inc/Blazor-ApexTree/blob/master/LICENSE)
[![NuGet](https://img.shields.io/nuget/v/Blazor-ApexTree.svg)](https://www.nuget.org/packages/Blazor-ApexTree/)
[![Build](https://img.shields.io/github/actions/workflow/status/NF-Software-Inc/Blazor-ApexTree/build.yml)](https://github.com/NF-Software-Inc/Blazor-ApexTree/actions/workflows/build.yml)
[![Publish](https://img.shields.io/github/actions/workflow/status/NF-Software-Inc/Blazor-ApexTree/publish.yml?label=publish)](https://github.com/NF-Software-Inc/Blazor-ApexTree/actions/workflows/publish.yml)

A modern, feature-rich Blazor wrapper for [ApexTree.js](https://apexcharts.com/apextree/) that enables you to create beautiful, interactive hierarchical tree visualizations in your Blazor applications.

## Features

- **Simple API** - Easy-to-use component with strongly-typed data models
- **Fully Customizable** - Control colors, sizing, borders, fonts, and more
- **Multiple Data Types** - Built-in support for `string` and `Image` types, plus custom templates
- **Custom Templates** - Create custom node and tooltip templates with HTML/CSS
- **Interactive Events** - Hook into click, hover, expand, and collapse events
- **Expand/Collapse** - Built-in support for collapsible tree structures
- **Tooltips** - Fully customizable hover tooltips
- **Per-Node Styling** - Individual customization for each node in the tree
- **Multiple Directions** - Render trees top-down, bottom-up, left-to-right, right-to-left, or radial
- **Spring Motion** - Collapse, expand and data changes animate, and can be interrupted mid-flight
- **Live Data Updates** - `UpdateData` reconciles a new dataset instead of redrawing it
- **Focus Mode** - Spotlight a node's lineage and subtree, dimming everything else
- **Semantic Zoom** - Nodes shed detail as they shrink, so a zoomed-out tree stays readable
- **Expandable Cards** - A card can expand in place to reveal stats, a progress meter and actions
- **Active Path** - Flow an animated dash along the route from the root to any node
- **Multi-Framework** - Targets .NET 8.0, 9.0, and 10.0

## 📦 Installation

Install via NuGet Package Manager:

```bash
dotnet add package Blazor-ApexTree
```

Or via the NuGet Package Manager Console:

```powershell
Install-Package Blazor-ApexTree
```

Visit the [NuGet package page](https://www.nuget.org/packages/Blazor-ApexTree/) for more details.

## Quick Start

### Basic Usage

A simple tree with a few nodes:

```razor
@code {
    private DataNode<string> Root = new()
    {
        Data = "CEO",
        Children =
        [
            new() { Data = "CTO" },
            new() { Data = "CFO" }
        ]
    };
}

<ApexTree TItem="string" Parent="Root" />
```

### Building a Tree Structure

For more complex trees, use the `AddChild()` helper methods to build hierarchical structures:

```razor
@page "/tree"

<ApexTree TItem="string" Parent="ParentNode" Options="Options" />

@code {
    private DataNode<string> ParentNode { get; set; } = new();
    private ApexTreeOptions Options { get; set; } = new();

    protected override void OnInitialized()
    {
        ParentNode.Id = "root";
        ParentNode.Data = "CEO";

        var cto = ParentNode.AddChild("cto", "CTO");
        cto.AddChild("dev1", "Developer 1");
        cto.AddChild("dev2", "Developer 2");

        var cfo = ParentNode.AddChild("cfo", "CFO");
        cfo.AddChild("acc1", "Accountant");
    }
}
```

### Using Events

```razor
<ApexTree TItem="string"
          Parent="ParentNode"
          Options="Options"
          OnNodeClick="HandleNodeClick"
          OnNodeHover="HandleNodeHover"
          OnNodeExpand="HandleNodeExpand"
          OnNodeCollapse="HandleNodeCollapse" />

@code {
    private void HandleNodeClick(NodeClickEventArgs e)
    {
        Console.WriteLine($"Node clicked: {e.NodeId} at {e.Timestamp}");
    }

    private void HandleNodeHover(NodeHoverEventArgs e)
    {
        Console.WriteLine($"Node hovered: {e.NodeId}");
    }

    private void HandleNodeExpand(NodeExpandEventArgs e)
    {
        Console.WriteLine($"Node expanded: {e.NodeId}");
    }

    private void HandleNodeCollapse(NodeCollapseEventArgs e)
    {
        Console.WriteLine($"Node collapsed: {e.NodeId}");
    }
}
```

### Custom Styling

```csharp
var options = new ApexTreeOptions
{
    Width = 800,
    Height = 600,
    Direction = Direction.Top,
    NodeWidth = 180,
    NodeHeight = 120,
    NodeBGColor = "#E3F2FD",
    NodeBGColorHover = "#BBDEFB",
    BorderColor = "#2196F3",
    BorderColorHover = "#1976D2",
    EnableExpandCollapse = true,
    EnableTooltip = true,
    HighlightOnHover = true
};
```

### Per-Node Customization

```csharp
var ceo = new DataNode<string>
{
    Id = "ceo",
    Data = "CEO",
    Options = new DataNodeOptions
    {
        NodeBGColor = "#FF5722",
        BorderColor = "#D84315",
        FontColor = "#FFFFFF",
        FontWeight = 700
    }
};
```

### Custom Templates

```csharp
var options = new ApexTreeOptions
{
    NodeTemplate = @"(content) => {
        return `<div style='display: flex; flex-direction: column; align-items: center; padding: 10px;'>
                  <strong>${content}</strong>
                  <small>Custom Content</small>
                </div>`;
    }",
    TooltipTemplate = @"(content) => {
        return `<div style='padding: 8px;'><strong>Info:</strong> ${content}</div>`;
    }"
};
```

## License Management

For commercial ApexTree licenses, configure the license key in your `Program.cs` before rendering any trees:

```csharp
using ApexTree;

// In Program.cs
ApexTreeLicense.SetLicense("your-commercial-license-key-here");

await builder.Build().RunAsync();
```

The license only needs to be set once for your entire application.

## Component Parameters

### ApexTree Component

| Parameter        | Type                                   | Description                                                                           |
| ---------------- | -------------------------------------- | ------------------------------------------------------------------------------------- |
| `Parent`         | `DataNode<TItem>`                      | **Required.** The root node of the tree. Add all children to this node.               |
| `Options`        | `ApexTreeOptions`                      | Configuration options for the tree. Each instance should have its own options object. |
| `OnNodeClick`    | `EventCallback<NodeClickEventArgs>`    | Callback invoked when a node is clicked.                                              |
| `OnNodeHover`    | `EventCallback<NodeHoverEventArgs>`    | Callback invoked when a node is hovered.                                              |
| `OnNodeExpand`   | `EventCallback<NodeExpandEventArgs>`   | Callback invoked when a node is expanded.                                             |
| `OnNodeCollapse` | `EventCallback<NodeCollapseEventArgs>` | Callback invoked when a node is collapsed.                                            |

### Component Methods

Capture the component with `@ref` to call these.

```razor
<ApexTree @ref="TreeRef" TItem="string" Parent="Root" Options="Options" />

@code {
    private ApexTree<string>? TreeRef;

    private async Task NextQuarter() => await TreeRef!.UpdateData(NextRoot);
}
```

| Method                    | Description                                                                    |
| ------------------------- | ------------------------------------------------------------------------------ |
| `UpdateData(data)`        | Reconcile a new dataset with animation, preserving collapse/selection/focus     |
| `Construct(data)`         | Replace the data and redraw without animation                                   |
| `RebuildChart()`          | Destroy and recreate the chart, e.g. after an options change                     |
| `CollapseNode(id)`        | Collapse one node                                                               |
| `ExpandNode(id)`          | Expand one node                                                                 |
| `CollapseAll()`           | Collapse every node                                                             |
| `ExpandAll()`             | Expand every node                                                               |
| `ExpandToDepth(depth)`    | Expand down to a depth, collapsing everything deeper                            |
| `ExpandSubtree(id)`       | Expand a node and everything beneath it                                         |
| `CollapseSubtree(id)`     | Collapse a node and everything beneath it                                       |
| `Focus(id)`               | Spotlight a node's lineage and subtree                                          |
| `ClearFocus()`            | Clear the spotlight                                                             |
| `GetFocusedNodeId()`      | Read the spotlighted node id, or `null`                                         |
| `SetActivePath(ids)`      | Flow an animated dash from the root to each node                                |
| `ClearActivePath()`       | Clear the active path                                                           |
| `GetActivePath()`         | Read the ids currently on the active path                                       |
| `ExpandCard(id)`          | Expand a node's card in place to show its detail section                        |
| `CollapseCard(id)`        | Collapse a node's card back to its summary                                      |
| `ToggleCard(id)`          | Toggle a node's card                                                            |
| `SetExpandedCards(ids)`   | Replace the set of expanded cards                                               |
| `GetExpandedCards()`      | Read which cards are expanded                                                   |
| `ChangeLayout(direction)` | Switch growth direction                                                         |
| `FitScreen()`             | Fit the tree to the viewport                                                    |
| `CenterOnNode(id)`        | Centre the camera on a node, keeping the zoom                                   |
| `Zoom(factor)`            | Zoom relative to the current scale                                              |
| `ResetPanZoomBaseAsync()` | Re-base pan/zoom after a programmatic viewBox change                            |
| `ExportToSvg()`           | Download the tree as an SVG file                                                |
| `Render(keepOldPosition)` | Re-render in place                                                              |

Everything from `UpdateData` through `GetExpandedCards`, plus `CenterOnNode`, requires ApexTree core
2.0.0 or later, which is bundled from Blazor-ApexTree 11.0.0 onward.

### ApexTreeOptions

#### Layout & Sizing

| Property          | Type          | Default   | Description                                           |
| ----------------- | ------------- | --------- | ----------------------------------------------------- |
| `Width`           | `int`         | `400`     | Width of the tree container (use with `WidthUnits`)   |
| `WidthUnits`      | `LengthUnits` | `Default` | Unit of measurement for width                         |
| `Height`          | `int`         | `400`     | Height of the tree container (use with `HeightUnits`) |
| `HeightUnits`     | `LengthUnits` | `Default` | Unit of measurement for height                        |
| `Direction`       | `Direction`   | `Top`     | Tree direction: `Top`, `Bottom`, `Left`, or `Right`   |
| `SiblingSpacing`  | `int`         | `50`      | Spacing between sibling nodes in pixels               |
| `ChildrenSpacing` | `int?`        | `20`      | Spacing between children and parent in pixels         |

#### Node Styling

| Property           | Type      | Default   | Description                     |
| ------------------ | --------- | --------- | ------------------------------- |
| `NodeWidth`        | `int?`    | `150`     | Width of each node in pixels    |
| `NodeHeight`       | `int?`    | `100`     | Height of each node in pixels   |
| `NodeBGColor`      | `string?` | `#FFFFFF` | Background color of nodes (hex) |
| `NodeBGColorHover` | `string?` | `#FFFFFF` | Background color on hover (hex) |
| `BorderWidth`      | `int?`    | `1`       | Border width in pixels          |
| `BorderStyle`      | `string?` | `solid`   | CSS border style                |
| `BorderRadius`     | `int?`    | `5`       | Border radius in pixels         |
| `BorderColor`      | `string?` | `#BCBCBC` | Border color (hex)              |
| `BorderColorHover` | `string?` | `#5C6BC0` | Border color on hover (hex)     |
| `FontSize`         | `int?`    | `14`      | Font size in pixels             |
| `FontWeight`       | `int?`    | `400`     | Font weight                     |
| `FontColor`        | `string?` | `#000000` | Font color (hex)                |

#### Features

| Property                | Type    | Default | Description                          |
| ----------------------- | ------- | ------- | ------------------------------------ |
| `EnableExpandCollapse`  | `bool`  | `false` | Enable expand/collapse functionality |
| `EnableTooltip`         | `bool`  | `false` | Enable tooltips on node hover        |
| `EnableToolbar`         | `bool`  | `false` | Enable graph toolbar                 |
| `HighlightOnHover`      | `bool`  | `true`  | Highlight nodes on hover             |
| `GroupLeafNodes`        | `bool?` | `null`  | Group leaf nodes together            |
| `GroupLeafNodesSpacing` | `int?`  | `null`  | Spacing between grouped leaf nodes   |

#### ApexTree 2.0 options

All added in Blazor-ApexTree 11.0.0, which bundles ApexTree core 2.0.0. Every one is optional and
leaves the previous behaviour in place when unset.

| Property                           | Type                       | Default | Description                                                                     |
| ---------------------------------- | -------------------------- | ------- | ------------------------------------------------------------------------------- |
| `LayoutType`                       | `LayoutType?`              | `Tree`  | `Cluster` pins leaves to the outer rank; pair with `Direction.Radial`            |
| `Motion`                           | `MotionOptions?`           | `null`  | Spring preset and expand stagger                                                |
| `Focus`                            | `FocusOptions?`            | `null`  | Click-to-focus gesture and dim strength for spotlight mode                       |
| `SemanticZoom`                     | `SemanticZoomOptions?`     | `null`  | Level-of-detail thresholds in on-screen node pixels                             |
| `AutoNodeHeight`                   | `AutoNodeHeightOptions?`   | `null`  | Measure each node's content and give it its own height                          |
| `CardExpansion`                    | `CardExpansionOptions?`    | `null`  | Let a card expand in place to reveal its detail section                          |
| `EdgeFlow`                         | `EdgeFlowOptions?`         | `null`  | Colour, speed and dash styling for the active path                              |
| `MaxZoomNodeSpan`                  | `int?`                     | `8`     | Caps zoom-in when re-fitting after collapse; `0` disables the cap               |
| `EnableCommandPalette`             | `bool?`                    | `false` | Ctrl/Cmd + K overlay for jumping to a node and expanding or collapsing all      |
| `CountBadgeEnabled`                | `bool?`                    | `false` | Persistent count badge on every node, independent of collapse state              |
| `CountBadgeSource`                 | `CountBadgeSource?`        | `Descendants` | What the count badge counts                                               |
| `CountBadgeDataKey`                | `string?`                  | `count` | Key read from node data when the source is `Data`                               |
| `CountBadgeThreshold`              | `int?`                     | `1`     | Minimum count before the badge appears                                          |
| `CountBadgeBGColor`                | `string?`                  | `#EEF2FF` | Count badge background                                                        |
| `CountBadgeFontColor`              | `string?`                  | `#3730A3` | Count badge text colour                                                       |
| `CountBadgeFontSize`               | `int?`                     | `12`    | Count badge font size                                                           |
| `ExpandCollapseButtonSize`         | `int?`                     | `15`    | Button diameter; the tap target stays at least 24px regardless                   |
| `ExpandCollapseButtonIconColor`    | `string?`                  | `#475467` | Colour of the `+`/`-` glyph                                                   |
| `ExpandCollapseButtonHaloColor`    | `string?`                  | `null`  | Opaque ring behind the button, in your page background colour                    |

`ExternalLabel` also gained `CollisionStrategy`, which culls colliding labels on crowded radial
rings. `OrgNodeData` gained `Tags`, `Stats`, `Progress`, `Actions` and `Details` for the expandable
card, and `DataNode<TItem>` gained `HasChildren` for nodes whose children load later.

#### Templates

| Property          | Type      | Description                                                     |
| ----------------- | --------- | --------------------------------------------------------------- |
| `NodeTemplate`    | `string?` | JavaScript function returning HTML for custom node templates    |
| `TooltipTemplate` | `string?` | JavaScript function returning HTML for custom tooltip templates |

#### Tooltip Styling

| Property             | Type      | Description                         |
| -------------------- | --------- | ----------------------------------- |
| `TooltipBorderColor` | `string?` | Tooltip border color (hex)          |
| `TooltipBGColor`     | `string?` | Tooltip background color (hex)      |
| `TooltipMaxWidth`    | `int?`    | Maximum tooltip width in pixels     |
| `TooltipMinWidth`    | `int?`    | Minimum tooltip width in pixels     |
| `TooltipFontColor`   | `string?` | Tooltip font color (hex)            |
| `TooltipFontSize`    | `int?`    | Tooltip font size                   |
| `TooltipPadding`     | `int?`    | Tooltip padding in pixels           |
| `TooltipOffset`      | `int?`    | Distance between tooltip and cursor |

### DataNode<TItem>

| Property   | Type                     | Description                                        |
| ---------- | ------------------------ | -------------------------------------------------- |
| `Id`       | `string?`                | Unique HTML element ID for this node (recommended) |
| `Data`     | `TItem?`                 | Content to display in the node                     |
| `Options`  | `DataNodeOptions?`       | Node-specific styling options                      |
| `Children` | `List<DataNode<TItem>>?` | Child nodes                                        |

**Helper Methods:**

- `AddChild(TItem data)` - Add a child with auto-generated ID
- `AddChild(string id, TItem data)` - Add a child with specific ID
- `AddChild(string? id, TItem data, DataNodeOptions? options)` - Add a child with full options
- `AddChild(IEnumerable<TItem> data)` - Add multiple children

## Supported Data Types

### Built-in Support

1. **String** (`string?`) - Simple text nodes
2. **Image** (`ApexTree.Image`) - Nodes with name and URL/image path

### Custom Types

For custom data types, provide a `NodeTemplate` function in the options:

```csharp
public class Employee
{
    public string Name { get; set; }
    public string Title { get; set; }
}

var options = new ApexTreeOptions
{
    NodeTemplate = @"(content) => {
        const data = JSON.parse(content);
        return `<div style='text-align: center;'>
                  <strong>${data.Name}</strong><br/>
                  <small>${data.Title}</small>
                </div>`;
    }"
};
```

## Event Arguments

All event argument classes include:

- `NodeId` (string?) - The ID of the affected node
- `Timestamp` (DateTime) - When the event occurred (UTC)

Available event argument types:

- `NodeClickEventArgs`
- `NodeHoverEventArgs`
- `NodeExpandEventArgs`
- `NodeCollapseEventArgs`

## Versioning Notice

As of version 10.0.0, Blazor-ApexTree has adopted a versioning scheme that aligns with .NET major versions:

- **Major version** corresponds to the .NET major version (e.g., Blazor-ApexTree 10.x.x targets .NET 10)
- **Minor and patch versions** are used for library-specific updates and do not map to .NET's minor or SDK versions

This provides better clarity and consistency regarding supported frameworks.

## Target Frameworks

- .NET 8.0
- .NET 9.0
- .NET 10.0

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments

- [joadan](https://github.com/apexcharts/Blazor-ApexCharts) - For the fantastic Blazor-ApexCharts project that inspired this wrapper
