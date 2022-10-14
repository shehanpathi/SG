using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SG.Models.MongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SG.Data
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IOptions<MongoDBSettings> mongoDBSettings;

        public MongoDbContext(IOptions<MongoDBSettings> mongoDBSettings)
        {
            
            this.mongoDBSettings = mongoDBSettings;
        }

        public IMongoDatabase getDatabase()
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionString);
            return client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        }
    }
}
