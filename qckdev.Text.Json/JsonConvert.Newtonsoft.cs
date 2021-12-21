#if NEWTONSOFT

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace qckdev.Text.Json
{

    /// <summary>
    /// Provides methods for converting between .NET types and JSON types.
    /// </summary>
    public static partial class JsonConvert
    {

        static readonly JsonSerializerSettings jsettings = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        /// <summary>
        /// Converts the <paramref name="value"/> of a type specified by a generic type parameter into a JSON string.
        /// </summary>
        /// <typeparam name="TValue">The type of the <paramref name="value"/> to serialize.</typeparam>
        /// <param name="value">The value to convert.</param>
        /// <returns> A JSON string representation of the <paramref name="value"/>.</returns>
        public static string SerializeObject<TValue>(TValue value)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(value, jsettings);
        }

        /// <summary>
        /// Parses the text representing a single JSON value into an instance of the type <see cref="Object"/>.
        /// </summary>
        /// <param name="value">The JSON text to parse.</param>
        /// <returns>A <see cref="Object"/> representation of the JSON value.</returns>
        public static object DeserializeObject(string value)
        {
#if NO_DYNAMIC
            return Newtonsoft.Json.JsonConvert.DeserializeObject<object>(value, jsettings);
#else
            return Newtonsoft.Json.JsonConvert.DeserializeObject(value, jsettings);
#endif
        }

        /// <summary>
        /// Parses the text representing a single JSON value into an instance of the type specified by a generic type parameter.
        /// </summary>
        /// <typeparam name="TValue">The target type of the JSON <paramref name="value"/>.</typeparam>
        /// <param name="value">The JSON text to parse.</param>
        /// <returns>A <typeparamref name="TValue"/> representation of the JSON value.</returns>
        public static TValue DeserializeObject<TValue>(string value)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<TValue>(value, jsettings);
        }

    }
}

#else
#endif