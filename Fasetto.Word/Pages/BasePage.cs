using System.Windows.Controls;
using System.Windows;
using System.Threading.Tasks;
using Fasetto.Word.Lib;

namespace Fasetto.Word
{
    /// <summary>
    /// A base page for all pages to gain base functionality
    /// </summary>
    public class BasePage<VM> : Page where VM : BaseViewModel, new()
    {
        #region Fields

        /// <summary>
        /// The animation that play when the page is first loaded
        /// </summary>
        private VM viewModel; 
        
        #endregion

        #region Constuctor

        /// <summary>
        /// Default constructor
        /// </summary>
        public BasePage()
        {
            //If we are animated in, hide to begin with
            if (PageLoadAnimation != PageAnimation.None)
            {
                Visibility = Visibility.Collapsed;
            }

            //Listen out for the page loading
            Loaded += BasePage_Loaded;

            //Create a default view model
            ViewModel = new VM();
        }

        #endregion

        #region Properties

        /// <summary>
        /// The animation that play when the page is first loaded
        /// </summary>
        public PageAnimation PageLoadAnimation { get; set; } = PageAnimation.SlideAndFadeInFromRight;

        /// <summary>
        /// The animation that play when the page is unloaded
        /// </summary>
        public PageAnimation PageUnloadAnimation { get; set; } = PageAnimation.SlideAndFadeOutToLeft;

        /// <summary>
        /// The time any slide animation takes to complete
        /// </summary>
        public float SlideSeconds { get; set; } = 0.8f;

        /// <summary>
        /// The View Model associated with this page
        /// </summary>
        public VM ViewModel 
        { 
            get => viewModel;
            set
            {
                //If nothning has changes, return
                if (viewModel == value)
                {
                    return;
                }

                //Update the value
                viewModel = value;

                //Set the data context for this page
                DataContext = viewModel;
            }
        }

        #endregion

        #region Animation Load / Unload

        /// <summary>
        /// Once the page is loaded, perform any required animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            //Animate the page in
            await AnimateIn();
        }

        /// <summary>
        /// Animates the page in
        /// </summary>
        /// <returns></returns>
        public async Task AnimateIn()
        {
            //Make sure we have something to do
            if (PageLoadAnimation == PageAnimation.None)
            {
                return;
            }

            switch (PageLoadAnimation)
            {
                //Start the animation
                case PageAnimation.SlideAndFadeInFromRight:

                    await this.SlideAndFadeInFromRight(SlideSeconds);

                    break;

                    //case PageAnimation.SlideAndFadeOutToLeft:
                    //    break;

                    //default:
                    //    break;
            }
        }

        /// <summary>
        /// Animates the page out
        /// </summary>
        /// <returns></returns>
        public async Task AnimateOut()
        {
            //Make sure we have something to do
            if (PageUnloadAnimation == PageAnimation.None)
            {
                return;
            }

            switch (PageUnloadAnimation)
            {
                //Start the animation
                case PageAnimation.SlideAndFadeOutToLeft:

                    await this.SlideAndFadeOutToLeft(SlideSeconds);

                    break;
            }
        }

        #endregion
    }
}
