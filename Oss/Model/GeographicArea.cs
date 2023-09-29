
using System.Text.Json.Serialization;

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Oss.Model
{
    public class GeographicArea
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement(elementName: "_id")]
        [JsonPropertyName("geographicalAreaId")]
        public string GeographicalAreaId { get; set; }

        [JsonIgnore]
        [BsonElement(elementName: "k8sNfvclUrlTemplate")]
        public string NfvclUrlTemplateForK8s { get; set; }

        [JsonIgnore]
        [BsonElement(elementName: "5gNfvclUrlTemplate")]
        public string NfvclUrlTemplateFor5G { get; set; }

        [JsonIgnore]
        [BsonElement(elementName: "labels")]
        public Dictionary<string, string> Labels { get; set; }

        [BsonElement(elementName: "locationName")]
        [JsonPropertyName("locationName")]
        public string LocationName { get; set; }

        [BsonElement(elementName: "latitude")]
        [JsonPropertyName("latitude")]
        public int Latitude { get; set; }

        [BsonElement(elementName: "longitude")]
        [JsonPropertyName("longitude")]
        public int Longitude { get; set; }

        [BsonElement(elementName: "coverageRadius")]
        [JsonPropertyName("coverageRadius")]
        public int CoverageRadius { get; set; }

        [BsonElement(elementName: "segment")]
        [JsonPropertyName("segment")]
        public string Segment { get; set; }

        [BsonElement(elementName: "cluster")]
        [JsonPropertyName("cluster")]
        public Cluster Cluster { get; set; }
    }
}
