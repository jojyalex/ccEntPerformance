using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ccEntPerformance
{
    /// <summary>
    /// The user credentials.
    /// </summary>
    public class Credential
    {
        /// <summary>
        /// Gets or sets the type as Credential Representation.
        /// </summary>
        /// <value>
        /// The type.
        /// </value
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is temporary.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is temporary; otherwise, <c>false</c>.
        /// Users forced to reset temporary password in login.
        /// </value>
        [JsonProperty("temporary")]
        public bool IsTemperory { get; set; }
    }
}
