using SG.Models.MongoDb;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SG.Repository
{
    public interface IDataSyncRepository
    {
        Task AddToPlaylistAsync(string id, string movieId);
        Task CreateAsync(DataSync playlist);
        Task<List<DataSync>> GetAsync();
    }
}