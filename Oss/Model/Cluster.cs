
using System.Text.Json.Serialization;

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Oss.Model
{
    public class Cluster
    {
        [BsonElement(elementName: "name")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [BsonElement(elementName: "type")]
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [BsonElement(elementName: "nodes")]
        [JsonPropertyName("nodes")]
        public IEnumerable<Node> Nodes { get; set; }
    }
}
