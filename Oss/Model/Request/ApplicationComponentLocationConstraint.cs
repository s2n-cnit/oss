
using System.Text.Json;
using System.Text.Json.Serialization;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Oss.Model.Request
{
    public class ApplicationComponentLocationConstraint
    {
        [BsonElement(elementName: "applicationComponentId")]
        [JsonPropertyName("applicationComponentId")]
        public string ApplicationComponentId { get; set; }

        [BsonElement(elementName: "geographicalAreaId")]
        [JsonPropertyName("geographicalAreaId")]
        public string GeographicalAreaId { get; set; }

        [JsonExtensionData]
        [BsonIgnore]
        public Dictionary<string, JsonElement> Extra { get; set; }

        [JsonIgnore]
        [BsonExtraElements]
        public BsonDocument Extra2 { get; set; }
    }
}
