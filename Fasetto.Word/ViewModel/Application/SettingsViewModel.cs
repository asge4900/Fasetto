using Dna;
using Fasetto.Word.Lib;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using static Fasetto.Word.DI;
using static Dna.FrameworkDI;
using System.Linq.Expressions;

namespace Fasetto.Word
{
    /// <summary>
    /// The settings state as a view model
    /// </summary>
    public class SettingsViewModel : BaseViewModel
    {
        #region Fields

        /// <summary>
        /// The text to show while loading text
        /// </summary>
        private string loadingText = "...";

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public SettingsViewModel()
        {          

            // Create first name
            FirstName = new TextEntryViewModel
            {
                Label = "First Name",
                OriginalText = loadingText,
                CommitAction = SaveFirstNameAsync
            };

            // Create last name
            LastName = new TextEntryViewModel
            {
                Label = "Last Name",
                OriginalText = loadingText,
                CommitAction = SaveLastNameAsync
            };

            // Create Username
            Username = new TextEntryViewModel
            {
                Label = "Username",
                OriginalText = loadingText,
                CommitAction = SaveUsernameAsync
            };

            // Create Password
            Password = new PasswordEntryViewModel
            {
                Label = "Password",
                FakePassword = "********",
                CommitAction = SavePasswordAsync
            };

            // Create Email
            Email = new TextEntryViewModel
            {
                Label = "Email",
                OriginalText = loadingText,
                CommitAction = SaveEmailAsync
            };

            // Create commands
            OpenCommand = new RelayCommand(Open);
            CloseCommand = new RelayCommand(Close);
            LogoutCommand = new RelayCommand(async () => await LogoutAsync());            
            ClearUserDataCommand = new RelayCommand(ClearUserData);
            LoadCommand = new RelayCommand(async () => await LoadAsync());
            SaveFirstNameCommand = new RelayCommand(async () => await SaveFirstNameAsync());
            SaveLastNameCommand = new RelayCommand(async () => await SaveLastNameAsync());
            SaveUsernameCommand = new RelayCommand(async () => await SaveUsernameAsync());
            SaveEmailCommand= new RelayCommand(async () => await SaveEmailAsync());

            //TODO: Get from localization
            LogoutButtonText = "Logout";
        }

        #endregion

        #region Properties

        /// <summary>
        /// The current users first name
        /// </summary>
        public TextEntryViewModel FirstName { get; set; }

        /// <summary>
        /// The current users last name
        /// </summary>
        public TextEntryViewModel LastName { get; set; }

        /// <summary>
        /// The current users username
        /// </summary>
        public TextEntryViewModel Username { get; set; }

        /// <summary>
        /// The current users password
        /// </summary>
        public PasswordEntryViewModel Password { get; set; }

        /// <summary>
        /// The current users email
        /// </summary>
        public TextEntryViewModel Email { get; set; }

        /// <summary>
        /// The text for the logout button
        /// </summary>
        public string LogoutButtonText { get; set; }

        #region Transactional Properties

        /// <summary>
        /// Indicates if the first name is current being saved
        /// </summary>
        public bool FirstNameIsSaving { get; set; }

        /// <summary>
        /// Indicates if the last name is current being saved
        /// </summary>
        public bool LastNameIsSaving { get; set; }

        /// <summary>
        /// Indicates if the username is current being saved
        /// </summary>
        public bool UsernameIsSaving { get; set; }

        /// <summary>
        /// Indicates if the email is current being saved
        /// </summary>
        public bool EmailIsSaving { get; set; }

        /// <summary>
        /// Indicates if the password is current being changed
        /// </summary>
        public bool PasswordIsChanging { get; set; }

        #endregion

        #endregion

        #region Public Commands

        /// <summary>
        /// The command to open the settings menu
        /// </summary>
        public ICommand OpenCommand { get; set; }

        /// <summary>
        /// The command to close the settings menu
        /// </summary>
        public ICommand CloseCommand { get; set; }

        /// <summary>
        /// The command to logout of the application
        /// </summary>
        public ICommand LogoutCommand { get; set; }

        /// <summary>
        /// The command to clear the users data from the view model
        /// </summary>
        public ICommand ClearUserDataCommand { get; set; }

        /// <summary>
        /// Loads the settings data from the client data store
        /// </summary>
        public ICommand LoadCommand { get; set; }

        /// <summary>
        /// Saves the current first name to the server
        /// </summary>
        public ICommand SaveFirstNameCommand { get; set; }

