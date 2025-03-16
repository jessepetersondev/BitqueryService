using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BitqueryService.Models.RaydiumMigrated
{
    public class TradeDetails
    {
        [JsonPropertyName("Buy")]
        public TradeBuy Buy { get; set; }

        [JsonPropertyName("Dex")]
        public DexInfo Dex { get; set; }

        [JsonPropertyName("Sell")]
        public TradeSell Sell { get; set; }
    }
}
