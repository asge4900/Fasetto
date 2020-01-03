using Fasetto.Word.Lib;
using System.Windows;

namespace Fasetto.Word
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new WindowViewModel(this);
        }
    }
}
