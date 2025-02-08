using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApexTree.Internal;

internal static class ChartSerializer
{
	internal static JsonSerializerOptions DefaultOptions
	{
		get
		{
			var options = JsonSerializerOptions.Default.CloneSerializerOptions();

			options.AllowTrailingCommas = false;
			options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
			options.PropertyNameCaseInsensitive = false;
			options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
			options.ReadCommentHandling = JsonCommentHandling.Disallow;
			options.WriteIndented = false;

			return options;
		}
	}

	internal static string? GetMeasurement(int? value, LengthUnits unit, bool width) => (value == null, unit) switch
	{
		(true, _) => null,
		(_, LengthUnits.Default) => value.ToString(),
		(_, LengthUnits.Pixels) => value.ToString() + "px",
		(_, LengthUnits.Percent) => value.ToString() + "%",
		(_, LengthUnits.Element) => value.ToString() + "em",
		(_, LengthUnits.RootElement) => value.ToString() + "rem",
		(_, LengthUnits.Points) => value.ToString() + "pt",
		(_, LengthUnits.Viewport) => width ? value.ToString() + "vw" : value.ToString() + "vh",
		(_, LengthUnits.ViewportMin) => value.ToString() + "vmin",
		(_, LengthUnits.ViewportMax) => value.ToString() + "vmax",
		(_, LengthUnits.Millimeters) => value.ToString() + "mm",
		(_, LengthUnits.Centimeters) => value.ToString() + "cm",
		(_, LengthUnits.Inches) => value.ToString() + "in",
		_ => throw new NotImplementedException(),
	};
}
