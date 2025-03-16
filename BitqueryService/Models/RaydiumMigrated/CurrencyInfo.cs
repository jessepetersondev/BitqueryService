using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BitqueryService.Models.RaydiumMigrated
{
    public class CurrencyInfo
    {
        [JsonPropertyName("Decimals")]
        public int Decimals { get; set; }

        [JsonPropertyName("MintAddress")]
        public string MintAddress { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("ProgramAddress")]
        public string ProgramAddress { get; set; }

        [JsonPropertyName("Symbol")]
        public string Symbol { get; set; }
    }
}
