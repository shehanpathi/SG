using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using SG.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SG.Controllers
{
    [ApiController]
    public class ManageDataController : Controller
    {
        private readonly ICustomerRepository customerRepository;

        public ManageDataController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        [HttpGet]
        [Route("api/customers")]
        public async Task<IActionResult> fetchCustomerRecords()
        {
            var list = await customerRepository.getCustomers();
            return Ok(list);
        }
    }
}
