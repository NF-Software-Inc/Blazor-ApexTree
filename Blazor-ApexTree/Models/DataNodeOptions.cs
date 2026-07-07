using ApexTree.Internal;
using System.Text.Json.Serialization;

namespace ApexTree;

/// <summary>
/// Configures options specific to an individual data node.
/// </summary>
/// <remarks>
/// Links:
///
/// <see href="https://apexcharts.com/apextree/docs/options">JavaScript Documentation</see>.
/// </remarks>
public class DataNodeOptions
{
	/// <summary>
	/// The width of the graph node in pixels.
	/// </summary>
	public virtual int? NodeWidth { get; set; }

	/// <summary>
	/// The height of the graph node in pixels.
	/// </summary>
	public virtual int? NodeHeight { get; set; }

	/// <summary>
	/// The hex background color of the node.
	/// </summary>
	/// <remarks>
	/// Prefix with '#'.
	/// </remarks>
	public virtual string? NodeBGColor { get; set; }

	/// <summary>
	/// The hex background color on hover of the node.
	/// </summary>
	/// <remarks>
	/// Prefix with '#'.
	/// </remarks>
	public virtual string? NodeBGColorHover { get; set; }

	/// <summary>
	/// The CSS styles to apply to the node.
	/// </summary>
	public virtual string? NodeStyle { get; set; }

	/// <summary>
	/// The CSS classes to apply to the node.
	/// </summary>
	/// <remarks>
	/// Undocumented property from source TypeScript.
	/// </remarks>
	public virtual string? NodeClassName { get; set; }

	/// <summary>
	/// The border width of the node in pixels.
	/// </summary>
	public virtual int? BorderWidth { get; set; }

	/// <summary>
	/// The border style of the node.
	/// </summary>
	public virtual string? BorderStyle { get; set; }

	/// <summary>
	/// The border radius of the node in pixels.
	/// </summary>
	[JsonIgnore]
	public virtual int? BorderRadius { get; set; }

	[JsonInclude]
	[JsonPropertyName("borderRadius")]
	private string? SerializeBorderRadius { get => ChartSerializer.GetMeasurement(BorderRadius, BorderRadiusUnits, true); }

	/// <summary>
	/// The measurement type to use for the <see cref="BorderRadius"/>.
	/// </summary>
	[JsonIgnore]
	public LengthUnits BorderRadiusUnits { get; set; } = LengthUnits.Pixels;

	/// <summary>
	/// The hex border color of the node.
	/// </summary>
	/// <remarks>
	/// Prefix with '#'.
	/// </remarks>
	public virtual string? BorderColor { get; set; }

	/// <summary>
	/// The hex border color on hover of the node.
	/// </summary>
	/// <remarks>
	/// Prefix with '#'.
	/// </remarks>
	public virtual string? BorderColorHover { get; set; }

	/// <summary>
	/// The edge width of the node in pixels.
	/// </summary>
	/// <remarks>
	/// Undocumented property from source TypeScript.
	/// </remarks>
	public virtual int? EdgeWidth { get; set; }

	/// <summary>
	/// The hex color for the edge of the node.
	/// </summary>
	/// <remarks>
	/// Prefix with '#'.
	/// </remarks>
	public virtual string? EdgeColor { get; set; }

	/// <summary>
	/// The hex color for the edges when highlighted.
	/// </summary>
	/// <remarks>
	/// Prefix with '#'.
	/// </remarks>
	public virtual string? EdgeColorHover { get; set; }

	/// <summary>
	/// The tooltip HTML element id.
	/// </summary>
	public virtual string? TooltipId { get; set; }

	/// <summary>
	/// The hex border color of tooltip.
	/// </summary>
	/// <remarks>
	/// Prefix with '#'.
	/// </remarks>
	public virtual string? TooltipBorderColor { get; set; }

	/// <summary>
	/// The hex background color of tooltip.
	/// </summary>
	/// <remarks>
	/// Prefix with '#'.
	/// </remarks>
	public virtual string? TooltipBGColor { get; set; }

	/// <summary>
	/// The max width of the tooltip in pixels.
	/// </summary>
	public virtual int? TooltipMaxWidth { get; set; }

	/// <summary>
	/// The size of font of the node in pixels.
	/// </summary>
	[JsonIgnore]
	public virtual int? FontSize { get; set; }

	[JsonInclude]
	[JsonPropertyName("fontSize")]
	private string? SerializeFontSize { get => ChartSerializer.GetMeasurement(FontSize, FontSizeUnits, true); }

	/// <summary>
	/// The measurement type to use for the <see cref="FontSize"/>.
	/// </summary>
	[JsonIgnore]
	public LengthUnits FontSizeUnits { get; set; } = LengthUnits.Pixels;

	/// <summary>
	/// The font family of the node.
	/// </summary>
	public virtual string? FontFamily { get; set; }

	/// <summary>
	/// The font weight (thickness of the typeface) of the node.
	/// </summary>
	public virtual int? FontWeight { get; set; }

	/// <summary>
	/// The hex font color of the node.
	/// </summary>
	/// <remarks>
	/// Prefix with '#'.
	/// </remarks>
	public virtual string? FontColor { get; set; }

	/// <summary>
	/// Avatar placement for the built-in org-card node template. Default: <see cref="ApexTree.CardImagePosition.Left"/>.
	/// </summary>
	public virtual CardImagePosition? CardImagePosition { get; set; }

	/// <summary>
	/// Render the node's label outside the node bounds (with alignment, offset and rotation) instead of via the node template.
	/// </summary>
	public virtual ExternalLabelOptions? ExternalLabel { get; set; }

	/// <summary>
	/// Toggle expand/collapse when the node itself is clicked (not just the expand/collapse button). Default: <see langword="false"/>.
	/// </summary>
	public virtual bool? ExpandCollapseOnNodeClick { get; set; }

	/// <summary>
	/// CSS box-shadow applied to the node.
	/// </summary>
	public virtual string? NodeShadow { get; set; }

	/// <summary>
	/// CSS box-shadow applied to the node on hover.
	/// </summary>
	public virtual string? NodeShadowHover { get; set; }

	/// <summary>
	/// Pass-through dictionary for any core library options not yet exposed as typed properties.
	/// Each entry is serialized as a top-level JSON key and forwarded directly to the ApexTree
	/// JavaScript constructor, enabling forward-compatibility without requiring a new Blazor release.
	/// </summary>
	/// <remarks>
	/// Note: values serialized via this dictionary are not processed by <see cref="Internal.FunctionStringConverter"/>,
	/// so JavaScript function strings are not supported here. Use the typed <see cref="ApexTreeOptions.NodeTemplate"/>
	/// and <see cref="ApexTreeOptions.TooltipTemplate"/> properties for function-valued options.
	/// </remarks>
	[JsonExtensionData]
	public Dictionary<string, object?>? AdditionalOptions { get; set; }
}
