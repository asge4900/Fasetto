using GalaSoft.MvvmLight.Command;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Fasetto.Word.Lib
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
                await Task.Delay(1000);

                //Go to chat page
                IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.Chat);

                //var email = this.Email;

                //// IMPORTANT: Never store unsecure password in variable like this
                //var pass = (parameter as IHavePassword).SecurePassword.Unsecure();
            });
        }

        /// <summary>
        /// Takes the user to the register page
        /// </summary>        
        /// <returns></returns>
        public async Task RegisterAsync()
        {
            //Go to register page
            IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.Register);

            await Task.Delay(1);
        }
    }
}
