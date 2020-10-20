using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ccEntPerformance
{
    /// <summary>
    /// The users access settings.
    /// This provide access permission to 
    /// manage group member ship, view users, map roles,
    /// impersonate, and manage user details.
    /// </summary>
    public class Access
    {
        [JsonProperty("manageGroupMembership")]
        public bool ManageGroupMembership { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is view.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is view; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("view")]
        public bool IsView { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [map roles].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [map roles]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("mapRoles")]
        public bool MapRoles { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Access"/> is impersonate.
        /// </summary>
        /// <value>
        ///   <c>true</c> if impersonate; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("impersonate")]
        public bool Impersonate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is manage.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is manage; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("manage")]
        public bool IsManage { get; set; }
    }
}
