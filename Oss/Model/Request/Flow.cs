
using System.Text.Json;
using System.Text.Json.Serialization;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Oss.Model.Request
{
    public class Flow
    {
        [BsonElement(elementName: "flowId")]
        [JsonPropertyName("flowId")]
        public string FlowId { get; set; }

        [BsonElement(elementName: "gfbr")]
        [JsonPropertyName("gfbr")]
        public string Gfbr { get; set; }

        [BsonElement(elementName: "mfbr")]
        [JsonPropertyName("mfbr")]
        public string Mfbr { get; set; }

        [BsonElement(elementName: "5qi")]
        [JsonPropertyName("5qi")]
        public string FlowQos { get; set; }

        [JsonExtensionData]
        [BsonIgnore]
        public Dictionary<string, JsonElement> Extra { get; set; }

        [JsonIgnore]
        [BsonExtraElements]
        public BsonDocument Extra2 { get; set; }
    }
}
