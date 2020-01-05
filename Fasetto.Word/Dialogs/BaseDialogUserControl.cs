using Fasetto.Word.Lib;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Fasetto.Word
{
    /// <summary>
    /// The base class for any content that is being used inside of a <see cref="DialogWindow"/>
    /// </summary>
    public class BaseDialogUserControl : UserControl
    {
        #region Fields

        /// <summary>
        /// The dialog window we will be contained within
        /// </summary>
        private DialogWindow dialogWindow;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseDialogUserControl()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                // Create a new dialog window
                dialogWindow = new DialogWindow();
                dialogWindow.ViewModel = new DialogWindowViewModel(dialogWindow);

                // Create close command
                CloseCommand = new RelayCommand(() => dialogWindow.Close());
            }
        }

        #endregion

        #region Public Commands

        /// <summary>
        /// Closes this dialog
        /// </summary>
        public ICommand CloseCommand { get; private set; }

        #endregion

        #region Public Properties

        /// <summary>
        /// The minimum width of this dialog
        /// </summary>
        public int WindowMinimumWidth { get; set; } = 250;

        /// <summary>
        /// The minimum height of this dialog
        /// </summary>
        public int WindowMinimumHeight { get; set; } = 100;

        /// <summary>
        /// The height of the title bar
        /// </summary>
        public int TitleHeight { get; set; } = 30;

        /// <summary>
        /// The title for this dialog
        /// </summary>
        public string Title { get; set; }

        #endregion       

        #region Public Dialog Show Methods

        /// <summary>
        /// Displays a single message box to the user
        /// </summary>
        /// <param name="viewModel">The view model</param>
        /// <typeparam name="T">The view model type for this control</typeparam>
        /// <returns></returns>
        public Task ShowDialog<T>(T viewModel)
            where T : BaseDialogViewModel
        {
            // Create a task to await the dialog closing
            var tcs = new TaskCompletionSource<bool>();

            // Run on UI thread
            Application.Current.Dispatcher.Invoke(() =>
            {
                try
                {
                    // Match controls expected sizes to the dialog windows view model
                    dialogWindow.ViewModel.WindowMinimumWidth = WindowMinimumWidth;
                    dialogWindow.ViewModel.WindowMinimumHeight = WindowMinimumHeight;
                    dialogWindow.ViewModel.TitleHeight = TitleHeight;
                    dialogWindow.ViewModel.Title = string.IsNullOrEmpty(viewModel.Title) ? Title : viewModel.Title;

                    // Set this control to the dialog window content
                    dialogWindow.ViewModel.Content = this;

                    // Setup this controls data context binding to the view model
                    DataContext = viewModel;

                    // Show dialog
                    dialogWindow.ShowDialog();
                }
                finally
                {
                    // Let caller know we finished
                    tcs.TrySetResult(true);
                }
            });

            return tcs.Task;
        }

        #endregion
    }
}
