using Dapper;
using MySqlConnector;
using SG.Data;
using SG_Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SG.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDapperContext _context;

        public CustomerRepository(IDapperContext dapperContext)
        {
            _context = dapperContext;
        }
        public async Task<IEnumerable<Customer>> getCustomers()
        {
            var customerList = new List<Customer>();
            try
            {
                
                var procedureName = "GET_ALL_CUSTOMERS";
                var parameters = new DynamicParameters();
                using (var connection = _context.CreateConnection())
                {
                    var company = await connection.QueryAsync<Customer>
                        (procedureName, parameters, commandType: CommandType.StoredProcedure);
                    return company;
                }
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Customer>();
            }


        }
    }
}
