using SG.Models.MongoDb;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SG.Repository
{
    public interface IDataSyncRepository
    {
        Task CreateLastSyncData(DataSync playlist);
        Task<int> GetLastSyncId();
        Task DropLastSync();
    }
}