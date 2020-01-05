using System.Windows;

namespace Fasetto.Word
{
    /// <summary>
    /// Interaction logic for DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        #region Fields

        /// <summary>
        /// The view model for this window
        /// </summary>
        private DialogWindowViewModel viewModel;

        #endregion


        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public DialogWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The view model for this window
        /// </summary>
        public DialogWindowViewModel ViewModel
        {
            get => viewModel;
            set
            {
                // Set new value
                viewModel = value;

                // Update data context
                DataContext = viewModel;
            }
        }

        #endregion

    }
}
