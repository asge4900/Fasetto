using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasetto.Word
{
    /// <summary>
    /// A base view model that fires Property Changed events as needed
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The event that is fired when any child propery changes its value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Call this to fire <see cref="PropertyChanged"/> event
        /// </summary>
        /// <param name="name"></param>
        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
