﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BitqueryService.Models.RaydiumMigrated
{
    public class DexInfo
    {
        [JsonPropertyName("ProgramAddress")]
        public string ProgramAddress { get; set; }

        [JsonPropertyName("ProtocolFamily")]
        public string ProtocolFamily { get; set; }

        [JsonPropertyName("ProtocolName")]
        public string ProtocolName { get; set; }
    }
}
