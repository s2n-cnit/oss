
using Microsoft.Extensions.Options;

using MongoDB.Bson;
using MongoDB.Driver;

using Extensions;

namespace Oss.Services
{
    public class MongoDbConnectionOptions
    {
        public string ConnectionString { get; set; }
    }

    public class MongoDbService : IMongoDbService
    {
        public const string CollectionTrash = "Trash";

        private readonly MongoClient dbClient_;
        private readonly IMongoDatabase database_;

        public MongoDbService(IOptions<MongoDbConnectionOptions> options)
        {
            var url = new MongoUrl(options.Value.ConnectionString);

            dbClient_ = new MongoClient(url);
            database_ = dbClient_.GetDatabase(url.DatabaseName);
        }

        public IClientSessionHandle StartSession()
        {
            return dbClient_.StartSession();
        }

        public async Task<IClientSessionHandle> StartSessionAsync()
        {
            return await dbClient_.StartSessionAsync();
        }

        public void WriteDocument<TObject>(string collection, TObject document)
        {
            database_.GetCollection<TObject>(collection).InsertOne(document);
        }

        public async Task WriteDocumentAsync<TObject>(string collection, TObject document)
        {
            await database_.GetCollection<TObject>(collection).InsertOneAsync(document);
        }

        public void WriteDocuments<TObject>(string collection, IEnumerable<TObject> documents)
        {
            database_.GetCollection<TObject>(collection).InsertMany(documents);
        }

        public async Task WriteDocumentsAsync<TObject>(string collection, IEnumerable<TObject> documents)
        {
            await database_.GetCollection<TObject>(collection).InsertManyAsync(documents);
        }

        public IEnumerable<TObject> ReadDocuments<TObject>(string collection)
        {
            return database_.GetCollection<TObject>(collection).Find(Builders<TObject>.Filter.Empty).ToList();
        }

        public async Task<IAsyncCursor<TObject>> ReadDocumentsAsync<TObject>(string collection)
        {
            return await database_.GetCollection<TObject>(collection).FindAsync(Builders<TObject>.Filter.Empty);
        }

        public IEnumerable<TObject> ReadDocuments<TObject>(string collection, FilterDefinition<TObject> filter)
        {
            return database_.GetCollection<TObject>(collection).Find(filter).ToList();
        }

        public async Task<IAsyncCursor<TObject>> ReadDocumentsAsync<TObject>(string collection, FilterDefinition<TObject> filter)
        {
            return await database_.GetCollection<TObject>(collection).FindAsync(filter);
        }

        public long CountDocuments<TObject>(string collection, FilterDefinition<TObject> filter)
        {
            return database_.GetCollection<TObject>(collection).CountDocuments(filter);
        }

        public async Task<long> CountDocumentsAsync<TObject>(string collection, FilterDefinition<TObject> filter)
        {
            return await database_.GetCollection<TObject>(collection).CountDocumentsAsync(filter);
        }

        public TObject ReadDocument<TObject>(string collection, FilterDefinition<TObject> filter)
        {
            return database_.GetCollection<TObject>(collection).Find(filter).FirstOrDefault();
        }

        public UpdateResult UpdateDocument<TObject>(string collection, FilterDefinition<TObject> filter, UpdateDefinition<TObject> update)
        {
            return database_.GetCollection<TObject>(collection).UpdateOne(filter, update);
        }

        public async Task<UpdateResult> UpdateDocumentAsync<TObject>(string collection, FilterDefinition<TObject> filter, UpdateDefinition<TObject> update)
        {
            return await database_.GetCollection<TObject>(collection).UpdateOneAsync(filter, update);
        }

        public UpdateResult UpdateDocuments<TObject>(string collection, FilterDefinition<TObject> filter, UpdateDefinition<TObject> update)
        {
            return database_.GetCollection<TObject>(collection).UpdateMany(filter, update);
        }

        public async Task<UpdateResult> UpdateDocumentsAsync<TObject>(string collection, FilterDefinition<TObject> filter, UpdateDefinition<TObject> update)
        {
            return await database_.GetCollection<TObject>(collection).UpdateManyAsync(filter, update);
        }

        public ReplaceOneResult ReplaceDocument<TObject>(string collection, FilterDefinition<TObject> filter, TObject document)
        {
            return database_.GetCollection<TObject>(collection).ReplaceOne(filter, document);
        }

        public async Task<ReplaceOneResult> ReplaceDocumentAsync<TObject>(string collection, FilterDefinition<TObject> filter, TObject document)
        {
            return await database_.GetCollection<TObject>(collection).ReplaceOneAsync(filter, document);
        }

        public ReplaceOneResult ReplaceOrInsertDocument<TObject>(string collection, FilterDefinition<TObject> filter, TObject document)
        {
            return database_.GetCollection<TObject>(collection).ReplaceOne(filter, document, new ReplaceOptions { IsUpsert = true });
        }

        public async Task<ReplaceOneResult> ReplaceOrInsertDocumentAsync<TObject>(string collection, FilterDefinition<TObject> filter, TObject document)
        {
            return await database_.GetCollection<TObject>(collection).ReplaceOneAsync(filter, document, new ReplaceOptions { IsUpsert = true });
        }

        public DeleteResult DeleteDocument<TObject>(string collection, FilterDefinition<TObject> filter)
        {
            MoveToTrash(collection, filter);

            return database_.GetCollection<TObject>(collection).DeleteOne(filter);
        }

        public async Task<DeleteResult> DeleteDocumentAsync<TObject>(string collection, FilterDefinition<TObject> filter)
        {
            MoveToTrash(collection, filter);

            return await database_.GetCollection<TObject>(collection).DeleteOneAsync(filter);
        }

        private void MoveToTrash<TObject>(string collection, FilterDefinition<TObject> filter)
        {
            database_.GetCollection<TObject>(CollectionTrash).InsertOne(ReadDocument(collection, filter));
        }

        public DeleteResult DeleteDocuments<TObject>(string collection, FilterDefinition<TObject> filter)
        {
            var documents = ReadDocuments(collection, filter);

            if (!documents.IsEmpty())
            {
                database_.GetCollection<TObject>(CollectionTrash).InsertMany(documents);
            }

            return database_.GetCollection<TObject>(collection).DeleteMany(filter);
        }

        public async Task<DeleteResult> DeleteDocumentsAsync<TObject>(string collection, FilterDefinition<TObject> filter)
        {
            database_.GetCollection<TObject>(CollectionTrash).InsertMany(ReadDocuments(collection, filter));

            return await database_.GetCollection<TObject>(collection).DeleteManyAsync(filter);
        }

        public DeleteResult EmptyTrash()
        {
            return database_.GetCollection<BsonDocument>(CollectionTrash).DeleteMany(Builders<BsonDocument>.Filter.Empty);
        }
    }
}
