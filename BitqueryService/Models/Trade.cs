using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitqueryService.Models
{
    public class Trade
    {
        public Buy Buy { get; set; }
        public Sell Sell { get; set; }
    }
}
