using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BitqueryService.Models.RaydiumMigrated
{
    public class TradeBuy
    {
        [JsonPropertyName("Account")]
        public Account Account { get; set; }

        // The API returns amounts as strings.
        [JsonPropertyName("Amount")]
        public string Amount { get; set; }

        [JsonPropertyName("Currency")]
        public CurrencyInfo Currency { get; set; }

        [JsonPropertyName("PriceAgaistSellCurrency")]
        public decimal PriceAgaistSellCurrency { get; set; }

        [JsonPropertyName("Uri")]
        public string Uri { get; set; }
    }
}
