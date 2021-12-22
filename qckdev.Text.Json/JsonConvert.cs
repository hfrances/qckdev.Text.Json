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
            switch (element.ValueKind)
            {
                case JsonValueKind.Array:
                    var list = new List<object>();
                    var etor = element.EnumerateArray();

                    while (etor.MoveNext())
                    {
                        list.Add(Parse(etor.Current));
                    }
                    return list.ToArray();

                case JsonValueKind.Object:
                    var rdo = new ExpandoObject();

                    foreach (var prop in element.EnumerateObject())
                    {
                        ((IDictionary<string, object>)rdo).Add(prop.Name, Parse(prop.Value));
                    }
                    return rdo;

                case JsonValueKind.Number:
                    var decimalValue = element.GetDecimal();

                    if (decimalValue % 1 == 0)
                    {
                        return (int)decimalValue;
                    }
                    else
                    {
                        return decimalValue;
                    }

                case JsonValueKind.True:
                case JsonValueKind.False:
                    return element.GetBoolean();

                case JsonValueKind.Null:
                    return null;

                case JsonValueKind.String:
                default:
                    return element.GetString();
            }
        }

    }
}

#endif