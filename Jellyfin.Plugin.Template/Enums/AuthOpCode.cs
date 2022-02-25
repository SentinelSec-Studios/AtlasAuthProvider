using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jellyfin.Plugin.AtlasAuthenticationProvider.Enums
{
    /// <summary>
    /// A list of all possible Authentication OpCodes.
    /// </summary>
    public enum AuthOpCode
    {
        /// <summary>
        /// An unknown error has occurred.
        /// </summary>
        UnknownError,

        /// <summary>
        /// The user account in context is not authenticated.
        /// </summary>
        NotAuthenticated,

        /// <summary>
        /// The user account in context is authenticated.
        /// </summary>
        Authenticated,

        /// <summary>
        /// The user account in context is already authenticated.
        /// </summary>
        AlreadyAuthenticated,

        /// <summary>
        /// Authentication of the specified user account has succeeded.
        /// </summary>
        AuthenticationSuccessful,

        /// <summary>
        /// Authentication of the specified user account has failed.
        /// </summary>
        AuthenticationFailed,

        /// <summary>
        /// Authentication of the specified user account has failed because the account is disabled.
        /// </summary>
        AuthenticationDisabled,

        /// <summary>
        /// The provided payload version is invalid.
        /// </summary>
        InvalidPayload,

        /// <summary>
        /// The provided API version is invalid.
        /// </summary>
        InvalidAPIVersion,

        /// <summary>
        /// Use of the requested Intent was forbidden.
        /// </summary>
        IntentForbidden,

        /// <summary>
        /// The request action succeeded.
        /// </summary>
        Success,

        /// <summary>
        /// The request action failed.
        /// </summary>
        Failed,

        /// <summary>
        /// An issue occurred while trying to initialize the specified Token.
        /// </summary>
        TokenInitFailed,

        /// <summary>
        /// The specified user already exists.
        /// </summary>
        UserAlreadyExists,

        /// <summary>
        /// The specified email already exists.
        /// </summary>
        EmailAlreadyExists,

        /// <summary>
        /// The specified user does exist.
        /// </summary>
        UserExists,

        /// <summary>
        /// The specified user does not exist.
        /// </summary>
        UserNonExistant,

        /// <summary>
        /// The specified email does exist.
        /// </summary>
        EmailExists,

        /// <summary>
        /// The specified email does not exist.
        /// </summary>
        EmailNonExistant,

        /// <summary>
        /// The OTP code in context is valid.
        /// </summary>
        OTPValid,

        /// <summary>
        /// The OTP code in context is invalid.
        /// </summary>
        OTPInvalid
    }
}
