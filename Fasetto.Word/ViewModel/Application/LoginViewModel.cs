﻿using Dna;
using Fasetto.Word.Lib;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;
using static Fasetto.Word.DI;

namespace Fasetto.Word
{
    /// <summary>
    /// The View Model for a login screen
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {

        #region Constructor

        public LoginViewModel()
        {
            //Create commands
            LoginCommand = new RelayParameterizedCommand(async (parameter) => await LoginAsync(parameter));
            RegisterCommand = new RelayCommand(async () => await RegisterAsync());
        }

        #endregion

        #region Properties    

        /// <summary>
        /// The email of the user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// A flag indicating if the login command is running
        /// </summary>
        public bool LoginIsRunning { get; set; }

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
        /// Attempts to log the user in
        /// </summary>
        /// <param name="parameter">The <see cref="SecureString"/>passed in from the view for the users password</param>
        /// <returns></returns>
        public async Task LoginAsync(object parameter)
        {
            await RunCommandAsync(() => LoginIsRunning, async () =>
            {
                // Call the server and attempt to login with credentials
                // TODO: Move all URLS and API routes to static class
                var result = await WebRequests.PostAsync<ApiResponse<UserProfileDetailsApiModel>>(
                    "http://localhost:58727/api/login",
                    new LoginCredentialsApiModel
                    {
                        UsernameOrEmail = Email,
                        Password = (parameter as IHavePassword).SecurePassword.Unsecure()
                    });

                // If the reposne has an error...
                if (await result.DisplayErrorIfFailedAsync("Login failed"))
                    //We are done
                    return;

                // OK successfully logged in... now get users data
                var loginResult = result.ServerResponse.ResponseT;

                // Let the application view model handle what happens
                // With the successful login
                await ViewModelApplication.HandleSuccessfulLoginAsync(loginResult);
            });
        }

        /// <summary>
        /// Takes the user to the register page
        /// </summary>        
        /// <returns></returns>
        public async Task RegisterAsync()
        {
            //Go to register page
            ViewModelApplication.GoToPage(ApplicationPage.Register);

            await Task.Delay(1);
        }
    }
}
