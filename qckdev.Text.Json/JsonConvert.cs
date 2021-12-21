#if NEWTONSOFT
#else

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text.Json;

namespace qckdev.Text.Json
{

    /// <summary>
    /// Provides methods for converting between .NET types and JSON types.
    /// </summary>
    public static partial class JsonConvert
    {

        static readonly JsonSerializerOptions joptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        /// <summary>
        /// Converts the <paramref name="value"/> of a type specified by a generic type parameter into a JSON string.
        /// </summary>
        /// <typeparam name="TValue">The type of the <paramref name="value"/> to serialize.</typeparam>
        /// <param name="value">The value to convert.</param>
        /// <returns> A JSON string representation of the <paramref name="value"/>.</returns>
        /// <exception cref="System.NotSupportedException">
        /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"></see> for <typeparamref name="TValue"/> or its serializable members.
        /// </exception>
        public static string SerializeObject<TValue>(TValue value)
        {
            return JsonSerializer.Serialize(value, joptions);
        }

        /// <summary>
        /// Parses the text representing a single JSON value into an instance of the type <see cref="ExpandoObject"/>.
        /// </summary>
        /// <param name="value">The JSON text to parse.</param>
        /// <returns>A <see cref="ExpandoObject"/> representation of the JSON value.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// JSON is null.
        /// </exception>
        /// <exception cref="JsonException">
        /// The JSON is invalid. 
        /// -or- 
        /// <see cref="ExpandoObject"/> is not compatible with the JSON. 
        /// -or-
        /// There is remaining data in the string beyond a single JSON <paramref name="value"/>.
        ///</exception>
        /// <exception cref="System.NotSupportedException">
        /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"></see> for <see cref="ExpandoObject"/> or its serializable members.
        /// </exception>
        public static object DeserializeObject(string value)
        {
            using (var doc = JsonDocument.Parse(value))
            {
                return Parse(doc.RootElement);
            }
        }

        /// <summary>
        /// Parses the text representing a single JSON value into an instance of the type specified by a generic type parameter.
        /// </summary>
        /// <typeparam name="TValue">The target type of the JSON <paramref name="value"/>.</typeparam>
        /// <param name="value">The JSON text to parse.</param>
        /// <returns>A <typeparamref name="TValue"/> representation of the JSON value.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// JSON is null.
        /// </exception>
        /// <exception cref="JsonException">
        /// The JSON is invalid. 
        /// -or- 
        /// <typeparamref name="TValue"/> is not compatible with the JSON. 
        /// -or-
        /// There is remaining data in the string beyond a single JSON <paramref name="value"/>.
        ///</exception>
        /// <exception cref="System.NotSupportedException">
        /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"></see> for <typeparamref name="TValue"/> or its serializable members.
        /// </exception>
        public static TValue DeserializeObject<TValue>(string value)
        {
            if (typeof(TValue) == typeof(object) || typeof(TValue) == typeof(ExpandoObject))
            {
                return (TValue)DeserializeObject(value);
            }
            else
            {
                return JsonSerializer.Deserialize<TValue>(value, joptions);
            }
        }


        private static object Parse(JsonElement element)
        {
            if (element.ValueKind == JsonValueKind.Array)
            {
                var rdo = new List<object>();
                var etor = element.EnumerateArray();

                while (etor.MoveNext())
                {
                    rdo.Add(Parse(etor.Current));
                }
                return rdo;
            }
            else
            {
                var rdo = new ExpandoObject();

                foreach (var prop in element.EnumerateObject())
                {
                    var name = prop.Name;
                    var elto = prop.Value;

                    switch (elto.ValueKind)
                    {
                        case JsonValueKind.Number:
                            var decimalValue = elto.GetDecimal();
                            var intValue = (int)decimalValue;

                            if (intValue == decimalValue)
                            {
                                ((IDictionary<string, object>)rdo).Add(name, intValue);
                            }
                            else
                            {
                                ((IDictionary<string, object>)rdo).Add(name, decimalValue);
                            }
                            break;
                        case JsonValueKind.String:
                            ((IDictionary<string, object>)rdo).Add(name, elto.GetString());
                            break;
                        case JsonValueKind.True:
                        case JsonValueKind.False:
                            ((IDictionary<string, object>)rdo).Add(name, elto.GetBoolean());
                            break;
                        case JsonValueKind.Object:
                        case JsonValueKind.Array:
                            ((IDictionary<string, object>)rdo).Add(name, Parse(elto));
                            break;
                        default:
                            ((IDictionary<string, object>)rdo).Add(name, elto.GetString());
                            break;
                    }
                }
                return rdo;
            }
        }

    }
}

#endif