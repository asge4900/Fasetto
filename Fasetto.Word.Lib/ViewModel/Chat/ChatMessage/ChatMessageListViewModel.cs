using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasetto.Word.Lib
{
    /// <summary>
    /// A view model for a chat message thread list
    /// </summary>
    public class ChatMessageListViewModel : BaseViewModel
    {
        /// <summary>
        /// The chat thread list items for the list
        /// </summary>
        public List<ChatMessageListItemViewModel> Items { get; set; }
    }
}
