
using System.Text.Json;
using System.Text.Json.Serialization;

using MongoDB.Bson.Serialization.Attributes;

namespace Oss.Model
{
    public class ErrorMsg
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonExtensionData]
        [BsonIgnore]
        public Dictionary<string, JsonElement> Extra { get; set; }
    }
}
