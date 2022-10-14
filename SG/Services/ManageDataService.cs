using SG.Models.MongoDb;
using SG.Repository;
using SG_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SG.Services
{
    public class ManageDataService : IManageDataService
    {
        private readonly IDataSyncRepository dataSyncRepository;
        private readonly ICustomerRepository customerRepository;

        public ManageDataService(IDataSyncRepository dataSyncRepository, ICustomerRepository customerRepository)
        {
            this.dataSyncRepository = dataSyncRepository;
            this.customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Customer>> retrieveData()
        {
            var customerList = await customerRepository.getCustomers();
            var lastCustomerId = customerList.LastOrDefault().customerid.ToString();
            await dataSyncRepository.CreateAsync(new DataSync() { lastSyncId = lastCustomerId });
            return customerList;

        }

    }
}
