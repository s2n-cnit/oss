
using System.Text.Json;
using System.Text.Json.Serialization;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Oss.Model.Request
{
    public class AdditionalParam
    {
        [BsonElement(elementName: "pduSessionId")]
        [JsonPropertyName("pduSessionId")]
        public string PduSessionId { get; set; }

        [BsonElement(elementName: "flowId")]
        [JsonPropertyName("flowId")]
        public string FlowId { get; set; }

        [BsonElement(elementName: "enableInternetAccess")]
        [JsonPropertyName("enableInternetAccess")]
        public bool EnableInternetAccess { get; set; }

        [JsonExtensionData]
        [BsonIgnore]
        public Dictionary<string, JsonElement> Extra { get; set; }

        [JsonIgnore]
        [BsonExtraElements]
        public BsonDocument Extra2 { get; set; }
    }
}
