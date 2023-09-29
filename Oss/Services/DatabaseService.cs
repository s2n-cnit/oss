
using MongoDB.Driver;

using Oss.Model;
using Oss.Model.Request;
using Oss.Model.Response;

namespace Oss.Services
{
    public class DatabaseService
    {
        internal const string CollectionGeographicAreas = "geographicAreas";
        internal const string CollectionVasInfos = "vasInfos";
        internal const string CollectionIntents = "intents";
        internal const string CollectionSetup = "setup";
        internal const string CollectionBlueprints = "blueprints";
        internal const string CollectionTest = "test";

        private readonly IMongoDbService db_;

        public DatabaseService(IMongoDbService db)
        {
            db_ = db;
        }

        public IEnumerable<VasInfo> GetTestObject(string id)
        {
            return db_.ReadDocuments(CollectionTest, Builders<VasInfo>.Filter.Eq(i => i.Id, id));
        }

        public void SaveTestObject(VasInfo intent)
        {
            db_.WriteDocument(CollectionTest, intent);
        }

        public IEnumerable<GeographicArea> GetGeographicAreas()
        {
            return db_.ReadDocuments<GeographicArea>(CollectionGeographicAreas);
        }

        public GeographicArea GetGeographicArea(string geographicalAreaId)
        {
            return db_.ReadDocument(CollectionGeographicAreas, Builders<GeographicArea>.Filter.Eq(i => i.GeographicalAreaId, geographicalAreaId));
        }

        public bool GeographicAreaExists(string geographicalAreaId)
        {
            return db_.ReadDocument(CollectionGeographicAreas, Builders<GeographicArea>.Filter.Eq(i => i.GeographicalAreaId, geographicalAreaId)) is not null;
        }

        public IEnumerable<VasInfo> GetVasInfos()
        {
            return db_.ReadDocuments<VasInfo>(CollectionVasInfos);
        }

        public VasInfo GetVasInfo(string id)
        {
            return db_.ReadDocument(CollectionVasInfos, Builders<VasInfo>.Filter.Eq(i => i.Id, id));
        }

        public VasInfo GetVasInfoFromSliceId(string id)
        {
            return db_.ReadDocument(CollectionVasInfos, Builders<VasInfo>.Filter.Eq(i => i.VasConfiguration.Id, id));
        }

        public void SaveVasInfo(VasInfo intent)
        {
            db_.WriteDocument(CollectionVasInfos, intent);
        }

        public ReplaceOneResult ReplaceVasInfo(VasInfo vasInfo)
        {
            return db_.ReplaceDocument(CollectionVasInfos, Builders<VasInfo>.Filter.Eq(i => i.Id, vasInfo.Id), vasInfo);
        }

        public DeleteResult DeleteVasInfo(string id)
        {
            return db_.DeleteDocument(CollectionVasInfos, Builders<VasInfo>.Filter.Eq(i => i.Id, id));
        }

        public Blueprint GetBlueprint(string geographicalAreaId)
        {
            return db_.ReadDocument(CollectionBlueprints, Builders<Blueprint>.Filter.Eq(i => i.GeographicalAreaId, geographicalAreaId));
        }
    }
}
