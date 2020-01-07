using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Fasetto.Word
{
    /// <summary>
    /// Interaction logic for TextEntryControl.xaml
    /// </summary>
    public partial class TextEntryControl : UserControl
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
            DependencyProperty.Register(nameof(LabelWidth), typeof(GridLength), typeof(TextEntryControl), new PropertyMetadata(GridLength.Auto, LabelWidthChangeCallback));        

        #endregion

        #region Constructor

        /// <summary>
        /// Default construcor
        /// </summary>
        public TextEntryControl()
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
                (d as TextEntryControl).LabelColumnDefinition.Width = (GridLength)e.NewValue;
            }
            catch (Exception) 
            {
                //Make developer aware of potential issue
                Debugger.Break();

                (d as TextEntryControl).LabelColumnDefinition.Width = GridLength.Auto;
            }
        } 
        #endregion
    }
}
