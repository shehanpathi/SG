﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> fetchCustomerRecords()
        {
            var list = await manageDataService.retrieveData();
            return Ok(list);
        }
    }
}
