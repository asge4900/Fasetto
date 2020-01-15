using Fasetto.Word.Lib;
using System.Threading.Tasks;
using static Fasetto.Word.DI;

namespace Fasetto.Word
{
    /// <summary>
    /// The application state as a view model
    /// </summary>
    public class ApplicationViewModel : BaseViewModel
    {
        /// <summary>
        /// The current page of the application
        /// </summary>
        public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.Login;

        /// <summary>
        /// The view model to use for the current page when the currentpage changes
        /// Note: This is not a live up-to-date view model of the current page
        ///       It is simply used to set the view model of the current page
        ///       at the time it changes
        /// </summary>
        public BaseViewModel CurrentPageViewModel { get; set; }

        /// <summary>
        /// True if the side menu should be shown
        /// </summary>
        public bool SideMenuVisible { get; set; } = false;

        /// <summary>
        /// True if the settings menu should be shown
        /// </summary>
        public bool SettingsMenuVisible { get; set; } = false;

        /// <summary>
        /// Navigates to the specified page
        /// </summary>
        /// <param name="page">The page to go to</param>
        /// <param name="viewModel">The view model, if any, to set explicitly to the new page</param>
        public void GoToPage(ApplicationPage page, BaseViewModel viewModel = null)
        {
            //Always hide settings page if we are changing pages
            SettingsMenuVisible = false;

            //Set the current view model
            CurrentPageViewModel = viewModel;

            //Set the current page
            CurrentPage = page;

            //Fire off a CurrentPage changed event
            OnPropertyChanged(nameof(CurrentPage));

            //Show side menu or not?
            SideMenuVisible = page == ApplicationPage.Chat;
        }

        /// <summary>
        /// Handles what happen when we have successfully logged in
        /// </summary>
        /// <param name="loginResult">The results from the succesful login</param>
        public async Task HandleSuccessfulLoginAsync(UserProfileDetailsApiModel loginResult)
        {
            // Store this in the client data store
            await ClientDataStore.SaveLoginCredentialsAsync(new LoginCredentialsDataModel
            {
                Id = loginResult.Id,
                Email = loginResult.Email,
                FirstName = loginResult.FirstName,
                LastName = loginResult.LastName,
                Username = loginResult.Username,
                Token = loginResult.Token
            });

            // Load new settings
            await ViewModelSettings.LoadAsync();

            //Go to chat page
            ViewModelApplication.GoToPage(ApplicationPage.Chat);
        }
    }
}
