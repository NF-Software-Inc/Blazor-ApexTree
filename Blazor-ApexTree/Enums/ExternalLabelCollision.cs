namespace ApexTree;

/// <summary>
/// How colliding external labels are resolved. Chiefly for crowded radial rings.
/// Requires ApexTree core 2.0.0 or later.
/// </summary>
public enum ExternalLabelCollision
{
	/// <summary>
	/// Draw every label, overlapping if necessary (the default).
	/// </summary>
	None,

	/// <summary>
	/// Cull labels that would collide, keeping as many as fit per ring.
	/// </summary>
	Hide,

	/// <summary>
	/// Only ever label leaf nodes, then cull collisions among them.
	/// </summary>
	Leaves
}