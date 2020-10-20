using System;
using System.Collections.Generic;
using System.Text;

namespace ccEntPerformance
{
    public class ErrorMessage
    {
        public DateTime Timestamp { get; set; }

        public Guid TransactionId { get; set; }

        public string InitiatedBy { get; set; }

        public DateTime RequestedDate { get; set; }

        public Guid EventId { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        public int Version { get; set; }
    }
}
