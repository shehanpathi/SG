using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using SG.Models.MongoDb;
using System.Collections.Generic;
using System.Threading.Tasks;
using SG.Data;

namespace SG.Repository
{
    public class DataSyncRepository : IDataSyncRepository
    {
        private const string COLLECTION_NAME = "LastDataSync";
        private readonly IMongoCollection<DataSync> _dataSyncCollection;

        public DataSyncRepository(IMongoDbContext mongoDbContext)
        {
            _dataSyncCollection = mongoDbContext.GetDatabase().GetCollection<DataSync>(COLLECTION_NAME);
        }

        public async Task<int> GetLastSyncId()
        {
            var lastSyncData = await _dataSyncCollection.Find(new BsonDocument()).FirstOrDefaultAsync();
            return lastSyncData is null ? 0 : lastSyncData.lastSyncId;
        }
        public async Task CreateLastSyncData(DataSync dataSync)
        {

            await _dataSyncCollection.InsertOneAsync(dataSync);
            return;
        }

        public async Task DropLastSync()
        {
            await _dataSyncCollection.Database.DropCollectionAsync(COLLECTION_NAME);
            return;
        }

        
    }

}


