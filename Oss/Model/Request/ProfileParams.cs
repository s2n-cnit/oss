
using System.Text.Json;
using System.Text.Json.Serialization;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Oss.Model.Request
{
    public class ProfileParams
    {
        [BsonElement(elementName: "isolationLevel")]
        [JsonPropertyName("isolationLevel")]
        public IsolationLevel IsolationLevel { get; set; }

        [BsonElement(elementName: "pduSessions")]
        [JsonPropertyName("pduSessions")]
        public PduSession[] PduSessions { get; set; }

        [BsonElement(elementName: "ueAmbr")]
        [JsonPropertyName("ueAmbr")]
        public string UeAmbr { get; set; }

        [BsonElement(elementName: "maximumNumberUE")]
        [JsonPropertyName("maximumNumberUE")]
        public string MaximumNumberUE { get; set; }

        [JsonExtensionData]
        [BsonIgnore]
        public Dictionary<string, JsonElement> Extra { get; set; }

        [JsonIgnore]
        [BsonExtraElements]
        public BsonDocument Extra2 { get; set; }
    }
}
