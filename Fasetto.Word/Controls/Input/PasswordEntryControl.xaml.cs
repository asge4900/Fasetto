﻿using Fasetto.Word.Lib;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Fasetto.Word
{
    /// <summary>
    /// Interaction logic for PasswordEntryControl.xaml
    /// </summary>
    public partial class PasswordEntryControl : UserControl
    {

        #region Dependency properties

        /// <summary>
        /// The label width of the control
        /// </summary>
        public GridLength LabelWidth
        {
            get => (GridLength)GetValue(LengthProperty);
            set => SetValue(LengthProperty, value);
        }

        // Using a DependencyProperty as the backing store for Length.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LengthProperty =
            DependencyProperty.Register(nameof(LabelWidth), typeof(GridLength), typeof(PasswordEntryControl), new PropertyMetadata(GridLength.Auto, LabelWidthChangeCallback));        

        #endregion

        #region Constructor

        /// <summary>
        /// Default construcor
        /// </summary>
        public PasswordEntryControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Dependency Callbacks

        /// <summary>
        /// Called when the label width has changed
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void LabelWidthChangeCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {  
                //Set the column definition width to the new value
                (d as PasswordEntryControl).LabelColumnDefinition.Width = (GridLength)e.NewValue;
            }
            catch (Exception) 
            {
                //Make developer aware of potential issue
                Debugger.Break();

                (d as PasswordEntryControl).LabelColumnDefinition.Width = GridLength.Auto;
            }
        }
        #endregion

        /// <summary>
        /// Update the view model value with the new password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Update view model
            if (DataContext is PasswordEntryViewModel viewModel)
                viewModel.CurrentPassword = CurrentPassword.SecurePassword;
        }

        /// <summary>
        /// Update the view model value with the new password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Update view model
            if (DataContext is PasswordEntryViewModel viewModel)
                viewModel.NewPassword = NewPassword.SecurePassword;
        }

        /// <summary>
        /// Update the view model value with the new password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Update view model
            if (DataContext is PasswordEntryViewModel viewModel)
                viewModel.ConfirmPassword = ConfirmPassword.SecurePassword;
        }
    }
}
