
using System.Text.Json;
using System.Text.Json.Serialization;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Oss.Model.Request
{
    public class ApplicationGraph
    {
        [BsonElement(elementName: "applicationComponents")]
        [JsonPropertyName("applicationComponents")]
        public ApplicationComponent[] ApplicationComponents { get; set; }

        [BsonElement(elementName: "applicationComponentEndpoints")]
        [JsonPropertyName("applicationComponentEndpoints")]
        public ApplicationComponentEndpoint[] ApplicationComponentEndpoints { get; set; }

        [JsonExtensionData]
        [BsonIgnore]
        public Dictionary<string, JsonElement> Extra { get; set; }

        [JsonIgnore]
        [BsonExtraElements]
        public BsonDocument Extra2 { get; set; }
    }
}
