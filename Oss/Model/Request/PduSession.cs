
using System.Text.Json;
using System.Text.Json.Serialization;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Oss.Model.Request
{
    public class PduSession
    {
        [BsonElement(elementName: "pduSessionId")]
        [JsonPropertyName("pduSessionId")]
        public string PduSessionId { get; set; }

        [BsonElement(elementName: "flows")]
        [JsonPropertyName("flows")]
        public Flow[] Flows { get; set; }

        [BsonElement(elementName: "pduSessionAmbr")]
        [JsonPropertyName("pduSessionAmbr")]
        public string PduSessionAmbr { get; set; }

        [JsonExtensionData]
        [BsonIgnore]
        public Dictionary<string, JsonElement> Extra { get; set; }

        [JsonIgnore]
        [BsonExtraElements]
        public BsonDocument Extra2 { get; set; }
    }
}
