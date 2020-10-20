using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ccEntPerformance
{
    public class CommandMessage
    {

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("transaction_id")]
        public string TransactionId { get; set; }
    }
}
