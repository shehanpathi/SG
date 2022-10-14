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

        private readonly IMongoCollection<DataSync> _dataSyncCollection;

        public DataSyncRepository(IMongoDbContext mongoDbContext)
        {
            _dataSyncCollection = mongoDbContext.getDatabase().GetCollection<DataSync>("LastDataSync");
        }

        public async Task<List<DataSync>> GetAsync()
        {
            return await _dataSyncCollection.Find(new BsonDocument()).ToListAsync();
        }
        public async Task CreateAsync(DataSync dataSync)
        {
            await _dataSyncCollection.InsertOneAsync(dataSync);
            return;
        }

        public async Task AddToPlaylistAsync(string id, string movieId)
        {
            FilterDefinition<DataSync> filter = Builders<DataSync>.Filter.Eq("Id", id);
            UpdateDefinition<DataSync> update = Builders<DataSync>.Update.AddToSet<string>("movieIds", movieId);
            await _dataSyncCollection.UpdateOneAsync(filter, update);
            return;
        }
    }

}


