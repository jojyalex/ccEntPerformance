using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ccEntPerformance
{
    class Program
    {
        private const string BaseUrl = "https://dev-accounts-ccpeople-ent.fingent.net";
        private const string Realm = "master";
        private const string ClientId = "enterprise-ccpeople";
        private const string ClientSecret = "04221812-bc58-43a9-8d7f-f4e1a1b5ecc2";

        static void Main(string[] args)
        {
            int testUsersCount = 500;
            Console.WriteLine("cc:People Performance Tester");

            var userService = new UserService();
            userService.InitializeKeyCloak(BaseUrl, Realm, ClientId, ClientSecret);

            var users = CreateUsersAndLogin(true, testUsersCount, userService);
            ConcurrentSingleTransaction(users);
            foreach (var transaction in TransactionManager.Transactions)
            {
                TransactionLog.Log("TRANSACTION", transaction.TransactionId, transaction.UserId, $"Event: {transaction.ReceivedEvent} with Time Difference: {transaction.CommandEventDifference.TotalMilliseconds} ms");
            }
            Console.WriteLine($" Succeeded: {TransactionManager.Transactions.Count(p => p.ReceivedEvent)}/{TransactionManager.Transactions.Count}");
            DeleteTestUsers(userService);

            //userService.DeleteUserAsync(getUser.)
            Console.ReadLine();
        }

        private static void ConcurrentSingleTransaction(List<TestUser> users)
        {
            foreach (var p in users)
            {
                Task.Delay(1000);
                SocketReceiver.Subscribe(p.Token);
                //Task.Delay(5000);

            }
            //Task.Delay(10000);
            Console.WriteLine("Subscription Completed!!!");
            //Parallel.ForEach(users, p =>
            //{
            foreach (var p in users)
            {
                var apiService = new ApiService(p.AuthToken);
                //Console.WriteLine(apiService.GetAllAwards());
                var response = apiService.RegisterWorkPattern();
                if (response != null)
                    TransactionManager.Record(response);
            }
            //});
            //Task.Delay(5000);
        }

        private static void ConcurrentMultipleTransactionPerSocket(List<TestUser> users)
        {
            foreach (var p in users)
            {
                Task.Delay(1000);
                SocketReceiver.Subscribe(p.Token);
                //Task.Delay(5000);

            }
            Console.WriteLine("Subscription Completed!!!");
            foreach (var p in users)
            {
                var apiService = new ApiService(p.AuthToken);
                var response = apiService.RegisterWorkPattern();
                if (response != null)
                    TransactionManager.Record(response);
            }
            //});
            //Task.Delay(5000);
        }


        private static List<TestUser> CreateUsersAndLogin(bool create, int testUsersCount, UserService userService)
        {
            var testUsers = new List<TestUser>();
            var users = new List<User>();


            if (create)
                Console.WriteLine($"Creating {testUsersCount} users!!!");
            for (int i = 1; i <= testUsersCount; i++)
            {
                users.Add(CreateUserModel(i));
            }

            Parallel.ForEach(users, user =>
            {

                if (create)
                {
                    var created = userService.CreateUserAsync(user).Result;
                    if (created)
                    {
                    }
                }
                var testUser = new TestUser { Email = user.Email, FirstName = user.FirstName, LastName = user.LastName, TempCredentials = user.TempCredentials, UserId = user.UserId };
                testUser.Token = userService.LoginUserAsync(user).Result;
                Console.WriteLine($"Logging in {user.UserName}. Token : {testUser.Token.Substring(testUser.Token.Length - 10)}!!!");

                testUsers.Add(testUser);
            });

            return testUsers;
        }


        private static void DeleteTestUsers(UserService userService)
        {

            var getAllPerformanceUsers = userService.GetUsersAsync("performer", "", "", 0, 100).Result;
            Console.WriteLine($"Deleting {getAllPerformanceUsers.Count} users!!!");
            getAllPerformanceUsers.ForEach(p =>
            {
                var delResponse = userService.DeleteUserAsync(p.UserId.ToString()).Result;
            });
            Console.WriteLine($"Deleting users completed!!!");

        }

        private static User CreateUserModel(int varUser)
        {
            User user = new User(
                             "performer" + varUser,
                             true,
                             false,
                             "firstName" + varUser,
                             "lastName" + varUser,
                             "name" + varUser + "@gmail.com",
                             new Credential
                             {
                                 Type = "password",
                                 IsTemperory = false,
                                 Value = "changeme123"
                             },
                            null, null

                            );

            return user;
        }


    }
}
