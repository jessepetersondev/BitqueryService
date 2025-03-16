using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitqueryService.Models
{
    public class Currency
    {
        public int Decimals { get; set; }
        public bool Fungible { get; set; }
        public string MintAddress { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Uri { get; set; }
    }
}
