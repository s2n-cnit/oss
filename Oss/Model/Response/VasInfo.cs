
using System.Text.Json;
using System.Text.Json.Serialization;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using Oss.Model.Request;

namespace Oss.Model.Response
{
    public class VasInfo
    {
        [BsonId]
        [JsonIgnore]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [JsonPropertyName("vasStatus")]
        [BsonElement(elementName: "vasStatus")]
        public VasStatus VasStatus { get; set; }

        [JsonPropertyName("vaQuotaInfo")]
        [BsonElement(elementName: "vaQuotaInfo")]
        public KubeConfig[] VaQuotaInfo { get; set; }

        [JsonPropertyName("vasConfiguration")]
        [BsonElement(elementName: "vasConfiguration")]
        public SliceIntent VasConfiguration { get; set; }

        [JsonExtensionData]
        [BsonIgnore]
        public Dictionary<string, JsonElement> Extra { get; set; }
    }
}
