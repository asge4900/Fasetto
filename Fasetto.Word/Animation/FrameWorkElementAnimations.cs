using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Fasetto.Word
{
    /// <summary>
    /// Helpers to animate framework elements in specific ways
    /// </summary>
    public static class FrameWorkElementAnimations
    {
        #region Slide from/to right       

        /// <summary>
        /// Slides an element in from the right
        /// </summary>
        /// <param name="element">The element to animate</param>
        /// <param name="seconds">The time the animation will take</param>
        /// <param name="keepMargin">Whether to keep the element at the same width during animation</param>
        /// <returns></returns>
        public static async Task SlideAndFadeInFromRightAsync(this FrameworkElement element, float seconds = 0.3f, bool keepMargin = true)
        {
            // Create the storyboard
            var sb = new Storyboard();

            // Add slide from right animation
            sb.AddSlideFromRight(seconds, element.ActualWidth, keepMargin: keepMargin);

            // Add fade in animation
            sb.AddFadeIn(seconds);

            // Start animating
            sb.Begin(element);

            // Make page visible only if we are animating
            if (seconds != 0)
            {
                element.Visibility = Visibility.Visible;
            }

            // Wait for it to finish
            await Task.Delay((int)(seconds * 1000));
        }

        /// <summary>
        /// Slides an element out to the right
        /// </summary>
        /// <param name="element">The element to animate</param>
        /// <param name="seconds">The time the animation will take</param>
        /// <param name="keepMargin">Whether to keep the element at the same width during animation</param>
        /// <returns></returns>
        public static async Task SlideAndFadeOutToRightAsync(this FrameworkElement element, float seconds = 0.3f, bool keepMargin = true)
        {
            // Create the storyboard
            var sb = new Storyboard();

            // Add slide to right animation
            sb.AddSlideToRight(seconds, element.ActualWidth, keepMargin: keepMargin);

            // Add fade in animation
            sb.AddFadeOut(seconds);

            // Start animating
            sb.Begin(element);

            // Make page visible only if we are animating
            if (seconds != 0)
            {
                element.Visibility = Visibility.Visible; 
            }

            // Wait for it to finish
            await Task.Delay((int)(seconds * 1000));

            // Make element invisible
            element.Visibility = Visibility.Hidden;
        }

        #endregion

        #region Slide from/to Left

        /// <summary>
        /// Slides an element in from the left
        /// </summary>
        /// <param name="element">The element to animate</param>
        /// <param name="seconds">The time the animation will take</param>
        /// <param name="keepMargin">Whether to keep the element at the same width during animation</param>
        /// <returns></returns>
        public static async Task SlideAndFadeInFromLeftAsync(this FrameworkElement element, float seconds = 0.3f, bool keepMargin = true)
        {
            // Create the storyboard
            var sb = new Storyboard();

            // Add slide from left animation
            sb.AddSlideFromLeft(seconds, element.ActualWidth, keepMargin: keepMargin);

            // Add fade in animation
            sb.AddFadeIn(seconds);

            // Start animating
            sb.Begin(element);

            // Make page visible only if we are animating
            if (seconds != 0)
            {
                element.Visibility = Visibility.Visible;
            }

            // Wait for it to finish
            await Task.Delay((int)(seconds * 1000));
        }

        /// <summary>
        /// Slides an element out to the left
        /// </summary>
        /// <param name="element">The element to animate</param>
        /// <param name="seconds">The time the animation will take</param>
        /// <param name="keepMargin">Whether to keep the element at the same width during animation</param>
        /// <returns></returns>
        public static async Task SlideAndFadeOutToLeftAsync(this FrameworkElement element, float seconds = 0.3f, bool keepMargin = true)
        {
            // Create the storyboard
            var sb = new Storyboard();

            // Add slide to left animation
            sb.AddSlideToLeft(seconds, element.ActualWidth, keepMargin: keepMargin);

            // Add fade in animation
            sb.AddFadeOut(seconds);

            // Start animating
            sb.Begin(element);

            // Make page visible only if we are animating
            if (seconds != 0)
            {
                element.Visibility = Visibility.Visible;
            }

            // Wait for it to finish
            await Task.Delay((int)(seconds * 1000));

            // Make element invisible
            element.Visibility = Visibility.Hidden;
        }

        #endregion

        #region Slide from/to buttom

        /// <summary>
        /// Slides an element in from the buttom
        /// </summary>
        /// <param name="element">The element to animate</param>
        /// <param name="seconds">The time the animation will take</param>
        /// <param name="keepMargin">Whether to keep the element at the same height during animation</param>
        /// <returns></returns>
        public static async Task SlideAndFadeInFromBottomAsync(this FrameworkElement element, float seconds = 0.3f, bool keepMargin = true)
        {
            // Create the storyboard
            var sb = new Storyboard();

            // Add slide from buttom animation
            sb.AddSlideFromBottom(seconds, element.ActualHeight, keepMargin: keepMargin);

            // Add fade in animation
            sb.AddFadeIn(seconds);

            // Start animating
            sb.Begin(element);

            // Make page visible only if we are animating
            if (seconds != 0)
            {
                element.Visibility = Visibility.Visible;
            }

            // Wait for it to finish
            await Task.Delay((int)(seconds * 1000));
        }

        /// <summary>
        /// Slides an element out to the buttom
        /// </summary>
        /// <param name="element">The element to animate</param>
        /// <param name="seconds">The time the animation will take</param>
        /// <param name="keepMargin">Whether to keep the element at the same height during animation</param>
        /// <returns></returns>
        public static async Task SlideAndFadeOutToBottomAsync(this FrameworkElement element, float seconds = 0.3f, bool keepMargin = true)
        {
            // Create the storyboard
            var sb = new Storyboard();

            // Add slide to buttom animation
            sb.AddSlideToBottom(seconds, element.ActualHeight, keepMargin: keepMargin);

            // Add fade in animation
            sb.AddFadeOut(seconds);

            // Start animating
            sb.Begin(element);

            // Make page visible only if we are animating
            if (seconds != 0)
            {
                element.Visibility = Visibility.Visible;
            }

            // Wait for it to finish
            await Task.Delay((int)(seconds * 1000));

            // Make element invisible
            element.Visibility = Visibility.Hidden;
        }

        #endregion       

        #region Fade in/out

        /// <summary>
        /// Fades an element in
        /// </summary>
        /// <param name="element">The element to animate</param>
        /// <param name="seconds">The time the animation will take</param>
        /// <returns></returns>
        public static async Task FadeInAsync(this FrameworkElement element, float seconds = 0.3f)
        {
            // Create the storyboard
            var sb = new Storyboard();           

            // Add fade in animation
            sb.AddFadeIn(seconds);

            // Start animating
            sb.Begin(element);

            // Make page visible only if we are animating
            if (seconds != 0)
            {
                element.Visibility = Visibility.Visible;
            }

            // Wait for it to finish
            await Task.Delay((int)(seconds * 1000));
        }

        /// <summary>
        /// Fades an element out
        /// </summary>
        /// <param name="element">The element to animate</param>
        /// <param name="seconds">The time the animation will take</param>
        /// <returns></returns>
        public static async Task FadeOutAsync(this FrameworkElement element, float seconds = 0.3f)
        {
            // Create the storyboard
            var sb = new Storyboard();

            // Add fade in animation
            sb.AddFadeOut(seconds);

            // Start animating
            sb.Begin(element);

            // Make page visible only if we are animating
            if (seconds != 0)
            {
                element.Visibility = Visibility.Visible;
            }

            // Wait for it to finish
            await Task.Delay((int)(seconds * 1000));

            //Fully hide the element
            element.Visibility = Visibility.Collapsed;
        }

        #endregion

        #region Marquee

        /// <summary>
        /// Animates a marquee style element
        /// The structure should be:
        /// [Border ClipToBounds="True"]
        ///   [Border local:AnimateMarqueeProperty.Value="True"]
        ///      [Content HorizontalAlignment="Left"]
        ///   [/Border]
        /// [/Border]
        /// </summary>
        /// <param name="element">The element to animate</param>
        /// <param name="seconds">The time the animation will take</param>
        /// <returns></returns>
        public static void MarqueeAsync(this FrameworkElement element, float seconds = 3f)
        {
            // Create the storyboard
            var sb = new Storyboard();

            // Run until element is unloaded
            var unloaded = false;

            // Monitor for element unloading
            element.Unloaded += (s, e) => unloaded = true;

            // Run a loop off the caller thread
            Task.Run(async () =>
            {
                // While the element is still available, recheck the size
                // after every loop in case the container was resized
                while (element != null && !unloaded)
                {
                    // Create width variables
                    var width = 0d;
                    var innerWidth = 0d;

                    try
                    {
                        // Check if element is still loaded
                        if (element == null || unloaded)
                            break;

                        // Try and get current width
                        width = element.ActualWidth;
                        innerWidth = ((element as Border).Child as FrameworkElement).ActualWidth;
                    }
                    catch
                    {
                        // Any issues then stop animating (presume element destroyed)
                        break;
                    }

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        // Add marquee animation
                        sb.AddMarquee(seconds, width, innerWidth);

                        // Start animating
                        sb.Begin(element);

                        // Make page visible
                        element.Visibility = Visibility.Visible;
                    });

                    // Wait for it to finish animating
                    await Task.Delay((int)seconds * 1000);

                    // If this is from first load or zero seconds of animation, do not repeat
                    if (seconds == 0)
                        break;
                }
            });
        }

        #endregion
    }
}
