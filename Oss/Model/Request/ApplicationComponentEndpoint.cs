
using System.Text.Json;
using System.Text.Json.Serialization;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Oss.Model.Request
{
    public class ApplicationComponentEndpoint
    {
        [BsonElement(elementName: "applicationComponentEndpointId")]
        [JsonPropertyName("applicationComponentEndpointId")]
        public string ApplicationComponentEndpointId { get; set; }

        [BsonElement(elementName: "fromApplicationComponentId")]
        [JsonPropertyName("fromApplicationComponentId")]
        public string FromApplicationComponentId { get; set; }

        [BsonElement(elementName: "toApplicationComponentId")]
        [JsonPropertyName("toApplicationComponentId")]
        public string ToApplicationComponentId { get; set; }

        [BsonElement(elementName: "type")]
        [JsonPropertyName("type")]
        public ApplicationComponentEndpointType Type { get; set; }

        [JsonExtensionData]
        [BsonIgnore]
        public Dictionary<string, JsonElement> Extra { get; set; }

        [JsonIgnore]
        [BsonExtraElements]
        public BsonDocument Extra2 { get; set; }
    }
}
