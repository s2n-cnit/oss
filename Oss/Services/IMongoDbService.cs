
using MongoDB.Driver;

namespace Oss.Services
{
    public interface IMongoDbService
    {
        long CountDocuments<TObject>(string collection, FilterDefinition<TObject> filter);
        Task<long> CountDocumentsAsync<TObject>(string collection, FilterDefinition<TObject> filter);
        DeleteResult DeleteDocument<TObject>(string collection, FilterDefinition<TObject> filter);
        Task<DeleteResult> DeleteDocumentAsync<TObject>(string collection, FilterDefinition<TObject> filter);
        DeleteResult DeleteDocuments<TObject>(string collection, FilterDefinition<TObject> filter);
        Task<DeleteResult> DeleteDocumentsAsync<TObject>(string collection, FilterDefinition<TObject> filter);
        DeleteResult EmptyTrash();
        TObject ReadDocument<TObject>(string collection, FilterDefinition<TObject> filter);
        IEnumerable<TObject> ReadDocuments<TObject>(string collection);
        IEnumerable<TObject> ReadDocuments<TObject>(string collection, FilterDefinition<TObject> filter);
        Task<IAsyncCursor<TObject>> ReadDocumentsAsync<TObject>(string collection);
        Task<IAsyncCursor<TObject>> ReadDocumentsAsync<TObject>(string collection, FilterDefinition<TObject> filter);
        ReplaceOneResult ReplaceDocument<TObject>(string collection, FilterDefinition<TObject> filter, TObject document);
        Task<ReplaceOneResult> ReplaceDocumentAsync<TObject>(string collection, FilterDefinition<TObject> filter, TObject document);
        ReplaceOneResult ReplaceOrInsertDocument<TObject>(string collection, FilterDefinition<TObject> filter, TObject document);
        Task<ReplaceOneResult> ReplaceOrInsertDocumentAsync<TObject>(string collection, FilterDefinition<TObject> filter, TObject document);
        IClientSessionHandle StartSession();
        Task<IClientSessionHandle> StartSessionAsync();
        UpdateResult UpdateDocument<TObject>(string collection, FilterDefinition<TObject> filter, UpdateDefinition<TObject> update);
        Task<UpdateResult> UpdateDocumentAsync<TObject>(string collection, FilterDefinition<TObject> filter, UpdateDefinition<TObject> update);
        UpdateResult UpdateDocuments<TObject>(string collection, FilterDefinition<TObject> filter, UpdateDefinition<TObject> update);
        Task<UpdateResult> UpdateDocumentsAsync<TObject>(string collection, FilterDefinition<TObject> filter, UpdateDefinition<TObject> update);
        void WriteDocument<TObject>(string collection, TObject document);
        Task WriteDocumentAsync<TObject>(string collection, TObject document);
        void WriteDocuments<TObject>(string collection, IEnumerable<TObject> documents);
        Task WriteDocumentsAsync<TObject>(string collection, IEnumerable<TObject> documents);
    }
}
