using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BitqueryService.Models.RaydiumMigrated
{
    public class Data
    {
        [JsonPropertyName("Solana")]
        public SolanaResponse Solana { get; set; }
    }
}
