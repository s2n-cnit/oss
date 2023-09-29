
using System.Text.Json;
using System.Text.Json.Serialization;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Oss.Model.Request
{
    public class ComputingConstraint
    {
        [BsonElement(elementName: "applicationComponentId")]
        [JsonPropertyName("applicationComponentId")]
        public string ApplicationComponentId { get; set; }

        [BsonElement(elementName: "ram")]
        [JsonPropertyName("ram")]
        public string Ram { get; set; }

        [BsonElement(elementName: "cpu")]
        [JsonPropertyName("cpu")]
        public string Cpu { get; set; }

        [BsonElement(elementName: "storage")]
        [JsonPropertyName("storage")]
        public string Storage { get; set; }

        [JsonExtensionData]
        [BsonIgnore]
        public Dictionary<string, JsonElement> Extra { get; set; }

        [JsonIgnore]
        [BsonExtraElements]
        public BsonDocument Extra2 { get; set; }
    }
}
