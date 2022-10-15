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
        private const string PROCEDURE_NAME = "GET_ALL_CUSTOMERS";
        private readonly IDapperContext _context;

        public CustomerRepository(IDapperContext dapperContext)
        {
            _context = dapperContext;
        }
        public async Task<IEnumerable<Customer>> GetCustomersByLastSyncId(int lastSyncId)
        {
            var customerList = new List<Customer>();
            try
            {
                
                var parameters = new DynamicParameters();
                parameters.Add("lastSyncId", lastSyncId);
                using (var connection = _context.CreateConnection())
                {
                    var company = await connection.QueryAsync<Customer>
                        (PROCEDURE_NAME, parameters, commandType: CommandType.StoredProcedure);
                    return company;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Enumerable.Empty<Customer>();
            }


        }
    }
}
