namespace ApexTree;

/// <summary>
/// Localization and text-direction (RTL) options for the tree.
/// </summary>
public class LocaleOptions
{
	/// <summary>
	/// Text and layout direction. "rtl" mirrors the tree horizontally and sets dir="rtl" on the
	/// container (node text and the search/breadcrumb chrome flow right-to-left); "auto" defers to
	/// the document/element direction. Accepts "ltr", "rtl", or "auto" (default: "ltr").
	/// </summary>
	public string? Direction { get; set; }

	/// <summary>
	/// Overrides for user-facing strings, keyed by message name (e.g. "searchPlaceholder",
	/// "rootAriaLabel"). Unset keys keep their English defaults.
	/// </summary>
	public Dictionary<string, string>? Messages { get; set; }
}
