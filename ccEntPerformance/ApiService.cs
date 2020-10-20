using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace ccEntPerformance
{
    public class ApiService
    {
        private string _token;
        public ApiService(string token)
        {
            _token = token;
        }

        public string GetAllAwards()
        {
            var client = new RestClient("https://dev-mw-ccpeople-ent.fingent.net/awards?IsPagingEnabled=true&Index=1&Size=10&SortBy=&SortDirection=&SearchBy=&Status=1");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", _token);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }


        public CommandMessage RegisterWorkPattern()
        {
            var client = new RestClient("https://dev-mw-ccpeople-ent.fingent.net/employees/work-patterns");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", _token);
            request.AddParameter("application/json", "{\n    \"name\": \"WP1\",\n    \"employmentType\": 1,\n    \"payrollName\": \"P1\",\n    \"expiryApplicable\": true\n}", ParameterType.RequestBody);
            var response = client.Execute<CommandMessage>(request);
            return response.Data;
        }


    }
}
