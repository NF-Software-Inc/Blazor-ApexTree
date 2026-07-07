using ApexTree.Internal;
using System.Text.Json.Serialization;

namespace ApexTree;

/// <summary>
/// Main class to configure options that are to be serialized and passed to the JavaScript.
/// </summary>
/// <remarks>
/// Links:
///
/// <see href="https://apexcharts.com/apextree/docs/options">JavaScript Documentation</see>.
/// </remarks>
public class ApexTreeOptions : DataNodeOptions
{
	/// <summary>
	/// Enables debug mode when true.
	/// </summary>
	public bool Debug { get; set; }

	/// <summary>
	/// The width of graph container.
	/// </summary>
	[JsonIgnore]
	public int Width { get; set; } = 400;

	[JsonInclude]
	[JsonPropertyName("width")]
	private string? SerializeWidth { get => ChartSerializer.GetMeasurement(Width, WidthUnits, true); }

	/// <summary>
	/// The measurement type to use for the <see cref="Width"/>.
	/// </summary>
	[JsonIgnore]
	public LengthUnits WidthUnits { get; set; } = LengthUnits.Default;

	/// <summary>
	/// The height of graph container in pixels.
	/// </summary>
	[JsonIgnore]
	public int Height { get; set; } = 400;

	[JsonInclude]
	[JsonPropertyName("height")]
	private string? SerializeHeight { get => ChartSerializer.GetMeasurement(Height, HeightUnits, false); }

	/// <summary>
	/// The measurement type to use for the <see cref="Height"/>.
	/// </summary>
	[JsonIgnore]
	public LengthUnits HeightUnits { get; set; } = LengthUnits.Default;

	/// <summary>
	/// The direction of the tree to start rendering.
	/// </summary>
	public Direction Direction { get; set; } = Direction.Top;

	/// <summary>
	/// The key of content in passed data object.
	/// </summary>
	/// <remarks>
	/// The name of the data key after serialization that selects the text to display.
	/// </remarks>
	[JsonInclude]
	private string ContentKey { get; } = "data";

	/// <summary>
	/// The spacing between sibling nodes in pixels.
	/// </summary>
	public int SiblingSpacing { get; set; } = 50;

	/// <summary>
	/// The spacing between children and parent in pixels.
	/// </summary>
	public int? ChildrenSpacing { get; set; } = 20;

	/// <summary>
	/// Enable or disable highlight on hover.
	/// </summary>
	public bool HighlightOnHover { get; set; } = true;

	/// <summary>
	/// The class name for the root container.
	/// </summary>
	[JsonInclude]
	private string ContainerClassName { get; } = "root";

	/// <summary>
	/// The CSS styles to apply to the canvas root container.
	/// </summary>
	public string? CanvasStyle { get; set; }

	/// <summary>
	/// Enable or disable graph toolbar.
	/// </summary>
	public bool EnableToolbar { get; set; }

	/// <summary>
	/// Enables expanding and collapsing nodes.
	/// </summary>
	public bool EnableExpandCollapse { get; set; }

	/// <summary>
	/// Enable tooltip on hover of nodes.
	/// </summary>
	public bool EnableTooltip { get; set; }

	/// <summary>
	/// Groups leaf nodes together when true.
	/// </summary>
	public bool? GroupLeafNodes { get; set; }

	/// <summary>
	/// The spacing between grouped leaf nodes in pixels.
	/// </summary>
	public int? GroupLeafNodesSpacing { get; set; }

	/// <summary>
	/// A function that returns the HTML template for nodes. Example:
	///
	/// <code>
	/// (content) => { return `&lt;div style='display: flex; justify-content: center; align-items: center; text-align: center; height: 100%;'&gt;${content}&lt;/div&gt;`; }
	/// </code>
	/// </summary>
	/// <remarks>
	/// The function will receive a parameter containing a single item from the collection.
	/// </remarks>
	[JsonConverter(typeof(FunctionStringConverter))]
	public string? NodeTemplate { get; set; }

	/// <summary>
	/// A function that returns the HTML template for tooltip. Example:
	///
	/// <code>
	/// (content) => { return `&lt;div style='display: flex; justify-content: center; align-items: center; text-align: center; height: 100%;'&gt;${content}&lt;/div&gt;`; }
	/// </code>
	/// </summary>
	/// <remarks>
	/// The function will receive a string parameter for the provided content.
	/// </remarks>
	[JsonConverter(typeof(FunctionStringConverter))]
	public string? TooltipTemplate { get; set; }

