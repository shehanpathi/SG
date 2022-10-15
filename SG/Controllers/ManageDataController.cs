using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using SG.Repository;
using SG.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SG.Controllers
{
    [ApiController]
    public class ManageDataController : Controller
    {
        private readonly IManageDataService manageDataService;

        public ManageDataController(IManageDataService manageDataService)
        {
            this.manageDataService = manageDataService;
        }

        [HttpGet]
        [Route("api/customers")]
        public async Task<IActionResult> FetchCustomerRecords()
        {
            try
            {
                var list = await manageDataService.RetrieveCustomerData();
                return Ok(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
