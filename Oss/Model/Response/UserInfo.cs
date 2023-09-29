
using System.Text.Json;
using System.Text.Json.Serialization;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Oss.Model.Response
{
    public class UserInfo
    {
        [JsonPropertyName("token")]
		[BsonElement(elementName: "token")]
        public string Token { get; set; }

        [JsonExtensionData]
        [BsonIgnore]
        public Dictionary<string, JsonElement> Extra { get; set; }
    }
}