        /// <summary>
        /// Saves the current last name to the server
        /// </summary>
        public ICommand SaveLastNameCommand { get; set; }

        /// <summary>
        /// Saves the current username to the server
        /// </summary>
        public ICommand SaveUsernameCommand { get; set; }

        /// <summary>
        /// Saves the current email to the server
        /// </summary>
        public ICommand SaveEmailCommand { get; set; }

        #endregion

        #region Commands Methods

        /// <summary>
        /// Open the settings menu
        /// </summary>
        public void Open()
        {
            // Close settings menu
            ViewModelApplication.SettingsMenuVisible = true;
        }

        /// <summary>
        /// Closes the settings menu
        /// </summary>
        public void Close()
        {
            // Close settings menu
            ViewModelApplication.SettingsMenuVisible = false;
        }

        /// <summary>
        /// Logs the user out
        /// </summary>
        public async Task LogoutAsync()
        {
            //TODO: Confirm the user wants to logout

            //Clear any user data/cache
            await ClientDataStore.ClearAllLoginCredentialsAsync();

            //Clean all application level view models that contain
            //any information about the current user
            ClearUserData();

            // Go to login page
            ViewModelApplication.GoToPage(ApplicationPage.Login);
        }

        /// <summary>
        /// Clears any data specific to the current user
        /// </summary>
        public void ClearUserData()
        {
            //Clear all view models containing the users info
            FirstName.OriginalText = loadingText;
            LastName.OriginalText = loadingText;
            Username.OriginalText = loadingText;            
            Email.OriginalText = loadingText;
        }

        /// <summary>
        /// Sets the settings view model properties based on the data in the client data store
        /// </summary>
        public async Task LoadAsync()
        {  
            // Update values from local cache
            await UpdateValuesFromLocalStoreAsync();

            var token = (await ClientDataStore.GetLoginCredentialsAsync()).Token;

            // If we dont have a token (so we are not logged in...)
            if (string.IsNullOrEmpty(token))
            {
                // Then do nothing more
                return;
            }

            // Load user profile details from server
            var result = await WebRequests.PostAsync<ApiResponse<UserProfileDetailsApiModel>>(
                "http://localhost:58727/api/user/profile", 
                bearerToken: token);

            // If it was successful...
            if (result.Successful)
            {
                // TODO: Should we check if the values are diffent before saving?

                // Create data model from the response
                var dataModel = result.ServerResponse.ResponseT.ToLoginCredentialsDataModel();

                // Re-add our known token
                dataModel.Token = token;
                
                // Save the new information in the data store
                await ClientDataStore.SaveLoginCredentialsAsync(dataModel);

                // Update values from local cache
                await UpdateValuesFromLocalStoreAsync();
            }
        }

        /// <summary>
        /// Saves the new first Name to the server
        /// </summary>
        /// <param name="self">The details of the view model</param>
        /// <returns>Returns true if successful, false otherwise</returns>
        public async Task<bool> SaveFirstNameAsync()
        {
            // Lock this command to ignore any other request while processing
            return await RunCommandAsync(() => FirstNameIsSaving, async () =>
            {
                // Update the First Name value on the server...
                return await UpdateUserCredentialsValueAsync(
                    // Display name
                    "First Name",
                    // Update the first name
                    (credentials) => credentials.FirstName,
                    // To new value
                    FirstName.OriginalText,
                    // Set Api model value
                    (apiModel, value) => apiModel.FirstName = value);                              
            });
        }

        /// <summary>
        /// Saves the new last Name to the server
        /// </summary>
        /// <param name="self">The details of the view model</param>
        /// <returns>Returns true if successful, false otherwise</returns>
        public async Task<bool> SaveLastNameAsync()
        {
            // Lock this command to ignore any other request while processing
            return await RunCommandAsync(() => LastNameIsSaving, async () =>
            {
                // Update the First Name value on the server...
                return await UpdateUserCredentialsValueAsync(
                    // Display name
                    "Last Name",
                    // Update the first name
                    (credentials) => credentials.LastName,
                    // To new value
                    LastName.OriginalText,
                    // Set Api model value
                    (apiModel, value) => apiModel.LastName = value);
            });
        }

        /// <summary>
        /// Saves the new Username to the server
        /// </summary>
        /// <param name="self">The details of the view model</param>
        /// <returns>Returns true if successful, false otherwise</returns>
        public async Task<bool> SaveUsernameAsync()
        {
            // Lock this command to ignore any other request while processing
            return await RunCommandAsync(() => UsernameIsSaving, async () =>
            {
                // Update the First Name value on the server...
                return await UpdateUserCredentialsValueAsync(
                    // Display name
                    "Username",
                    // Update the first name
                    (credentials) => credentials.Username,
                    // To new value
                    Username.OriginalText,
                    // Set Api model value
                    (apiModel, value) => apiModel.Username = value);
            });
        }

