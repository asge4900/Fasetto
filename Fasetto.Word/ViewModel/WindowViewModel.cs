using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Fasetto.Word
{
    /// <summary>
    /// The view Model for the custom flat window
    /// </summary>
    public class WindowViewModel : BaseViewModel
    {
        #region Fields

        /// <summary>
        /// The window this view model controls
        /// </summary>
        private Window window;

        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        private int outerMarginSize = 10;

        /// <summary>
        /// The radius of the egdes of the window
        /// </summary>
        private int windowRadius = 10;

        #endregion        

        #region Constructor

        /// <summary>
        /// Default constuctor
        /// </summary>
        public WindowViewModel(Window window)
        {
            this.window = window;

            //Listen out for the window resizing
            this.window.StateChanged += (sender, e) =>
            {
                //Fire off events of all properties that are affected by a resize
                OnPropertyChanged(nameof(ResizeBorderThickness));
                OnPropertyChanged(nameof(OuterMarginSize));
                OnPropertyChanged(nameof(OuterMargninSizeThickness));
                OnPropertyChanged(nameof(WindowRadius));
                OnPropertyChanged(nameof(WindowCornerRadius));
            };

            //Create commands
            MinimizeCommand = new RelayCommand(() => this.window.WindowState = WindowState.Maximized);
            MaximizeCommand = new RelayCommand(() => this.window.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => this.window.Close());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(this.window, GetMousePosition()));
        }

        #endregion

        #region Private Helpers

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public int X;
            public int Y;
        };

        /// <summary>
        /// Gets the current mouse position on the screen
        /// </summary>
        /// <returns></returns>
        private static Point GetMousePosition()
        {
            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            return new Point(w32Mouse.X, w32Mouse.Y);
        }

        #endregion


        #region Properties

        /// <summary>
        /// The size of the resize border around the window
        /// </summary>
        public int ResizeBorder { get; set; } = 6;

        /// <summary>
        /// The size of the resize border around the window, taking into account the outer margin
        /// </summary>
        public Thickness ResizeBorderThickness => new Thickness(ResizeBorder + OuterMarginSize);

        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        public int OuterMarginSize 
        { 
            get
            { 
                return window.WindowState == WindowState.Maximized ? 0 : outerMarginSize; 
            
            } 
            set 
            { 
                outerMarginSize = value; 
            } 
        }

        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        public Thickness OuterMargninSizeThickness => new Thickness(OuterMarginSize);

        /// <summary>
        /// The radius of the edges of the window
        /// </summary>
        public int WindowRadius
        {
            get
            {
                return window.WindowState == WindowState.Maximized ? 0 : windowRadius;

            }
            set
            {
                windowRadius = value;
            }
        }

        /// <summary>
        /// The radius of the edges of the window
        /// </summary>
        public CornerRadius WindowCornerRadius => new CornerRadius(WindowRadius);

        /// <summary>
        /// The height of the title bar / caption of the window
        /// </summary>
        public int TitleHeight { get; set; } = 42;

        /// <summary>
        /// The height of the title bar / caption of the window
        /// </summary>
        public GridLength TitleHeightGridLength => new GridLength(TitleHeight + ResizeBorder);

        #endregion

        #region Commands

        /// <summary>
        /// The command to minimize the window
        /// </summary>
        public ICommand MinimizeCommand { get; set; }

        /// <summary>
        /// The command to maximize the window
        /// </summary>
        public ICommand MaximizeCommand { get; set; }

        /// <summary>
        /// The command to close the window
        /// </summary>
        public ICommand CloseCommand { get; set; }

        /// <summary>
        /// The command to show the system menu of the window
        /// </summary>
        public ICommand MenuCommand { get; set; }

        #endregion
    }
}
