
using System.Text.Json;
using System.Text.Json.Serialization;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Oss.Model.Request
{
    public class SliceProfile
    {
        [BsonElement(elementName: "sliceId")]
        [JsonPropertyName("sliceId")]
        public string SliceId { get; set; }

        [BsonElement(elementName: "sliceAmbr")]
        [JsonPropertyName("sliceAmbr")]
        public string SliceAmbr { get; set; }

        [BsonElement(elementName: "minimumGuaranteedBandwidth")]
        [JsonPropertyName("minimumGuaranteedBandwidth")]
        public string MinimumGuaranteedBandwidth { get; set; }

        [BsonElement(elementName: "enabledUEList")]
        [JsonPropertyName("enabledUEList")]
        public EnabledUe[] EnabledUEList { get; set; }

        [BsonElement(elementName: "locationConstraints")]
        [JsonPropertyName("locationConstraints")]
        public LocationConstraint[] LocationConstraints { get; set; }

        [BsonElement(elementName: "profileParams")]
        [JsonPropertyName("profileParams")]
        public ProfileParams ProfileParams { get; set; }

        [BsonElement(elementName: "sliceType")]
        [JsonPropertyName("sliceType")]
        public string SliceType { get; set; }

        [JsonExtensionData]
        [BsonIgnore]
        public Dictionary<string, JsonElement> Extra { get; set; }

        [JsonIgnore]
        [BsonExtraElements]
        public BsonDocument Extra2 { get; set; }
    }
}
