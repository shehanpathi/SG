using MongoDB.Driver;
using SG.Models.MongoDb;

namespace SG.Data
{
    public interface IMongoDbContext
    {
        IMongoDatabase GetDatabase();
    }
}