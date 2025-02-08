namespace ApexTree;

/// <summary>
/// Main class to configure options that are to be serialized and passed to the JavaScript.
/// </summary>
public class ApexTreeOptions
{
	/// <summary>
	/// The HTML id attribute for the chart.
	/// </summary>
	public string Id { get; set; } = Guid.NewGuid().ToHtmlId().ToString("N");
}
