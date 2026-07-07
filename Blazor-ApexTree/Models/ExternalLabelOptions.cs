namespace ApexTree;

/// <summary>
/// Options for rendering a node's label outside the node bounds, instead of via the in-node template.
/// </summary>
public class ExternalLabelOptions
{
	/// <summary>
	/// Render the node's label outside the node bounds. When <see langword="false"/> (default), the
	/// in-node <see cref="ApexTreeOptions.NodeTemplate"/> is used as before.
	/// </summary>
	public bool? Enabled { get; set; }

	/// <summary>
	/// Horizontal placement of the label relative to the node (default: <see cref="ExternalLabelAlign.Center"/>).
	/// </summary>
	public ExternalLabelAlign? Align { get; set; }

	/// <summary>
	/// Vertical placement of the label relative to the node (default: <see cref="ExternalLabelVerticalAlign.Middle"/>).
	/// </summary>
	public ExternalLabelVerticalAlign? VerticalAlign { get; set; }

	/// <summary>
	/// Override the global font color for the external label only.
	/// </summary>
	public string? FontColor { get; set; }

	/// <summary>
	/// Override the global font family for the external label only.
	/// </summary>
	public string? FontFamily { get; set; }

	/// <summary>
	/// Override the global font size for the external label only (e.g. "12px").
	/// </summary>
	public string? FontSize { get; set; }

	/// <summary>
	/// Override the global font weight for the external label only.
	/// </summary>
	public string? FontWeight { get; set; }

	/// <summary>
	/// Additional horizontal pixel offset applied after alignment (default: 0).
	/// </summary>
	public int? OffsetX { get; set; }

	/// <summary>
	/// Additional vertical pixel offset applied after vertical alignment (default: 0).
	/// </summary>
	public int? OffsetY { get; set; }

	/// <summary>
	/// Rotation in degrees applied around the label anchor. Use 90 for vertical leaf labels
	/// (top-to-bottom) and -90 for bottom-to-top text (default: 0).
	/// </summary>
	public int? Rotation { get; set; }
}
