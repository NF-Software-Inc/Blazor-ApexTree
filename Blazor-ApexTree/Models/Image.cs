namespace ApexTree;

/// <summary>
/// Defines properties required for loading images in the chart.
/// </summary>
/// <param name="Name">The name to display with the image.</param>
/// <param name="Url">The URL to get the image or base64 string containing the image data.</param>
public record class Image(string Name, string Url);
