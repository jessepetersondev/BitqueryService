using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BitqueryService.Models.RaydiumMigrated
{
    public class TransactionInfo
    {
        [JsonPropertyName("FeePayer")]
        public string FeePayer { get; set; }

        [JsonPropertyName("Signature")]
        public string Signature { get; set; }

        [JsonPropertyName("Signer")]
        public string Signer { get; set; }
    }
}
