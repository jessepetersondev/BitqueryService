using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BitqueryService.Models.RaydiumMigrated
{
    public class TradeSell
    {
        [JsonPropertyName("Account")]
        public Account Account { get; set; }

        [JsonPropertyName("Amount")]
        public string Amount { get; set; }

        [JsonPropertyName("Currency")]
        public CurrencyInfo Currency { get; set; }

        [JsonPropertyName("PriceAgaistBuyCurrency")]
        public decimal PriceAgaistBuyCurrency { get; set; }
    }
}
