
using System.Text.Json;
using System.Text.Json.Serialization;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Oss.Model.Response
{
    public class KubeConfig
    {
        [JsonPropertyName("clusters")]
		[BsonElement(elementName: "clusters")]
        public Cluster[] Clusters { get; set; }

        [JsonPropertyName("contexts")]
		[BsonElement(elementName: "contexts")]
        public Context[] Contexts { get; set; }

        [JsonPropertyName("users")]
		[BsonElement(elementName: "users")]
        public User[] Users { get; set; }

        [JsonExtensionData]
        [BsonIgnore]
        public Dictionary<string, JsonElement> Extra { get; set; }
    }
}
