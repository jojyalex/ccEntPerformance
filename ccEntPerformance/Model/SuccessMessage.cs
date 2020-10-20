using System;
using System.Collections.Generic;
using System.Text;

namespace ccEntPerformance
{
    public class SuccessMessage
    {
        public DateTime Timestamp { get; set; }

        public Guid TransactionId { get; set; }

        public string InitiatedBy { get; set; }

        public DateTime RequestedDate { get; set; }

        public Guid EventId { get; set; }
    }
}
