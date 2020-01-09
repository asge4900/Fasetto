﻿using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Fasetto.Word
{
    /// <summary>
    /// Scroll an item control to the bottom when the data context changes
    /// </summary>
    public class ScrollToBottomOnLoadProperty : BaseAttachedProperty<ScrollToBottomOnLoadProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // Don't do this in design time
            if (DesignerProperties.GetIsInDesignMode(sender))
            {
                return;
            }

            //If we don't have a control, return
            if (!(sender is ScrollViewer control))
            {
                return;
            }

            // Scroll content to bottom when context changes
            control.DataContextChanged -= Control_DataContextChanged;
            control.DataContextChanged += Control_DataContextChanged;
        }

        private void Control_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // Scroll to bottom
            (sender as ScrollViewer).ScrollToBottom();
        }
    }    
}
