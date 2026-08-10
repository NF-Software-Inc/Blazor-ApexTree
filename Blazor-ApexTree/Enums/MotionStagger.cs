namespace ApexTree;

/// <summary>
/// How an expand is sequenced across the nodes it reveals. Requires ApexTree core 2.0.0 or later.
/// </summary>
public enum MotionStagger
{
	/// <summary>
	/// Radiate outward by graph distance from the toggled node, so a subtree unfolds as a wave
	/// (the default).
	/// </summary>
	Wave,

	/// <summary>
	/// Move every node at once.
	/// </summary>
	None
}