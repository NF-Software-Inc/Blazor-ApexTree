using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApexTree.Internal;

/// <summary>
/// Ensures that JS functions are serialized with the key '@eval' so they can be appropriately evaluated on the client side. Example:
///
/// <code>
/// myFunction: {
///     "@eval": "(content) => { return `&lt;div style='display: flex; justify-content: center; align-items: center; text-align: center; height: 100%;'&gt;${content}&lt;/div&gt;`; }"
/// }
/// </code>
/// </summary>
internal class FunctionStringConverter : JsonConverter<string>
{
	/// <inheritdoc />
	public override bool CanConvert(Type typeToConvert) => true;

	/// <inheritdoc />
	public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		throw new NotImplementedException();
	}

	/// <inheritdoc />
	public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("@eval");
		writer.WriteStringValue(value);
		writer.WriteEndObject();
	}
}
