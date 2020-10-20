using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ccEntPerformance
{
    public static class TransactionManager
    {
        public static List<TestTransaction> Transactions; // Static List instance

        static TransactionManager()
        {
            //
            // Allocate the list.
            //
            Transactions = new List<TestTransaction>();
        }

        public static void Record(CommandMessage value)
        {
            //
            // Record this value in the list.
            //
            Transactions.Add(new TestTransaction { CommandResponseTime = DateTime.Now, TransactionId = value.TransactionId, Success = value.Success });
            TransactionLog.Log("COMMAND", value.TransactionId, "", value.Success.ToString());

        }


        public static void RecordCompletion(string transactionId, string userId, string message)
        {
            //
            // Record this value in the list.
            //
            var transaction = Transactions.FirstOrDefault(p => p.TransactionId == transactionId);
            if (transaction != null)
            {
                transaction.ReceivedEvent = true;
                transaction.EventResponseTime = DateTime.Now;
                transaction.Message = message;
                transaction.UserId = userId;
            }
            else
            {
                Console.WriteLine("Transaction Not Found!!!! - " + transactionId);
            }
            TransactionLog.Log("EVENT",transactionId, userId, message);

        }

        public static void Display()
        {
            //
            // Write out the results.
            //
            foreach (var value in Transactions)
            {
                Console.WriteLine(value);
            }
        }
    }
}
