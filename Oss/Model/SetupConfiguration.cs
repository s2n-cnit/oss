
using System.Text.Json.Serialization;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Oss.Model
{
    public enum SetupStatus
    {
        InProgress,
        Completed,
        Error
    }

    public class SetupConfiguration
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("_id")]
        public string Id { get; set; }

        [JsonPropertyName("zoneId")]
        [BsonElement(elementName: "zoneId")]
        public string ZoneId { get; set; }

        [JsonPropertyName("vimName")]
        [BsonElement(elementName: "vimName")]
        public string VimName { get; set; }

        [JsonPropertyName("plmn")]
        [BsonElement(elementName: "plmn")]
        public string Plmn { get; set; }

        [JsonPropertyName("k8sId")]
        [BsonElement(elementName: "k8sId")]
        public string K8sId { get; set; }

        [JsonPropertyName("coreType")]
        [BsonElement(elementName: "coreType")]
        public string CoreType { get; set; }

        [JsonPropertyName("coreId")]
        [BsonElement(elementName: "coreId")]
        public string CoreId { get; set; }

        [JsonPropertyName("status")]
        [BsonElement(elementName: "status")]
        [BsonRepresentation(BsonType.String)]
        public SetupStatus Status { get; set; }
    }
}
