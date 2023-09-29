
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Oss.Model
{
    public class Blueprint
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement(elementName: "_id")]
        public string Id { get; set; }

        //[BsonRepresentation(BsonType.ObjectId)]
        [BsonElement(elementName: "geographicalAreaId")]
        public string GeographicalAreaId { get; set; }

        [BsonElement(elementName: "free5GcBlueId")]
        public string Free5GcBlueId { get; set; }

        [BsonElement(elementName: "k8sClusterId")]
        public string K8sClusterId { get; set; }
    }
}
