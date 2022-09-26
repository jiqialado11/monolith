using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SubContractors.Common.Mvc.Converters
{
    public class NullableIntConverter : JsonConverter<int?>
    {
        public override bool HandleNull => true;

        public override int? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var stringValue = reader.GetString();
                if (int.TryParse(stringValue, out var value))
                {
                    if (stringValue[0] == '0')
                    {
                        return null;
                    }

                    return value;
                }

            }
            else if (reader.TokenType == JsonTokenType.Number)
            {
                if (reader.TryGetInt32(out var value))
                {
                    return value;
                }

                return null;
            }

            return null;
        }

        public override void Write(Utf8JsonWriter writer, int? value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteStringValue(string.Empty);
            }
            else
            {
                writer.WriteNumberValue(value.Value);
            }
        }
    }
}
