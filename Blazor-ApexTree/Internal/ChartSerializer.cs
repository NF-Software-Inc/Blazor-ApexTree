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
}
