
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Oss.Model.Request
{
    public class SliceIntent
    {
        [BsonElement(elementName: "name")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [BsonElement(elementName: "id")]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [BsonElement(elementName: "callbackUrl")]
        [JsonPropertyName("callbackUrl")]
        public string CallbackUrl { get; set; }

        [BsonElement(elementName: "applicationGraph")]
        [JsonPropertyName("applicationGraph")]
        public ApplicationGraph ApplicationGraph { get; set; }

        [BsonElement(elementName: "locationConstraints")]
        [JsonPropertyName("locationConstraints")]
        public ApplicationComponentLocationConstraint[] LocationConstraints { get; set; }

        [BsonElement(elementName: "computingConstraints")]
        [JsonPropertyName("computingConstraints")]
        public ComputingConstraint[] ComputingConstraints { get; set; }

        [BsonElement(elementName: "networkingConstraints")]
        [JsonPropertyName("networkingConstraints")]
        public NetworkingConstraint[] NetworkingConstraints { get; set; }

        [BsonElement(elementName: "sliceProfiles")]
        [JsonPropertyName("sliceProfiles")]
        public SliceProfile[] SliceProfiles { get; set; }

        [JsonExtensionData]
        [BsonIgnore]
        public Dictionary<string, JsonElement> Extra { get; set; }

        [JsonIgnore]
        [BsonExtraElements]
        public BsonDocument Extra2 { get; set; }
    }
}
