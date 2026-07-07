namespace ApexTree;

/// <summary>
/// Built-in theme presets for the tree.
/// </summary>
public enum Theme
{
	/// <summary>
	/// Default soft-neutral light palette.
	/// </summary>
	Light,

	/// <summary>
	/// Dark-mode palette with slate backgrounds.
	/// </summary>
	Dark,

	/// <summary>
	/// Disables the built-in CSS variable injection so host-page variables win cleanly.
	/// </summary>
	Custom
}
