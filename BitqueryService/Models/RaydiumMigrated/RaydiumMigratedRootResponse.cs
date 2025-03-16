using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BitqueryService.Models.RaydiumMigrated
{
    public class RaydiumMigratedRootResponse
    {
        [JsonPropertyName("data")]
        public Data Data { get; set; }
    }
}
