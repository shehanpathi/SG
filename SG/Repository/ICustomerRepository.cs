using SG_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SG.Repository
{
    public interface ICustomerRepository
    {
        public Task<IEnumerable<Customer>> GetCustomersByLastSyncId(int lastSyncId);
    }
}
