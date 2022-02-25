using Jellyfin.Plugin.AtlasAuthenticationProvider.Enums;

namespace Jellyfin.Plugin.AtlasAuthenticationProvider.Models
{
    /// <summary>
    /// A model representing a return authentication payload from the authentication server.
    /// </summary>
    public class AuthenticateUserResult
    {
        /// <summary>
        /// Gets or Sets the AuthenticationResult.
        /// </summary>
        public AuthOpCode AuthenticationResult { get; set; }

        /// <summary>
        /// Gets or Sets the UserToken Result.
        /// </summary>
        public string? UserToken { get; set; }
    }
}
