using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ccEntPerformance
{
    public class KeyCloakClientAuthentication
    {
        public static async Task<string> GetTokenAsync(string baseUrl, string realm,
            string clientId, string clientScret)
        {
            string accessToken = "";
            var restClient = new RestClient();
            restClient.BaseUrl = new Uri($"{baseUrl}/auth/realms/{realm}/protocol/openid-connect/");
            var request = new RestRequest("token", Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "client_credentials");
            //request.AddParameter("username", userName);
            request.AddParameter("client_secret", clientScret);
            request.AddParameter("client_id", clientId);

            var response = await restClient.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                var tokenResponse = JsonSerializer.Deserialize<AccessTokenResponse>(response.Content);
                accessToken = $"Bearer {tokenResponse.access_token}";
            }
            else
            {
                var errorMessage = JsonSerializer.Deserialize<ErrorResponse>(response.Content);
                throw new System.InvalidOperationException(errorMessage.ErrorMessage);
            }

            return accessToken;
        }

        public static async Task<string> GetUserTokenAsync(string baseUrl, string realm,
            string clientId, string username, string password)
        {
            string accessToken = "";
            var restClient = new RestClient();
            restClient.BaseUrl = new Uri($"{baseUrl}/auth/realms/{realm}/protocol/openid-connect/");
            var request = new RestRequest("token", Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "password");
            request.AddParameter("client_id", clientId);
            request.AddParameter("username", username);
            request.AddParameter("password", password);

            var response = await restClient.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                var tokenResponse = JsonSerializer.Deserialize<AccessTokenResponse>(response.Content);
                accessToken = $"{tokenResponse.access_token}";
            }
            else
            {
                var errorMessage = JsonSerializer.Deserialize<ErrorResponse>(response.Content);
                throw new System.InvalidOperationException(errorMessage.ErrorMessage);
            }

            return accessToken;
        }
    }
}
