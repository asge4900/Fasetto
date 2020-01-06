using Fasetto.Word.Lib;
using System.Windows.Controls;

namespace Fasetto.Word
{
    /// <summary>
    /// Interaction logic for SettingsControl.xaml
    /// </summary>
    public partial class SettingsControl : UserControl
    {
        public SettingsControl()
        {
            InitializeComponent();

            //Set data context to settings view model
            DataContext = IoC.Settings;
        }
    }
}
