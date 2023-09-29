
using System.Text.Json.Serialization;

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Oss.Model
{
    public class Node
    {
        [BsonElement(elementName: "name")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [BsonElement(elementName: "labels")]
        [JsonPropertyName("labels")]
        public Dictionary<string, string> Labels { get; set; }
    }
}
