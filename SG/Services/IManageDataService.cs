using SG_Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SG.Services
{
    public interface IManageDataService
    {
        Task<byte[]> RetrieveCustomerData();
    }
}