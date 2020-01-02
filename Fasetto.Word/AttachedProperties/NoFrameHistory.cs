﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Fasetto.Word
{
    /// <summary>
    /// The NoFrameHistory attached property creating <see cref="Frame"/> that never shows navigation
    /// and keeps the navigation history empty
    /// </summary>
    public class NoFrameHistory : BaseAttachedProperty<NoFrameHistory, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //Get the frame
            var frame = (sender as Frame);

            //Hide navigation bar
            frame.NavigationUIVisibility = NavigationUIVisibility.Hidden;

            //Clear history on navigate
            frame.Navigated += (ss, ee) => ((Frame)ss).NavigationService.RemoveBackEntry();
        }
    }
}
