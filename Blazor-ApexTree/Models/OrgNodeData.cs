using System.Text.Json.Serialization;

namespace ApexTree;

/// <summary>
/// Structured payload for the built-in org-chart card node template. Attach an instance as the
/// <see cref="DataNode{TItem}.Data"/> of each node (i.e. use <c>ApexTree&lt;OrgNodeData&gt;</c>). The
/// built-in template renders an org card when the data object has any of <see cref="ImageUrl"/>,
/// <see cref="Title"/>, <see cref="Subtitle"/>, <see cref="Badge"/>, <see cref="AccentColor"/>, or
/// <see cref="Meta"/>.
/// </summary>
public class OrgNodeData
{
	/// <summary>
	/// Primary display label (equivalent to the node name).
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// Second line, typically a job title. Medium size, lower contrast.
	/// </summary>
	public string? Title { get; set; }

	/// <summary>
	/// Third line, typically a department or team. Smaller, lower contrast.
	/// </summary>
	public string? Subtitle { get; set; }

	/// <summary>
	/// Avatar URL rendered as a circular image on the card.
	/// </summary>
	[JsonPropertyName("imageURL")]
	public string? ImageUrl { get; set; }

	/// <summary>
	/// Colored left stripe on the card, useful for categorising roles or departments. Any valid CSS color.
	/// </summary>
	public string? AccentColor { get; set; }

	/// <summary>
	/// Status chip shown in the upper-right corner of the card.
	/// </summary>
	public OrgNodeBadge? Badge { get; set; }

	/// <summary>
	/// Extra metadata rows rendered under the title/subtitle as icon + label lines.
	/// </summary>
	public List<OrgNodeMeta>? Meta { get; set; }
}

/// <summary>
/// Status chip for an <see cref="OrgNodeData"/> card.
/// </summary>
public class OrgNodeBadge
{
	/// <summary>
	/// Chip label.
	/// </summary>
	public string? Text { get; set; }

	/// <summary>
	/// Chip background color; defaults to a soft indigo.
	/// </summary>
	public string? Color { get; set; }
}

/// <summary>
/// A metadata row on an <see cref="OrgNodeData"/> card.
/// </summary>
public class OrgNodeMeta
{
	/// <summary>
	/// Optional CSS class for an icon font (e.g. "bi bi-person"). Omit for a plain text row.
	/// </summary>
	public string? Icon { get; set; }

	/// <summary>
	/// Row label text.
	/// </summary>
	public string? Label { get; set; }
}
