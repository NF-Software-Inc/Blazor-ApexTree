namespace ApexTree;

/// <summary>
/// WCAG 2.1 AA accessibility options for the tree chart.
/// </summary>
/// <remarks>
/// Controls ARIA role/label semantics on the SVG root and enables keyboard navigation.
/// Disable only when rendering inside a container that already provides its own accessibility layer.
/// </remarks>
public class A11yOptions
{
	/// <summary>
	/// Enable ARIA semantics and keyboard navigation. Default: <see langword="true"/>.
	/// </summary>
	public bool? Enabled { get; set; }

	/// <summary>
	/// Override the default <c>aria-label</c> ("Organizational chart") on the root SVG.
	/// </summary>
	public string? Label { get; set; }
}
