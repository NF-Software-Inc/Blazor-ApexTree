using System.Text.Json;

namespace ApexTree;

/// <summary>
/// Extension methods for use in Blazor components.
/// </summary>
public static class GeneralExtensions
{
	/// <summary>
	/// Returns a GUID that always starts with a letter to be used as an HTML element ID.
	/// </summary>
	/// <param name="value"></param>
	public static Guid ToHtmlId(this Guid value)
	{
		var valid = "ABCDEF";
		var guid = value.ToString("N");

		if (valid.Contains(guid[0]) == false)
			return Guid.Parse(valid[Random.Shared.Next(valid.Length)] + guid[1..]);
		else
			return value;
	}

	/// <summary>
	/// Clones the provided serializer options.
	/// </summary>
	/// <param name="value">The value to clone.</param>
	/// <param name="includeConverters">Specifies whether to include converters in the returned object.</param>
	public static JsonSerializerOptions CloneSerializerOptions(this JsonSerializerOptions value, bool includeConverters = true)
	{
		var serializerOptions = new JsonSerializerOptions()
		{
			AllowTrailingCommas = value.AllowTrailingCommas,
			DefaultBufferSize = value.DefaultBufferSize,
			DefaultIgnoreCondition = value.DefaultIgnoreCondition,
			DictionaryKeyPolicy = value.DictionaryKeyPolicy,
			Encoder = value.Encoder,
			IgnoreReadOnlyFields = value.IgnoreReadOnlyFields,
			IgnoreReadOnlyProperties = value.IgnoreReadOnlyProperties,
			IncludeFields = value.IncludeFields,
			MaxDepth = value.MaxDepth,
			NumberHandling = value.NumberHandling,
			PreferredObjectCreationHandling = value.PreferredObjectCreationHandling,
			PropertyNameCaseInsensitive = value.PropertyNameCaseInsensitive,
			PropertyNamingPolicy = value.PropertyNamingPolicy,
			ReadCommentHandling = value.ReadCommentHandling,
			ReferenceHandler = value.ReferenceHandler,
			TypeInfoResolver = value.TypeInfoResolver,
			UnknownTypeHandling = value.UnknownTypeHandling,
			UnmappedMemberHandling = value.UnmappedMemberHandling,
			WriteIndented = value.WriteIndented
		};

		if (includeConverters)
			foreach (var converter in value.Converters)
				serializerOptions.Converters.Add(converter);

		return serializerOptions;
	}
}
