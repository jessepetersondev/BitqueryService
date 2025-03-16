using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BitqueryService.Models
{
    internal class GraphQLQuery
    {
        [JsonPropertyName("query")]
        public string Query { get; set; }

        [JsonPropertyName("variables")]
        public object Variables { get; set; } = new { };
    }
}
