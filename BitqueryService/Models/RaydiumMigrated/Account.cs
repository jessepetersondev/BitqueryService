using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BitqueryService.Models.RaydiumMigrated
{
    public class Account
    {
        [JsonPropertyName("Address")]
        public string Address { get; set; }
    }
}
