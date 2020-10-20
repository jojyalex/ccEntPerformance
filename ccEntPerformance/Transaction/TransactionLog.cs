using System;
using System.Collections.Generic;
using System.Text;

namespace ccEntPerformance
{
    public static class TransactionLog
    {
        public static void Log(DateTime date, string module, string transactionId, string userId, string message)
        {
            Console.WriteLine($"{date.ToString("hh:mm:ss.fff tt")}\tMODULE: {module}\tTRN_ID: {transactionId}\tUSR_ID: {userId}\tMSG: {message}");
        }

        public static void Log(string module, string transactionId, string userId, string message)
        {
            Log(DateTime.Now, module, transactionId, userId, message);
        }
    }
}
