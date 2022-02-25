using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Jellyfin.Data.Entities;
using Jellyfin.Plugin.AtlasAuthenticationProvider.Enums;
using Jellyfin.Plugin.AtlasAuthenticationProvider.Models;
using MediaBrowser.Common;
using MediaBrowser.Controller.Authentication;
using MediaBrowser.Controller.Library;
using Microsoft.Extensions.Logging;

namespace Jellyfin.Plugin.AtlasAuthenticationProvider
{
    /// <summary>
    /// Contains all user authentication checks.
    /// </summary>
    public class AuthValidation : IAuthenticationProvider
    {
        private readonly ILogger<AuthValidation> _logger;
        private readonly IApplicationHost _applicationHost;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthValidation"/> class.
        /// </summary>
        /// <param name="applicationHost">Instance of the <see cref="IApplicationHost"/> interface.</param>
        /// <param name="logger">Instance of the <see cref="ILogger{AuthValidation}"/> interface.</param>
        public AuthValidation(IApplicationHost applicationHost, ILogger<AuthValidation> logger)
        {
            _applicationHost = applicationHost;
            _logger = logger;
        }

        /// <summary>
        /// Gets a value indicating whether gets plugin enabled.
        /// </summary>
        public string Name => "AtlasAuthenticationProvider";

        /// <summary>
        /// Gets a value indicating whether gets plugin enabled.
        /// </summary>
        public bool IsEnabled => true;

        /// <summary>
        /// Authenticate user against the AtlasAuthentication server.
        /// </summary>
        /// <param name="username">Username to authenticate.</param>
        /// <param name="password">Password to authenticate.</param>
        /// <returns>A <see cref="ProviderAuthenticationResult"/> with the authentication result.</returns>
        /// <exception cref="AuthenticationException">Exception when failing to authenticate.</exception>
        public async Task<ProviderAuthenticationResult> Authenticate(string username, string password)
        {
            if (Plugin.Instance == null)
            {
                _logger.LogError("An error occurred while obtaining the current plugin instance.");
                throw new AuthenticationException("Found no instance for the plugin in context.");
            }

            var configuration = Plugin.Instance.Configuration;
            var userManager = _applicationHost.Resolve<IUserManager>();

            AuthenticateUserResult? authUserResult;
            try
            {
                using var client = new HttpClient();
                using var emptyContent = new StringContent(string.Empty);
                var url = $"{configuration.ConnectionURI}/API/AuthenticateUser?username={username}&userPass={password}";
                var response = await client.PostAsync(url, emptyContent).ConfigureAwait(true);
                authUserResult = JsonSerializer.Deserialize<AuthenticateUserResult>(await response.Content.ReadAsStringAsync().ConfigureAwait(true));
            }
            catch (Exception e)
            {
                _logger.LogWarning("An exception occurred whilst contacting authentication services.", e);
                throw new AuthenticationException("An issue occurred while attempting to contact authentication services.");
            }

            if (authUserResult == null) // Auth request returned invalid user object.
            {
                throw new AuthenticationException("The provided user could not be found.");
            }
            else if (authUserResult.AuthenticationResult != AuthOpCode.AuthenticationSuccessful)
            {
                throw new AuthenticationException("Authentication failed for the specified user.");
            }

            SubscriptionValidityResult? subscriptionValidityResult;
            try
            {
                using var client = new HttpClient();
                using var emptyContent = new StringContent(string.Empty);
                var url = $"{configuration.ConnectionURI}/API/ValidateSubscription?userToken={authUserResult.UserToken}";
                var response = await client.PostAsync(url, emptyContent).ConfigureAwait(true);
                subscriptionValidityResult = JsonSerializer.Deserialize<SubscriptionValidityResult>(await response.Content.ReadAsStringAsync().ConfigureAwait(true));
            }
            catch (Exception e)
            {
                _logger.LogWarning("An exception occurred whilst contacting authorization services.", e);
                throw new AuthenticationException("An issue occurred while attempting to contact authorization services.");
            }

            if (subscriptionValidityResult == null) // subValidity request returned invalid object.
            {
                throw new AuthenticationException("The provided user could not be found.");
            }
            else if (subscriptionValidityResult.ValidityResult != "Valid")
            {
                throw new AuthenticationException("The specified user's subscription is invalid.");
            }

            User? user;
            try// Check if our user exists in jellyfin.
            {
                user = userManager.GetUserByName(username);
                if (user == null)
                {
                    _logger.LogWarning("No JF user was returned, this is fatal.");
                    throw new AuthenticationException("The provided credentials have not been linked to a JF account.");
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning("User Manager could not find a JF user for Atlas User, user has not initialized a JF account.", e);
                throw new AuthenticationException("The provided credentials have not been linked to a JF account.");
            }

            var provider = new ProviderAuthenticationResult { Username = user.Username };
            return provider;
        }

        /// <inheritdoc />
        public Task ChangePassword(User user, string newPassword)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool HasPassword(User user)
        {
            return true;
        }
    }
}
