namespace ApexTree;

/// <summary>
/// Spring tuning for collapse/expand and camera motion. Requires ApexTree core 2.0.0 or later.
/// </summary>
public enum MotionSpring
{
	/// <summary>
	/// Tight and quick, with minimal overshoot (the default).
	/// </summary>
	Crisp,

	/// <summary>
	/// Softer and slower, with a longer settle.
	/// </summary>
	Gentle,

	/// <summary>
	/// Fastest, with a touch more overshoot.
	/// </summary>
	Snappy
}