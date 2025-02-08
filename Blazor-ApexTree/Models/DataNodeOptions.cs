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
	public string? NodeStyle { get; set; }

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
}
