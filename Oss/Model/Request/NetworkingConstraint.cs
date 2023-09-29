
using System.Text.Json;
using System.Text.Json.Serialization;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Oss.Model.Request
{
    public class NetworkingConstraint
    {
        [BsonElement(elementName: "applicationComponentId")]
        [JsonPropertyName("applicationComponentId")]
        public string ApplicationComponentId { get; set; }

        [BsonElement(elementName: "applicationComponentEndpointId")]
        [JsonPropertyName("applicationComponentEndpointId")]
        public string ApplicationComponentEndpointId { get; set; }

        [BsonElement(elementName: "additionalParams")]
        [JsonPropertyName("additionalParams")]
        public AdditionalParam[] AdditionalParams { get; set; }

        [BsonElement(elementName: "sliceId")]
        [JsonPropertyName("sliceId")]
        public string SliceId { get; set; }

        [JsonExtensionData]
        [BsonIgnore]
        public Dictionary<string, JsonElement> Extra { get; set; }

        [JsonIgnore]
        [BsonExtraElements]
        public BsonDocument Extra2 { get; set; }
    }
}
