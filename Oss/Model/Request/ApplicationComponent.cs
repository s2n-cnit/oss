
using System.Text.Json;
using System.Text.Json.Serialization;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Oss.Model.Request
{
    public class ApplicationComponent
    {
        [BsonElement(elementName: "applicationNodeInstanceId")]
        [JsonPropertyName("applicationNodeInstanceId")]
        public string ApplicationNodeInstanceId { get; set; }

        [BsonElement(elementName: "applicationNodeInstanceName")]
        [JsonPropertyName("applicationNodeInstanceName")]
        public string ApplicationNodeInstanceName { get; set; }

        [JsonExtensionData]
        [BsonIgnore]
        public Dictionary<string, JsonElement> Extra { get; set; }

        [JsonIgnore]
        [BsonExtraElements]
        public BsonDocument Extra2 { get; set; }
    }
}
