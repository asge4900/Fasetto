using Fasetto.Word.Lib;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fasetto.Word.Web.Server
{
    /// <summary>
    /// Manages the Web API calls
    /// </summary>
    [AuthorizeToken]
    public class ApiController : Controller
    {
        #region Fields

        /// <summary>
        /// The scoped Application context
        /// </summary>
        protected ApplicationDbContext context;

        /// <summary>
        /// The manager for handling user creation, deletion, searching, roles etc...
        /// </summary>
        protected UserManager<ApplicationUser> userManager;

        /// <summary>
        /// The manager for handling signing in and out for our users
        /// </summary>
        protected SignInManager<ApplicationUser> signInManager;
        
        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context">The injected context</param>
        /// <param name="signInManager">The Identity sign in manager</param>
        /// <param name="userManager">The Identity user manager</param>
        public ApiController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        #endregion

        #region Login / Register / Verify

        /// <summary>
        /// Tries to register for a new account on the server
        /// </summary>
        /// <param name="registerCredentials">The registration details</param>
        /// <returns>Returns the result of the register request</returns>
        [AllowAnonymous]
        [Route("api/register")]
        public async Task<ApiResponse<RegisterResultApiModel>> RegisterAsync([FromBody]RegisterCredentialsApiModel registerCredentials)
        {
            // TODO: Localize all strings
            // The message when we fail to login
            var invalidErrorMessage = "Please provide all required details to register for an account";

            // The error response for a failed login
            var errorResponse = new ApiResponse<RegisterResultApiModel>
            {
                // Set error message
                ErrorMessage = invalidErrorMessage
            };

            // If we gave no credentials..
            if (registerCredentials == null)
            {
                // Return failed response
                return errorResponse;
            }

            // Make sure we have a username
            if (string.IsNullOrWhiteSpace(registerCredentials.Username))
                // Return error message to user
                return errorResponse;

            // Create the desired user from the given details
            var user = new ApplicationUser
            {
                UserName = registerCredentials.Username,
                FirstName = registerCredentials.FirstName,
                LastName = registerCredentials.LastName,
                Email = registerCredentials.Email
            };

            // Try and create a user
            var result = await userManager.CreateAsync(user, registerCredentials.Password);

            // If the registration was succesful...
            if (result.Succeeded)
            {
                // Get the user details
                var userIdentity = await userManager.FindByNameAsync(user.UserName);

                // TODO: Email the user the verification code

                // Return valid response containing all users details
                return new ApiResponse<RegisterResultApiModel>
                {
                    Response = new RegisterResultApiModel
                    {
                        Id = userIdentity.Id,
                        FirstName = userIdentity.FirstName,
                        LastName = userIdentity.LastName,
                        Email = userIdentity.Email,
                        Username = userIdentity.UserName,
                        Token = userIdentity.GenerateJwtToken()
                    }
                };
            }
            // Otherwise if it failed
            else
            {
                // Return the failed response
                return new ApiResponse<RegisterResultApiModel>
                {
                    // Aggregate all errors into a single error string
                    ErrorMessage = result.Errors.AggregateErrors()
                };
            }
        }

        /// <summary>
        /// Logs in a user using token-based authentication
        /// </summary>
        /// <param name="loginCredentials"></param>
        /// <returns>Returns the result of the login request</returns>
        [AllowAnonymous]
        [Route("api/login")]
        public async Task<ApiResponse<UserProfileDetailsApiModel>> LogInAsync([FromBody]LoginCredentialsApiModel loginCredentials)
        {
            // TODO: Localize all strings
            // The message when we fail to login
            var invalidErrorMessage = "Invalid username or password";

            // The error response for a failed login
            var errorResponse = new ApiResponse<UserProfileDetailsApiModel>
            {
                // Set error message
                ErrorMessage = invalidErrorMessage
            };

            // Make sure we have a username
            if (loginCredentials?.UsernameOrEmail == null || string.IsNullOrWhiteSpace(loginCredentials.UsernameOrEmail))
                // Return error message to user
                return errorResponse;

            // Validate if the user credentials are correct

            // Is it an email
            var isEmail = loginCredentials.UsernameOrEmail.Contains("@");

            // Get the user details
            var user = isEmail ?
                // Find by email
                await userManager.FindByEmailAsync(loginCredentials.UsernameOrEmail) :
                // Find by username
                await userManager.FindByNameAsync(loginCredentials.UsernameOrEmail);

            // If we failed to find a user...
            if (user == null)
            {
                // Return error message to user
                return errorResponse;
            }

            // If we got here we have a user...
            // Let's validate the password

            // Get if password is valid
            var isValidPassword = await userManager.CheckPasswordAsync(user, loginCredentials.Password);

            // If the password was wrong
            if (!isValidPassword)
            {
                // Return error message to user
                return errorResponse;
            }

            // If we got here, we are valid and the user passed the correct login details

            // Get username
            var username = user.UserName;

            // Return token to user
            return new ApiResponse<UserProfileDetailsApiModel>
            {
                // Pass back the user details and the token
                Response = new UserProfileDetailsApiModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Username = user.UserName,
                    Token = user.GenerateJwtToken()
                }
            };
        }

        #endregion

        /// <summary>
        /// Returns the users profile details based on the authenticated user
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResponse<UserProfileDetailsApiModel>> GetUserProfileAsync()
        {
            // Get user claims
            var user = await userManager.GetUserAsync(HttpContext.User);

            // If we have no user...
            if (user == null)
                // Return error
                return new ApiResponse<UserProfileDetailsApiModel>()
                {
                    // TODO: Localization
                    ErrorMessage = "User not found"
                };

            // Return token to user
            return new ApiResponse<UserProfileDetailsApiModel>
            {
                // Pass back the user details and the token
                Response = new UserProfileDetailsApiModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Username = user.UserName
                }
            };
        }

        /// <summary>
        /// Attempts to update the users profile details
        /// </summary>
        /// <param name="model">The user profile details to update</param>
        /// <returns>
        ///     Returns successful response if the update was successful, 
        ///     otherwise returns the error reasons for the failure
        /// </returns>
        public async Task<ApiResponse> UpdateUserProfileAsync([FromBody]UpdateUserProfileApiModel model)
        {
            #region Declare Variables

            // Make a list of empty errors
            var errors = new List<string>();

            // Keep track of email change
            var emailChanged = false;

            #endregion

            #region Get User

            // Get the current user
            var user = await userManager.GetUserAsync(HttpContext.User);

            // If we have no user...
            if (user == null)
                return new ApiResponse
                {
                    // TODO: Localization
                    ErrorMessage = "User not found"
                };

            #endregion

            #region Update Profile

            // If we have a first name...
            if (model.FirstName != null)
                // Update the profile details
                user.FirstName = model.FirstName;

            // If we have a last name...
            if (model.LastName != null)
                // Update the profile details
                user.LastName = model.LastName;

            // If we have a email...
            if (model.Email != null &&
                // And it is not the same...
                !string.Equals(model.Email.Replace(" ", ""), user.NormalizedEmail))
            {
                // Update the email
                user.Email = model.Email;

                // Un-verify the email
                user.EmailConfirmed = false;

                // Flag we have changed email
                emailChanged = true;
            }

            // If we have a username...
            if (model.Username != null)
                // Update the profile details
                user.UserName = model.Username;

            #endregion

            #region Save Profile

            // Attempt to commit changes to data store
            var result = await userManager.UpdateAsync(user);

            // If successful, send out email verification
            
                // Send email verification
                

            #endregion

            #region Respond

            // If we were successful...
            if (result.Succeeded)
                // Return successful response
                return new ApiResponse();
            // Otherwise if it failed...
            else
                // Return the failed response
                return new ApiResponse
                {
                    ErrorMessage = result.Errors.AggregateErrors()
                };

            #endregion
        }
    }
}