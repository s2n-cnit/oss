
using System.Text.Json;
using System.Text.Json.Serialization;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Oss.Model.Response
{
    public class User
    {
        [JsonPropertyName("user")]
		[BsonElement(elementName: "user")]
        public UserInfo UserInfo { get; set; }

        [JsonExtensionData]
        [BsonIgnore]
        public Dictionary<string, JsonElement> Extra { get; set; }
    }
}
