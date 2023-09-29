
using System.Text.Json;
using System.Text.Json.Serialization;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Oss.Model.Response
{
    public class Context
    {
        [JsonPropertyName("context")]
		[BsonElement(elementName: "context")]
        public ContextInfo ContextInfo { get; set; }

        [JsonExtensionData]
        [BsonIgnore]
        public Dictionary<string, JsonElement> Extra { get; set; }
    }
}
