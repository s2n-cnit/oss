
using System.Text.Json.Serialization;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Oss.Model.Request
{
    public class LocationConstraint
    {
        [BsonElement(elementName: "geographicalAreaId")]
        [JsonPropertyName("geographicalAreaId")]
        public string GeographicalAreaId { get; set; }

        [JsonExtensionData]
        [BsonIgnore]
        public Dictionary<string, object> Extra { get; set; }

        [JsonIgnore]
        [BsonExtraElements]
        public BsonDocument Extra2 { get; set; }
    }
}
