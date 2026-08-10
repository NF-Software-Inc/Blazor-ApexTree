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

	/// <summary>
	/// Chip row shown in the card summary, always visible. Requires ApexTree core 2.0.0 or later.
	/// </summary>
	public List<string>? Tags { get; set; }

	/// <summary>
	/// Key/value rows revealed only when the card is expanded. Requires ApexTree core 2.0.0 or later.
	/// </summary>
	/// <remarks>
	/// Supplying any of <see cref="Stats"/>, <see cref="Progress"/>, <see cref="Actions"/> or
	/// <see cref="Details"/> is what makes the built-in card grow an expand chevron. See
	/// <see cref="CardExpansionOptions"/>, and enable <see cref="ApexTreeOptions.AutoNodeHeight"/>
	/// so the card can grow to fit them.
	/// </remarks>
	public List<OrgNodeStat>? Stats { get; set; }

	/// <summary>
	/// A meter bar revealed when the card is expanded. Requires ApexTree core 2.0.0 or later.
	/// </summary>
	public OrgNodeProgress? Progress { get; set; }

	/// <summary>
	/// Action links revealed when the card is expanded. Requires ApexTree core 2.0.0 or later.
	/// </summary>
	public List<OrgNodeAction>? Actions { get; set; }

	/// <summary>
	/// Free-form extra text revealed when the card is expanded. Requires ApexTree core 2.0.0 or later.
	/// </summary>
	public string? Details { get; set; }
}

/// <summary>
/// A key/value row in the detail section of an <see cref="OrgNodeData"/> card.
/// </summary>
public class OrgNodeStat
{
	/// <summary>
	/// Row label, shown on the left.
	/// </summary>
	public string? Label { get; set; }

	/// <summary>
	/// Row value, shown on the right.
	/// </summary>
	public string? Value { get; set; }
}

/// <summary>
/// A meter bar in the detail section of an <see cref="OrgNodeData"/> card.
/// </summary>
public class OrgNodeProgress
{
	/// <summary>
	/// Fill amount, from 0 to 100.
	/// </summary>
	public double? Value { get; set; }

	/// <summary>
	/// Optional caption shown alongside the bar.
	/// </summary>
	public string? Label { get; set; }

	/// <summary>
	/// Fill color. Any valid CSS color; defaults to the card accent.
	/// </summary>
	public string? Color { get; set; }
}

/// <summary>
/// An action link in the detail section of an <see cref="OrgNodeData"/> card.
/// </summary>
public class OrgNodeAction
{
	/// <summary>
	/// Link text.
	/// </summary>
	public string? Label { get; set; }

	/// <summary>
	/// Optional target. Omit to render inert text rather than a link.
	/// </summary>
	public string? Href { get; set; }
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
