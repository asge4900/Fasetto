using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Fasetto.Word
{
    /// <summary>
    /// A view model for a chat message thread list
    /// </summary>
    public class ChatMessageListViewModel : BaseViewModel
    {
        #region Protected Members

        /// <summary>
        /// The last searched text in this list
        /// </summary>
        protected string lastSearchText;

        /// <summary>
        /// The text to search for in the search command
        /// </summary>
        protected string searchText;

        /// <summary>
        /// The chat thread list items for the list
        /// </summary>
        protected ObservableCollection<ChatMessageListItemViewModel> items;

        /// <summary>
        /// A flag indicating if the search dialog is open
        /// </summary>
        protected bool searchIsOpen;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ChatMessageListViewModel()
        {
            //Create commands
            AttachmentButtonCommand = new RelayCommand(AttachmentButton);
            PopupClickawayCommand = new RelayCommand(PopupClickaway);
            SendCommand = new RelayCommand(Send);
            SearchCommand = new RelayCommand(Search);
            OpenSearchCommand = new RelayCommand(OpenSearch);
            CloseSearchCommand = new RelayCommand(CloseSearch);
            ClearSearchCommand = new RelayCommand(ClearSearch);

            //Make a default menu
            AttachmentMenu = new ChatAttachmentPopupMenuViewModel();
        }

        #endregion

        #region Properties

        /// <summary>
        /// The chat thread list items for the list
        /// NOTE: Do not call Items.Add to add message to this list
        ///       as it will make the FilteredItems out of sync
        /// </summary>
        public ObservableCollection<ChatMessageListItemViewModel> Items 
        {
            get => items;
            set
            {
                // Make sure list has changed
                if (items == value)
                {
                    return;
                }

                // Update value
                items = value;

                // Update filtered list to match
                FilteredItems = new ObservableCollection<ChatMessageListItemViewModel>(items);
            } 
        }

        /// <summary>
        /// The chat thread list items for the list that include any search filtering
        /// </summary>
        public ObservableCollection<ChatMessageListItemViewModel> FilteredItems { get; set; }

        /// <summary>
        /// The title of this chat list 
        /// </summary>
        public string DisplayTitle { get; set; }

        /// <summary>
        /// True to show the attachment menu, false to hide it
        /// </summary>
        public bool AttachmentMenuVisible { get; set; }

        /// <summary>
        /// True if any popup menus are visible
        /// </summary>
        public bool AnyPopupVisible => AttachmentMenuVisible;

        /// <summary>
        /// The view model for the attachment menu
        /// </summary>
        public ChatAttachmentPopupMenuViewModel AttachmentMenu { get; set; }

        /// <summary>
        /// The text for the current message being written
        /// </summary>
        public string PendingMessageText { get; set; }

        /// <summary>
        /// The text to search for when we do a search
        /// </summary>
        public string SearchText 
        {
            get => searchText;
            set
            {
                // Check value is different
                if (searchText == value)
                {
                    return;
                }

                // Update value
                searchText = value;

                if (string.IsNullOrEmpty(SearchText))
                {
                    // Search to restore messages
                    Search();
                }
            }
        }

        /// <summary>
        /// A flag indicating if the search dialog is open
        /// </summary>
        public bool SearchIsOpen 
        {
            get => searchIsOpen;
            set
            {
                // Check value has changed
                if (searchIsOpen == value)
                {
                    return;
                }

                // Update value
                searchIsOpen = value;

                // If dialog closes...
                if (!searchIsOpen)
                {
                    //Clear search text
                    SearchText = string.Empty;
                }
            }
        }

        #endregion

        #region Public Commands

        /// <summary>
        /// The command for when the attachment button is clicked
        /// </summary>
        public ICommand AttachmentButtonCommand { get; set; }

        /// <summary>
        /// The command for when the area outside of any popup is clicked
        /// </summary>
        public ICommand PopupClickawayCommand { get; set; }

        /// <summary>
        /// The command for when the user clicks the send button
        /// </summary>
        public ICommand SendCommand { get; set; }

        /// <summary>
        /// The command for when the user wants to search
        /// </summary>
        public ICommand SearchCommand { get; set; }

        /// <summary>
        /// The command for when the user wants to open the search dialog
        /// </summary>
        public ICommand OpenSearchCommand { get; set; }

        /// <summary>
        /// The command for when the user wants to close the search dialog
        /// </summary>
        public ICommand CloseSearchCommand { get; set; }

        /// <summary>
        /// The command for when the user wants to clear the search text
        /// </summary>
        public ICommand ClearSearchCommand { get; set; }

        #endregion

        #region Command Methods

        /// <summary>
        /// When the attachment button is clicked show/hide the attachment popup
        /// </summary>
        public void AttachmentButton()
        {
            //Toggle menu visibility
            AttachmentMenuVisible ^= true;
        }

        /// <summary>
        /// When the popup clickaway area is clicked hide any popups
        /// </summary>
        public void PopupClickaway()
        {
            //Hide attachment menu
            AttachmentMenuVisible = false;
        }

        /// <summary>
        /// When the user clicks the send button, send the message
        /// </summary>
        public void Send()
        {
            // Don't send a blank message
            if (string.IsNullOrEmpty(PendingMessageText))
            {
                return;
            }

            // Ensure lists are not null
            if (Items == null)
            {
                Items = new ObservableCollection<ChatMessageListItemViewModel>();
            }

            if (FilteredItems == null)
            {
                FilteredItems = new ObservableCollection<ChatMessageListItemViewModel>();
            }

            //Fake send a new message
            var message = new ChatMessageListItemViewModel
            {
                Initials = "LM",
                Message = PendingMessageText,
                MessageSentTime = DateTime.UtcNow,
                SendByMe = true,
                SenderName = "Luke Malpass",
                NewItem = true
            };

            // Add message to both lists
            Items.Add(message);
            FilteredItems.Add(message);

            // Clear the pending message text
            PendingMessageText = string.Empty;
        }

        /// <summary>
        /// Searches the current message list and filters the view
        /// </summary>
        public void Search()
        {
            // Make sure we dont resend the same text
            if ((string.IsNullOrEmpty(lastSearchText) && string.IsNullOrEmpty(SearchText)) || 
                string.Equals(lastSearchText, SearchText))
            {
                return;
            }

            // If we have no search text, or no items
            if (string.IsNullOrEmpty(SearchText) || Items == null || Items.Count <= 0)
            {
                // Make filtered list the same
                FilteredItems = new ObservableCollection<ChatMessageListItemViewModel>(Items);

                // Set last search
                lastSearchText = searchText;

                return;
            }

            // Find all items that contain the given text
            FilteredItems = new ObservableCollection<ChatMessageListItemViewModel>(
                Items.Where(item => item.Message.ToLower().Contains(SearchText)));

            // Set last search
            lastSearchText = searchText;
        }

        /// <summary>
        /// Clears the search text
        /// </summary>
        public void ClearSearch()
        {
            // If there is some search text...
            if (!string.IsNullOrEmpty(SearchText))
            {
                // Clear the text
                SearchText = string.Empty;
            }
            // Otherwise
            else
            {
                // Close search dialog
                SearchIsOpen = false;
            }
        }

        /// <summary>
        /// Opens the search dialog
        /// </summary>
        public void OpenSearch() => SearchIsOpen = true;

        /// <summary>
        /// Closes the search dialog
        /// </summary>
        public void CloseSearch() => SearchIsOpen = false;

        #endregion
    }
}
