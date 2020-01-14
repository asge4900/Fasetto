using Dna;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Fasetto.Word.Lib
{
    /// <summary>
    /// The View Model for a login screen
    /// </summary>
    public class RegisterViewModel : BaseViewModel
    {    

        #region Constructor

        public RegisterViewModel()
        {
            //Create commands
            RegisterCommand = new RelayParameterizedCommand(async (parameter) => await RegisterAsync(parameter));
            LoginCommand = new RelayCommand(async () => await LoginAsync());
        }

        #endregion

        #region Properties

        /// <summary>
        /// The username of the user
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The email of the user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// A flag indicating if the register command is running
        /// </summary>
        public bool RegisterIsRunning { get; set; }

        #endregion

        #region Commands     

        /// <summary>
        /// The command to login
        /// </summary>
        public ICommand LoginCommand { get; set; }

        /// <summary>
        /// The command to register for a new account
        /// </summary>
        public ICommand RegisterCommand { get; set; }

        #endregion

        /// <summary>
        /// Attempts to register a new user
        /// </summary>
        /// <param name="parameter">The <see cref="SecureString"/>passed in from the view for the users password</param>
        /// <returns></returns>
        public async Task RegisterAsync(object parameter)
        {
            // Call the server and attempt to register with the provided credentials
            // TODO: Move all URLS and API routes to static class
            var result = await WebRequests.PostAsync<ApiResponse<RegisterResultApiModel>>(
                "http://localhost:58727/api/register",
                new RegisterCredentialsApiModel
                {
                    Username = Username,
                    Email = Email,
                    Password = (parameter as IHavePassword).SecurePassword.Unsecure()
                });

            // If the reposne has an error...
            if (await result.DisplayErrorIfFailedAsync("Register failed"))
                //We are done
                return;

            // OK successfully registered (and logged in)... now get users data
            var loginResult = result.ServerResponse.Response;

            // Let the application view model handle what happens
            // With the successful login
            await IoC.Application.HandleSuccessfulLoginAsync(loginResult);
        }

        /// <summary>
        /// Takes the user to the login page
        /// </summary>        
        /// <returns></returns>
        public async Task LoginAsync()
        {
            //Go to register page
            IoC.Application.GoToPage(ApplicationPage.Login);

            await Task.Delay(1);
        }
    }
}
