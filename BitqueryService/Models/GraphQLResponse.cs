using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BitqueryService.Models
{
    public class GraphQLResponse<T>
    {
        [JsonPropertyName("data")]
        public T Data { get; set; }
    }
}
