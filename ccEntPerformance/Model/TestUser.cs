using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ccEntPerformance
{
    class TestUser
    {


        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the temporary credentials.
        /// </summary>
        /// <value>
        /// The temporary credentials.
        /// </value>
        [JsonProperty("credentials")]
        public List<Credential> TempCredentials { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [JsonProperty("id")]
        public string UserId { get; set; }

        public string Token { get; set; }
        public string AuthToken
        {
            get
            {
                return $"Bearer {Token}";
            }
        }
    }
}
