
using System.Text.Json;
using System.Text.Json.Serialization;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Oss.Model.Response
{
    public class ClusterInfo
    {
        [JsonPropertyName("namespace")]
        [BsonElement(elementName: "namespace")]
        public string Namespace { get; set; }

        [JsonPropertyName("certificate-authority-data")]
		[BsonElement(elementName: "certificateAuthorityData")]
        public string CertificateAuthorityData { get; set; }

        [JsonPropertyName("server")]
		[BsonElement(elementName: "server")]
        public string Server { get; set; }

        [JsonExtensionData]
        [BsonIgnore]
        public Dictionary<string, JsonElement> Extra { get; set; }
    }
}
