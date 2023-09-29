
using System.Text.Json;
using System.Text.Json.Serialization;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Oss.Model.Response
{
    public class VasStatus
    {
        [JsonPropertyName("vasi")]
        [BsonElement(elementName: "vasi")]
        public string Vasi { get; set; }

        [JsonPropertyName("status")]
        [BsonElement(elementName: "status")]
        public Status Status { get; set; }

        [JsonPropertyName("message")]
        [BsonElement(elementName: "message")]
        public string Message { get; set; }

        [JsonExtensionData]
        [BsonIgnore]
        public Dictionary<string, JsonElement> Extra { get; set; }
    }
}