	/// <inheritdoc />
	public override int? NodeWidth { get; set; } = 150;

	/// <inheritdoc />
	public override int? NodeHeight { get; set; } = 100;

	/// <inheritdoc />
	public override string? NodeBGColor { get; set; } = "#FFFFFF";

	/// <inheritdoc />
	public override string? NodeBGColorHover { get; set; } = "#FFFFFF";

	/// <inheritdoc />
	public override int? BorderWidth { get; set; } = 1;

	/// <inheritdoc />
	public override string? BorderStyle { get; set; } = "solid";

	/// <inheritdoc />
	[JsonIgnore]
	public override int? BorderRadius { get; set; } = 5;

	/// <inheritdoc />
	public override string? BorderColor { get; set; } = "#BCBCBC";

	/// <inheritdoc />
	public override string? BorderColorHover { get; set; } = "#5C6BC0";

	/// <inheritdoc />
	public override string? EdgeColor { get; set; } = "#BCBCBC";

	/// <inheritdoc />
	public override string? EdgeColorHover { get; set; } = "#BCBCBC";

	/// <inheritdoc />
	public override string? TooltipId { get; set; } = "apextree-tooltip-container";

	/// <inheritdoc />
	public override string? TooltipBorderColor { get; set; } = "#BCBCBC";

	/// <inheritdoc />
	public override string? TooltipBGColor { get; set; } = "#FFFFFF";

	/// <inheritdoc />
	public override int? TooltipMaxWidth { get; set; } = 100;

	/// <summary>
	/// The minimum width of the tooltip in pixels.
	/// </summary>
	public int? TooltipMinWidth { get; set; }

	/// <summary>
	/// The hex font color of the tooltip.
	/// </summary>
	/// <remarks>
	/// Prefix with '#'.
	/// </remarks>
	public string? TooltipFontColor { get; set; }

	/// <summary>
	/// The font size of the tooltip in pixels.
	/// </summary>
	[JsonIgnore]
	public int? TooltipFontSize { get; set; }

	[JsonInclude]
	[JsonPropertyName("tooltipFontSize")]
	private string? SerializeTooltipFontSize { get => ChartSerializer.GetMeasurement(TooltipFontSize, TooltipFontSizeUnits, true); }

	/// <summary>
	/// The measurement type to use for the <see cref="TooltipFontSize"/>.
	/// </summary>
	[JsonIgnore]
	public LengthUnits TooltipFontSizeUnits { get; set; } = LengthUnits.Pixels;

	/// <summary>
	/// The padding of the tooltip in pixels.
	/// </summary>
	public int? TooltipPadding { get; set; }

	/// <summary>
	/// The offset distance between tooltip and cursor in pixels.
	/// </summary>
	public int? TooltipOffset { get; set; }

	/// <inheritdoc />
	[JsonIgnore]
	public override int? FontSize { get; set; } = 14;

	/// <inheritdoc />
	public override int? FontWeight { get; set; } = 400;

	/// <inheritdoc />
	public override string? FontColor { get; set; } = "#000000";

	/// <summary>
	/// Animate node expansion/collapse transitions. Default: <see langword="true"/>.
	/// </summary>
	public bool? EnableAnimation { get; set; }

	/// <summary>
	/// Internal SVG viewport width in pixels. Default: 800.
	/// </summary>
	public int? ViewPortWidth { get; set; }

	/// <summary>
	/// Internal SVG viewport height in pixels. Default: 600.
	/// </summary>
	public int? ViewPortHeight { get; set; }

	/// <summary>
	/// Show the collapse-count badge on collapsed nodes. Default: <see langword="true"/>.
	/// </summary>
	public bool? CollapseBadgeEnabled { get; set; }

	/// <summary>
	/// Minimum number of hidden children required before the badge appears. Default: 1.
	/// </summary>
	public int? CollapseBadgeThreshold { get; set; }

	/// <summary>
	/// Background color of the collapse-count badge.
	/// </summary>
	/// <remarks>
	/// Prefix with '#'. Default: '#5C6BC0'.
	/// </remarks>
	public string? CollapseBadgeBGColor { get; set; }

