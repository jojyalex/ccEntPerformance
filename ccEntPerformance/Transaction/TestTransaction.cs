using System;
using System.Collections.Generic;

namespace ccEntPerformance
{
    public class TestTransaction
    {

        public TestTransaction()
        {
        }
        public bool Success { get; set; }

        public string Data { get; set; }

        public string TransactionId { get; set; }

        public string UserId { get; set; }


        public DateTime CommandResponseTime { get; set; }

        public DateTime EventResponseTime { get; set; }

        public bool ReceivedEvent { get; set; }

        public string Message { get; set; }

        public TimeSpan CommandEventDifference
        {
            get
            {
                return EventResponseTime.Subtract(CommandResponseTime);
            }
        }

    }
}