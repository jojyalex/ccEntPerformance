using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ccEntPerformance
{
    public class User
    {
        public User(
            string userName, bool isEnabled, bool isEmailVerified, string firstName, string lastName, string email,
            Credential tempCredentials, string userId, string[] disableableCredentialTypes, string[] requiredActions,
            Access access, object clientRoles, object attributes) : this(userName, isEnabled, isEmailVerified, firstName,
            lastName, email, tempCredentials, clientRoles, attributes)
        {
            UserName = userName;
            IsEnabled = isEnabled;
            IsEmailVerified = isEmailVerified;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            TempCredentials = new List<Credential>();
            TempCredentials.Add(tempCredentials);
            UserId = userId;
            DisableableCredentialTypes = disableableCredentialTypes;
            RequiredActions = requiredActions;
            Access = access;
            ClientRoles = clientRoles;
            Attributes = attributes;
        }

        public User(string userName, bool isEnabled, bool isEmailVerified, string firstName,
           string lastName, string email, Credential tempCredentials, object clientRoles, object attributes)
        {
            UserName = userName;
            IsEnabled = isEnabled;
            IsEmailVerified = isEmailVerified;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            TempCredentials = new List<Credential>();
            TempCredentials.Add(tempCredentials);
            UserId = Guid.NewGuid().ToString();
            ClientRoles = clientRoles;
            Attributes = attributes;
        }

        public User()
        {
        }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [JsonProperty("username")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("enabled")]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is email verified.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is email verified; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("emailVerified")]
        public bool IsEmailVerified { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
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

        /// <summary>
        /// Gets or sets the disable credential types.
        /// </summary>
        /// <value>
        /// The disable credential types.
        /// </value>
        [JsonProperty("disableableCredentialTypes")]
        public string[] DisableableCredentialTypes { get; set; }

        /// <summary>
        /// Gets or sets the required actions.
        /// </summary>
        /// <value>
        /// The required actions.
        /// </value>
        [JsonProperty("requiredActions")]
        public string[] RequiredActions { get; set; }

        /// <summary>
        /// Gets or sets the access.
        /// </summary>
        /// <value>
        /// The access.
        /// </value>
        [JsonProperty("access")]
        public Access Access { get; set; }

        /// <summary>
        /// Gets or sets the attributes.
        /// </summary>
        /// <value>
        /// The attributes.
        /// </value>
        [JsonProperty("attributes")]
        public object Attributes { get; set; }

        /// <summary>
        /// Gets or sets the client roles.
        /// </summary>
        /// <value>
        /// The client roles.
        /// </value>
        [JsonProperty("clientRoles")]
        public object ClientRoles { get; set; }

        public void Update(string userName, string firstName,
           string lastName, string email)
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            TempCredentials = new List<Credential>();
        }

        

    }
}
