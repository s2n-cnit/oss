
using MongoDB.Bson.Serialization;

namespace MongoDB.Bson
{
    public static class BsonExtensions
    {
        public static T As<T>(this BsonDocument document)
        {
            return document is not null ? BsonSerializer.Deserialize<T>(document) : default(T);
        }

        public static T As<T>(this BsonValue value)
        {
            return value is not null ? BsonSerializer.Deserialize<T>(value.AsBsonDocument) : default(T);
        }
    }
}