using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SG.Models.MongoDb
{
    public class MongoDBSettings
    {

        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;

    }
}
