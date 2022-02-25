namespace Jellyfin.Plugin.AtlasAuthenticationProvider.Models
{
    /// <summary>
    /// A model representing a return subscription payload from the authorization server.
    /// </summary>
    public class SubscriptionValidityResult
    {
        /// <summary>
        /// Gets or Sets the ValidityResult.
        /// </summary>
        public string? ValidityResult { get; set; }
    }
}
