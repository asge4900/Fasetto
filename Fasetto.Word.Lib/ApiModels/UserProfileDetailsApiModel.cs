namespace Fasetto.Word.Lib
{
    /// <summary>
    /// The result of a login request or get user profile details request via API
    /// </summary>
    public class UserProfileDetailsApiModel
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public UserProfileDetailsApiModel()
        {

        }

        #endregion

        #region Public Properties

        public string Id { get; set; }

        /// <summary>
        /// The authentication token used to stay authenticated through future requests
        /// </summary>
        /// /// <remarks>The Token is only provided when called from the login methods</remarks>
        public string Token { get; set; }

        /// <summary>
        /// The users first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The users last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The users username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The users email
        /// </summary>
        public string Email { get; set; }

        #endregion

        #region Public Helper Methods

        public LoginCredentialsDataModel ToLoginCredentialsDataModel()
        {
            return new LoginCredentialsDataModel
            {
                Id = Id,
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Username = Username,
                Token = Token
            };
        }

        #endregion
    }
}