using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ccEntPerformance
{
    /// <summary>
    /// User Service KeyCloak.
    /// </summary>
    /// <seealso cref="IUserService" />
    public class UserService 
    {
        /// <summary>
        /// The authentication header.
        /// </summary>
        private const string authHeader = "authorization";

        /// <summary>
        /// The user id.
        /// </summary>
        private const string _userId = "id";

        /// <summary>
        /// The user name.
        /// </summary>
        private const string _userName = "username";

        /// <summary>
        /// The page index.
        /// </summary>
        private const string _first = "first";

        /// <summary>
        /// The first name.
        /// </summary>
        private const string _firstName = "firstName";

        /// <summary>
        /// The last name.
        /// </summary>
        private const string _lastName = "lastName";

        /// <summary>
        /// The page size.
        /// </summary>
        private const string _max = "max";

        /// <summary>
        /// The base url.
        /// </summary>
        private string BaseUrl;

        /// <summary>
        /// The client id.
        /// </summary>
        private string ClientId;

        /// <summary>
        /// The client id.
        /// </summary>
        private string ClientSecret;

        /// <summary>
        /// The realm.
        /// </summary>
        private string Realm;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        public UserService()
        {
        }

        /// <summary>
        /// The initialize key cloak.
        /// </summary>
        /// <param name="baseUrl">The base url.</param>
        /// <param name="realms">The realms.</param>
        /// <param name="clientId">The client id.</param>
        /// <param name="userName">The user name.</param>
        /// <param name="password">The password.</param>
        public void InitializeKeyCloak(string baseUrl, string realms, string clientId,
           string clientScret)
        {
            this.BaseUrl = baseUrl;
            this.Realm = realms;
            this.ClientId = clientId;
            this.ClientSecret = clientScret;
        }

        /// <summary>
        /// Creates the user asynchronous.
        /// </summary>
        /// <param name="registrationModel">The registration model.</param>
        /// <returns>
        /// returns result.
        /// </returns>
        public async Task<bool> CreateUserAsync(User registrationModel)
        {
            if (registrationModel == null)
            {
                throw new ArgumentNullException(nameof(registrationModel));
            }

            try
            {
                string token = await KeyCloakClientAuthentication.GetTokenAsync(this.BaseUrl, this.Realm,
                     this.ClientId, this.ClientSecret);

                var keyCloakClient = new RestClient();
                keyCloakClient.BaseUrl = this.GetBaseUrl();
                var request = new RestRequest("/users", Method.POST);
                request.AddHeader(authHeader, token);
                request.AddHeader("Content-Type", "application/json");
                request.RequestFormat = DataFormat.Json;
                var jsonModel = JsonConvert.SerializeObject(registrationModel);
                request.AddParameter("application/json", jsonModel, ParameterType.RequestBody);
                var response = await keyCloakClient.ExecuteAsync(request);
                if (!response.IsSuccessful)
                {
                    var errorMessage = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
                    throw new System.InvalidOperationException(errorMessage.ErrorMessage);
                }
                return response.IsSuccessful;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Update the user asynchronous.
        /// </summary>
        /// <param name="userModel">The registration model.</param>
        /// <returns>
        /// returns result.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> UpdateUserAsync(User userModel)
        {
            if (userModel == null)
            {
                throw new ArgumentNullException(nameof(userModel));
            }

            try
            {
                string token = await KeyCloakClientAuthentication.GetTokenAsync(this.BaseUrl, this.Realm,
                    this.ClientId, this.ClientSecret);

                var keyCloakClient = new RestClient();
                keyCloakClient.BaseUrl = this.GetBaseUrl();
                var request = new RestRequest("/users/{id}", Method.PUT);
                request.AddUrlSegment(_userId, userModel.UserId);
                request.AddHeader(authHeader, token);
                request.AddHeader("Content-Type", "application/json");
                request.RequestFormat = DataFormat.Json;
                var jsonModel = JsonConvert.SerializeObject(userModel);
                request.AddParameter("application/json", jsonModel, ParameterType.RequestBody);
                var response = await keyCloakClient.ExecuteAsync(request);
                if (!response.IsSuccessful)
                {
                    var errorMessage = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
                    throw new System.InvalidOperationException(errorMessage.ErrorMessage);
                }
                return response.IsSuccessful;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes the user asynchronous.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>returns true if success.</returns>
        public async Task<bool> DeleteUserAsync(string userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            try
            {
                string token = await KeyCloakClientAuthentication.GetTokenAsync(this.BaseUrl, this.Realm,
                 this.ClientId, this.ClientSecret);

                var keyCloakClient = new RestClient();
                keyCloakClient.BaseUrl = this.GetBaseUrl();
                var request = new RestRequest("/users/{id}", Method.DELETE);
                request.AddUrlSegment(_userId, userId);
                request.AddHeader(authHeader, token);
                request.RequestFormat = DataFormat.Json;
                var response = await keyCloakClient.ExecuteAsync(request);
                return response.IsSuccessful;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get the user details asynchronous.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>returns the user details.</returns>
        public async Task<User> GetUserAsync(string userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            try
            {
                string token = await KeyCloakClientAuthentication.GetTokenAsync(this.BaseUrl, this.Realm,
                    this.ClientId, this.ClientSecret);

                var keyCloakClient = new RestClient();
                keyCloakClient.BaseUrl = this.GetBaseUrl();
                var request = new RestRequest("/users/{id}", Method.GET);
                request.AddUrlSegment(_userId, userId);
                request.AddHeader(authHeader, token);
                request.RequestFormat = DataFormat.Json;
                var response = await keyCloakClient.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    var user = JsonConvert.DeserializeObject<User>(response.Content);
                    return user;
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The get users.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="index">The index.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>return user list.</returns>
        public async Task<List<User>> GetUsersAsync(string userName, string firstName, string lastName, int index, int pageSize)
        {
            try
            {
                string token = await KeyCloakClientAuthentication.GetTokenAsync(this.BaseUrl, this.Realm,
                    this.ClientId, this.ClientSecret);

                var keyCloakClient = new RestClient();
                keyCloakClient.BaseUrl = this.GetBaseUrl();
                var request = new RestRequest("/users"
                    , Method.GET);
                request.AddQueryParameter(_userName, userName);
                request.AddQueryParameter(_firstName, firstName);
                request.AddQueryParameter(_lastName, lastName);
                request.AddQueryParameter(_first, index.ToString());
                request.AddQueryParameter(_max, pageSize.ToString());
                request.AddHeader(authHeader, token);
                request.RequestFormat = DataFormat.Json;
                var response = await keyCloakClient.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    var user = JsonConvert.DeserializeObject<List<User>>(response.Content);
                    return user;
                }

                else
                {
                    var errorMessage = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
                    throw new System.InvalidOperationException(errorMessage.ErrorMessage);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        ///// <summary>
        ///// The get realm roles.
        ///// </summary>
        ///// <param name="first">The first.</param>
        ///// <param name="max">The max.</param>
        ///// <param name="search">The search.</param>
        ///// <returns>The result.</returns>
        //public async Task<List<RoleRepresentation>> GetRealmRoles(int first, int max, string search)
        //{
        //    string token = await KeyCloakClientAuthentication.GetTokenAsync(this.BaseUrl, this.Realm,
        //           this.ClientId, this.ClientSecret);

        //    var keyCloakClient = new RestClient();
        //    keyCloakClient.BaseUrl = this.GetBaseUrl();
        //    var request = new RestRequest("/roles"
        //        , Method.GET);
        //    request.AddQueryParameter("first", first.ToString());
        //    request.AddQueryParameter("max", max.ToString());
        //    request.AddQueryParameter("search", search);
        //    request.AddHeader(authHeader, token);
        //    request.RequestFormat = DataFormat.Json;
        //    var response = await keyCloakClient.ExecuteAsync(request);

        //    if (response.IsSuccessful)
        //    {
        //        return JsonConvert.DeserializeObject<List<RoleRepresentation>>(response.Content);
        //    }

        //    else
        //    {
        //        var errorMessage = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
        //        throw new System.InvalidOperationException(errorMessage.ErrorMessage);
        //    }
        //}

        //public async Task<bool> AssignRealmRoles(string userId, List<RoleRepresentation> roles)
        //{
        //    if (roles == null)
        //    {
        //        throw new ArgumentNullException(nameof(roles));
        //    }

        //    try
        //    {
        //        string token = await KeyCloakClientAuthentication.GetTokenAsync(this.BaseUrl, this.Realm,
        //             this.ClientId, this.ClientSecret);

        //        var keyCloakClient = new RestClient();
        //        keyCloakClient.BaseUrl = this.GetBaseUrl();
        //        var request = new RestRequest("/users/{userId}/role-mappings/realm", Method.POST);
        //        request.AddHeader(authHeader, token);
        //        request.AddHeader("Content-Type", "application/json");
        //        request.AddUrlSegment("userId", userId);
        //        request.RequestFormat = DataFormat.Json;
        //        var jsonModel = JsonConvert.SerializeObject(roles);
        //        request.AddParameter("application/json", jsonModel, ParameterType.RequestBody);
        //        var response = await keyCloakClient.ExecuteAsync(request);
        //        if (!response.IsSuccessful)
        //        {
        //            var errorMessage = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
        //            throw new System.InvalidOperationException(errorMessage.ErrorMessage);
        //        }
        //        return response.IsSuccessful;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}



        /// <summary>
        /// Creates the user asynchronous.
        /// </summary>
        /// <param name="user">The registration model.</param>
        /// <returns>
        /// returns result.
        /// </returns>
        public async Task<string> LoginUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            try
            {
                string token = await KeyCloakClientAuthentication.GetUserTokenAsync(this.BaseUrl, this.Realm,
                     "performance-test", user.UserName, user.TempCredentials.FirstOrDefault().Value);
                return token;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// The get base url.
        /// </summary>
        /// <returns>The result.</returns>
        private Uri GetBaseUrl()
        {
            return new Uri($"{this.BaseUrl}/auth/admin/realms/{this.Realm}");
        }

    }
}
