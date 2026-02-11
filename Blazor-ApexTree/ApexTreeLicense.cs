namespace ApexTree;

/// <summary>
/// Manages ApexTree license configuration.
/// </summary>
public static class ApexTreeLicense
{
    private static string? _licenseKey;

    /// <summary>
    /// Gets the current license key.
    /// </summary>
    public static string? LicenseKey => _licenseKey;

    /// <summary>
    /// Sets the ApexTree commercial license key.
    /// Call this method in Program.cs before rendering any trees.
    /// </summary>
    /// <param name="licenseKey">Your ApexTree commercial license key.</param>
    /// <example>
    /// <code>
    /// // in Program.cs
    /// ApexTreeLicense.SetLicense("your-license-key-here");
    /// </code>
    /// </example>
    /// <exception cref="ArgumentException">Thrown when license key is null or empty.</exception>
    public static void SetLicense(string licenseKey)
    {
        if (string.IsNullOrWhiteSpace(licenseKey))
        {
            throw new ArgumentException("License key cannot be null or empty", nameof(licenseKey));
        }

        _licenseKey = licenseKey;
    }

    /// <summary>
    /// Checks if a license key has been configured.
    /// </summary>
    public static bool HasLicense => !string.IsNullOrWhiteSpace(_licenseKey);
}