        /// <summary>
        /// Saves the new Email to the server
        /// </summary>
        /// <param name="self">The details of the view model</param>
        /// <returns>Returns true if successful, false otherwise</returns>
        public async Task<bool> SaveEmailAsync()
        {
            // Lock this command to ignore any other request while processing
            return await RunCommandAsync(() => EmailIsSaving, async () =>
            {
                // Update the First Name value on the server...
                return await UpdateUserCredentialsValueAsync(
                    // Display name
                    "Email",
                    // Update the first name
                    (credentials) => credentials.Email,
                    // To new value
                    Email.OriginalText,
                    // Set Api model value
                    (apiModel, value) => apiModel.Email = value);
            });
        }

        /// <summary>
        /// Saves the new Password to the server
        /// </summary>
        /// <param name="self">The details of the view model</param>
        /// <returns>Returns true if successful, false otherwise</returns>
        public async Task<bool> SavePasswordAsync()
        {
            // TODO: Update with server
            await Task.Delay(3000);

            // Return succes
            return true;
        }

        #endregion

        #region Private Hepler Methods


        /// <summary>
        /// Loads the settings from the local data store and binds them
        /// to this view model
        /// </summary>
        /// <returns></returns>
        private async Task UpdateValuesFromLocalStoreAsync()
        {
            // Get the stored credentials
            var storedCredentials = await ClientDataStore.GetLoginCredentialsAsync();

            // Set first name
            FirstName.OriginalText = storedCredentials?.FirstName;

            // Set last name
            LastName.OriginalText = storedCredentials?.LastName;

            // Set username
            Username.OriginalText = storedCredentials?.Username;

            // Set email
            Email.OriginalText = storedCredentials?.Email;
        }

        /// <summary>
        /// Updates a specific value from the client data store for the user profile details
        /// and attempts to update the server to match those details.
        /// For example, updating the first name of the user.
        /// </summary>
        /// <param name="displayName">The display name for logging and display purposes of the property we are updating</param>
        /// <param name="propertyToUpdate">The property from the <see cref="LoginCredentialsDataModel"/> to be updated</param>
        /// <param name="newValue">The new value to update the property to</param>
        /// <param name="setApiModel">Sets the correct property in the <see cref="UpdateUserProfileApiModel"/> model that this property maps to</param>
        /// <returns></returns>
        private async Task<bool> UpdateUserCredentialsValueAsync(string displayName, Expression<Func<LoginCredentialsDataModel, string>> propertyToUpdate, string newValue, Action<UpdateUserProfileApiModel, string> setApiModel)
        {
            // Log it
            Logger.LogDebugSource($"Saving {displayName}...");

            // Get the current known credentials
            var credentials = await ClientDataStore.GetLoginCredentialsAsync();

            // Get the property to update from credentials
            var toUpdate = propertyToUpdate.GetPropertyValue(credentials);

            // Log it
            Logger.LogDebugSource($"{displayName} currently {toUpdate}, updating to {newValue}");

            // Check if the value is the same. If so...
            if (toUpdate == newValue)
            {
                // Log it
                Logger.LogDebugSource($"{displayName} is the same, ignoring");

                // Return true
                return true;
            }

            // Set the property
            propertyToUpdate.SetPropertyValue(newValue, credentials);

            // Create update details
            var updateApiModel = new UpdateUserProfileApiModel();

            // Ask caller to set apropriate value
            // Set the new first name
            setApiModel(updateApiModel, newValue);

            // Update the server with the details
            var result = await WebRequests.PostAsync<ApiResponse>(
               "http://localhost:58727/api/user/profile/update",
               // Pass the api model
               updateApiModel,
               // Create the user details to send
               bearerToken: credentials.Token);

            // If the response has an error...
            if (await result.DisplayErrorIfFailedAsync($"Update {displayName}"))
            {
                // Log it
                Logger.LogDebugSource($"Failed to update {displayName}. {result.ErrorMessage}");

                // Return false
                return false;
            }

            // Log it
            Logger.LogDebugSource($"Succesfully updated {displayName}. Saving to local database cahche...");

            // Store the new user credentials the data store
            await ClientDataStore.SaveLoginCredentialsAsync(credentials);

            // Return successful
            return true;
        }

        #endregion
    }
}