	/// <summary>
	/// Font color of the collapse-count badge.
	/// </summary>
	/// <remarks>
	/// Prefix with '#'. Default: '#FFFFFF'.
	/// </remarks>
	public string? CollapseBadgeFontColor { get; set; }

	/// <summary>
	/// Font size of the collapse-count badge in pixels. Default: 12.
	/// </summary>
	[JsonIgnore]
	public int? CollapseBadgeFontSize { get; set; }

	[JsonInclude]
	[JsonPropertyName("collapseBadgeFontSize")]
	private string? SerializeCollapseBadgeFontSize { get => ChartSerializer.GetMeasurement(CollapseBadgeFontSize, CollapseBadgeFontSizeUnits, true); }

	/// <summary>
	/// The measurement type to use for the <see cref="CollapseBadgeFontSize"/>.
	/// </summary>
	[JsonIgnore]
	public LengthUnits CollapseBadgeFontSizeUnits { get; set; } = LengthUnits.Pixels;

	/// <summary>
	/// Background color of the expand/collapse button.
	/// </summary>
	/// <remarks>
	/// Prefix with '#'. Default: '#FFFFFF'.
	/// </remarks>
	public string? ExpandCollapseButtonBGColor { get; set; }

	/// <summary>
	/// Border color of the expand/collapse button.
	/// </summary>
	/// <remarks>
	/// Prefix with '#'. Default: '#BCBCBC'.
	/// </remarks>
	public string? ExpandCollapseButtonBorderColor { get; set; }

	/// <summary>
	/// Built-in theme preset. <see cref="ApexTree.Theme.Light"/> uses the default soft-neutral palette,
	/// <see cref="ApexTree.Theme.Dark"/> a dark-mode palette, and <see cref="ApexTree.Theme.Custom"/>
	/// disables built-in CSS variable injection so host-page variables win. Default: Light.
	/// </summary>
	public Theme? Theme { get; set; }

	/// <summary>
	/// Show a search input in the toolbar area that filters nodes by label and lineage-highlights matches. Default: <see langword="false"/>.
	/// </summary>
	public bool? EnableSearch { get; set; }

	/// <summary>
	/// Show a breadcrumb trail above the chart that updates on node click; clicking a segment re-centers on that ancestor. Default: <see langword="false"/>.
	/// </summary>
	public bool? EnableBreadcrumb { get; set; }

	/// <summary>
	/// Re-fit the viewBox to the new tree bounds when a node is collapsed or expanded. Default: <see langword="true"/>.
	/// </summary>
	public bool? EnableExpandCollapseZoom { get; set; }

	/// <summary>
	/// Enable Ctrl/Cmd + mouse-wheel (or trackpad pinch) zoom and drag-to-pan on the canvas. Default: <see langword="true"/>.
	/// </summary>
	public bool? EnableZoomPan { get; set; }

	/// <summary>
	/// Node selection behaviour. Leave unset to disable selection (the default). Set to
	/// <see cref="TreeSelectionMode.Single"/> or <see cref="TreeSelectionMode.Multi"/> to enable it;
	/// listen for changes via the <c>OnSelectionChange</c> event.
	/// </summary>
	public TreeSelectionMode? EnableSelection { get; set; }

	/// <summary>
	/// Horizontal padding around the rendered tree in pixels. Default: 100.
	/// </summary>
	public int? PaddingX { get; set; }

	/// <summary>
	/// Vertical padding around the rendered tree in pixels. Default: 100.
	/// </summary>
	public int? PaddingY { get; set; }

	/// <summary>
	/// Shape of the connecting lines between nodes. Default: <see cref="ApexTree.EdgeStyle.Orthogonal"/>.
	/// </summary>
	public EdgeStyle? EdgeStyle { get; set; }

	/// <summary>
	/// How edge colors are resolved. Default: <see cref="ApexTree.EdgeColorMode.Default"/>.
	/// </summary>
	public EdgeColorMode? EdgeColorMode { get; set; }

	/// <summary>
	/// Localization and text-direction (RTL) options.
	/// </summary>
	public LocaleOptions? Locale { get; set; }

	/// <summary>
	/// WCAG 2.1 AA accessibility options for the tree chart.
	/// Controls ARIA role/label semantics and keyboard navigation.
	/// </summary>
	public A11yOptions? A11y { get; set; }
}
