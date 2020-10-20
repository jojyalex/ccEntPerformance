using System;
using System.Collections.Generic;
using System.Text;

namespace ccEntPerformance
{
    public class AccessTokenResponse
    {

        /// <summary>
        /// Gets or Sets the access_token.
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// Gets or Sets the token_type.
        /// </summary>
        public string token_type { get; set; }

        /// <summary>
        /// Gets or Sets the expires_in.
        /// </summary>
        public int expires_in { get; set; }

        /// <summary>
        /// Gets or Sets the refresh_expires_in.
        /// </summary>
        public int refresh_expires_in { get; set; }

        /// <summary>
        /// Gets or Sets the refresh_token.
        /// </summary>
        public string refresh_token { get; set; }
    }

}
