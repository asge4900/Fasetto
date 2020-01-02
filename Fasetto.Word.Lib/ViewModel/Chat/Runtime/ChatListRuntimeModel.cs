using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasetto.Word.Lib
{
    public class ChatListRuntimeModel : ChatListViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of the design model
        /// </summary>
        public static ChatListRuntimeModel Instance => new ChatListRuntimeModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ChatListRuntimeModel()
        {            
            //Items = new List<ChatListItemViewModel>();

            //using (var db = new AppDbContext())
            //{
            //    foreach (var item in db.ChatListItems)
            //    {
            //        var chatItem = new ChatListItemViewModel
            //        {
            //            Name = item.FirstName,
            //            Initials = item.Initials,
            //            Message = item.ChatMessage,
            //            ProfilePictureRGB = item.ProfilePictureRGB
            //        };

            //        Items.Add(chatItem);+
            //    }
            //}
        }
        #endregion
    }
}
