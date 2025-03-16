using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitqueryService.Models
{
    public class _10KToken
    {
        public string TokenAddress { get; set; }
        public string Symbol { get; set; }
        public DateTime MigrationTime { get; set; }
        public string Uri { get; set; }
    }
}
