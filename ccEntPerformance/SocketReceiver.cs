using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ccEntPerformance
{

    public class SocketReceiver
    {
        private HubConnection _connection;
        public void Subscribe(string _myAccessToken)
        {
            //Task.Delay(5000);
            _connection = new HubConnectionBuilder()
                .WithUrl("https://dev-mw-ccpeople-ent.fingent.net/notifications", options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(_myAccessToken);
                })
                .Build();
            _connection.ServerTimeout = TimeSpan.FromMinutes(5);
            _connection.Closed += async (error) =>
            {
                Console.WriteLine(error.Message);
                Console.WriteLine("Connection Closed");
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await _connection.StartAsync();
            };

            var ccEventName = "WorkPatternRegisteredEvent";
            _connection.On<string, SuccessMessage>(ccEventName, (user, message) =>
            {
                TransactionManager.RecordCompletion(message.TransactionId.ToString(), user, $"{ccEventName} at { message.RequestedDate.ToString()}");
            });


            _connection.On<string, ErrorMessage>("ErrorEvent", (user, message) =>
            {
                TransactionManager.RecordCompletion(message.TransactionId.ToString(), user, $"Error Message {message.Key}, {message.Value} at {message.RequestedDate}");

                //Console.WriteLine($"Transaction ID: {message.TransactionId} - User:{user} : Error Message {message.Key}, {message.Value} " + $"requested at {message.RequestedDate}");
            });

            //connection.On<string, string>("fundingGuideTimingUpdated", (user, message) =>
            //{
            //    Console.WriteLine($"Funding Guide Timing Updated by {user} at {message}");
            //});

            try
            {
                _connection.StartAsync();
                Console.WriteLine("Connection Started");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CloseConnection()
        {
            _connection.StopAsync();

        }

    }
}