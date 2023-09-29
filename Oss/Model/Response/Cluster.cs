
using System.Text.Json;
using System.Text.Json.Serialization;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Oss.Model.Response
{
    public class Cluster
    {
        [JsonPropertyName("cluster")]
		[BsonElement(elementName: "cluster")]
        public ClusterInfo ClusterInfo { get; set; }

        [JsonExtensionData]
        [BsonIgnore]
        public Dictionary<string, JsonElement> Extra { get; set; }
    }
}
