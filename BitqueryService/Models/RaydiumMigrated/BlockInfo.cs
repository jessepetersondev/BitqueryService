using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BitqueryService.Models.RaydiumMigrated
{
    public class BlockInfo
    {
        [JsonPropertyName("Height")]
        public string Height { get; set; }

        [JsonPropertyName("Time")]
        public string Time { get; set; }
    }
}
