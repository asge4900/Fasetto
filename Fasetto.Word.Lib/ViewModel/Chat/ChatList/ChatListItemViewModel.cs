using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Fasetto.Word.Lib
{
    /// <summary>
    /// A view model for each chat list item in the overview chat list
    /// </summary>
    public class ChatListItemViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ChatListItemViewModel()
        {
            //Create commands
            OpenMessageCommand = new RelayCommand(OpenMessage);
        }

        #endregion

        #region Properperties

        /// <summary>
        /// The display name of this chat list
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The initials to show for the profile picture background
        /// </summary>
        public string Initials { get; set; }

        /// <summary>
        /// The latest message from this chat
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The RGB values (in hex) for the background color of the profile picture
        /// For example FF00FF for Red and Blue mixed
        /// </summary>
        public string ProfilePictureRGB { get; set; }

        /// <summary>
        /// True if there are unread messages in this chat 
        /// </summary>
        public bool NewContentAvailable { get; set; }

        /// <summary>
        /// True if this item is currently selected
        /// </summary>
        public bool IsSelected { get; set; }

        #endregion

        #region Public Commands

        /// <summary>
        /// Opens the current message thread
        /// </summary>
        public ICommand OpenMessageCommand { get; set; }

        #endregion

        #region Command methods

        private void OpenMessage()
        {
            IoC.Application.GoToPage(ApplicationPage.Chat, new ChatMessageListViewModel
            {
                DisplayTitle = "Parnell, Me",
                
                Items = new ObservableCollection<ChatMessageListItemViewModel>
                {
                    new ChatMessageListItemViewModel
                    {
                        Message = Message,
                        Initials = Initials,
                        MessageSentTime = DateTime.UtcNow,
                        ProfilePictureRGB = "FF00FF",
                        SenderName = "Luke",
                        SendByMe = true
                    },
                    new ChatMessageListItemViewModel
                    {
                        Message = "A recevied message",
                        Initials = Initials,
                        MessageSentTime = DateTime.UtcNow,
                        ProfilePictureRGB = "FF0000",
                        SenderName = "Parenel",
                        SendByMe = false
                    },
                     new ChatMessageListItemViewModel
                    {
                        Message = "A recevied message",
                        Initials = Initials,
                        MessageSentTime = DateTime.UtcNow,
                        ProfilePictureRGB = "FF0000",
                        SenderName = "Parenel",
                        SendByMe = false
                    },
                     new ChatMessageListItemViewModel
                    {
                        Message = Message,
                        Initials = Initials,
                        MessageSentTime = DateTime.UtcNow,
                        ProfilePictureRGB = "FF00FF",
                        SenderName = "Luke",
                        SendByMe = true
                    },
                    new ChatMessageListItemViewModel
                    {
                        Message = "A recevied message",
                        Initials = Initials,
                        MessageSentTime = DateTime.UtcNow,
                        ProfilePictureRGB = "FF0000",
                        SenderName = "Parenel",
                        SendByMe = false
                    },
                     new ChatMessageListItemViewModel
                    {
                        Message = "A recevied message",
                        ImageAttachment = new ChatMessageListItemImageAttachmentViewModel
                        {
                            ThumbnailUrl = "http://anywhere"
                        },
                        Initials = Initials,
                        MessageSentTime = DateTime.UtcNow,
                        ProfilePictureRGB = "FF0000",
                        SenderName = "Parenel",
                        SendByMe = false
                    }
                }
            });
        }

        #endregion
    }
}
