using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitqueryService.Models
{
    public class Buy
    {
        public Currency Currency { get; set; }
        public double Price { get; set; }
        public double PriceInUSD { get; set; }
    }
}
