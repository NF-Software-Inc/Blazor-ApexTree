namespace ApexTree;

/// <summary>
/// A listing of methods to measure elements on the screen.
/// </summary>
public enum LengthUnits
{
	/// <summary>
	/// No units are provided.
	/// </summary>
	Default,

	/// <summary>
	/// Measure in pixels.
	/// </summary>
	Pixels,

	/// <summary>
	/// Measure as a percentage of the parent element.
	/// </summary>
	Percent,

	/// <summary>
	/// Measure as a multiple of the current element.
	/// </summary>
	Element,

	/// <summary>
	/// Measure as a multiple of the root element.
	/// </summary>
	RootElement,

	/// <summary>
	/// Measure in points.
	/// </summary>
	Points,

	/// <summary>
	/// Measure as a percentage of the viewport.
	/// </summary>
	Viewport,

	/// <summary>
	/// Measure as a percentage of the smaller dimension of the viewport.
	/// </summary>
	ViewportMin,

	/// <summary>
	/// Measure as a percentage of the larger dimension of the viewport.
	/// </summary>
	ViewportMax,

	/// <summary>
	/// Measure in millimeters.
	/// </summary>
	Millimeters,

	/// <summary>
	/// Measure in centimeters.
	/// </summary>
	Centimeters,

	/// <summary>
	/// Measure in inches.
	/// </summary>
	Inches
}
