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

	/// <inheritdoc />
	[JsonIgnore]
	public override int? FontSize { get; set; } = 14;

	/// <inheritdoc />
	public override int? FontWeight { get; set; } = 400;

	/// <inheritdoc />
	public override string? FontColor { get; set; } = "#000000";
}
