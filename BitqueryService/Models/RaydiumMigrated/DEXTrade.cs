using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BitqueryService.Models.RaydiumMigrated
{
    public class DEXTrade
    {
        [JsonPropertyName("Block")]
        public BlockInfo Block { get; set; }

        [JsonPropertyName("Trade")]
        public TradeDetails Trade { get; set; }

        [JsonPropertyName("Transaction")]
        public TransactionInfo Transaction { get; set; }
    }
}
