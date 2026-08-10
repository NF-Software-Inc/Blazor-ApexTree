namespace ApexTree;

/// <summary>
/// Tuning for the spring motion that drives collapse/expand, data updates and camera moves.
/// </summary>
/// <remarks>
/// Requires ApexTree core 2.0.0 or later. Motion is on by default; set
/// <see cref="ApexTreeOptions.EnableAnimation"/> to <see langword="false"/> to turn it off entirely,
/// or add the <c>apextree-reduced-motion</c> class to the container to honour a reduced-motion
/// preference.
/// </remarks>
public class MotionOptions
{
	/// <summary>
	/// Stiffness and damping preset. Default: <see cref="MotionSpring.Crisp"/>.
	/// </summary>
	public MotionSpring? Spring { get; set; }

	/// <summary>
	/// How an expand is sequenced across the nodes it reveals. Default:
	/// <see cref="MotionStagger.Wave"/>.
	/// </summary>
	public MotionStagger? Stagger { get; set; }
}
