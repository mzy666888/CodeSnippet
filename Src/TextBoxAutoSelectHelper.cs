using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;


namespace DependencyHelpers;

    public class TextBoxAutoSelectHelper
    {
        public static readonly DependencyProperty SelectAllWhenGetFocusProperty =
            DependencyProperty.RegisterAttached(
                "SelectAllWhenGetFocus", typeof(bool), typeof(TextBoxAutoSelectHelper),
                new FrameworkPropertyMetadata(default(bool),
                    new PropertyChangedCallback(OnSelectAllWhenGetFocusChanged)));

        public static bool GetSelectAllWhenGotFocus(TextBoxBase d)
        {
            return (bool)d.GetValue(SelectAllWhenGetFocusProperty);
        }

        public static void SetSelectAllWhenGetFocus(TextBoxBase d, bool value)
        {
            d.SetValue(SelectAllWhenGetFocusProperty, value);
        }

        private static void OnSelectAllWhenGetFocusChanged(DependencyObject dependency,
            DependencyPropertyChangedEventArgs e)
        {
            if (dependency is TextBoxBase textBox)
            {
                var isSelectedAllWhenGotfoucs = (bool)e.NewValue;
                if (isSelectedAllWhenGotfoucs)
                {
                    textBox.PreviewMouseDown += TextBoxOnPreviewMouseDown;
                    textBox.GotFocus += TextBoxOnGotFocus;
                    textBox.LostFocus += TextBoxOnLostFocus;
                }
                else
                {
                    textBox.PreviewMouseDown -= TextBoxOnPreviewMouseDown;
                    textBox.GotFocus -= TextBoxOnGotFocus;
                    textBox.LostFocus -= TextBoxOnLostFocus;
                }
            }
        }

        private static void TextBoxOnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBoxBase textBox)
            {
                textBox.Focus();
                e.Handled = true;
            }
        }

        private static void TextBoxOnGotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBoxBase textBox)
            {
                textBox.SelectAll();
                textBox.PreviewMouseDown -= TextBoxOnPreviewMouseDown;
            }
        }

        private static void TextBoxOnLostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBoxBase textBox)
            {
                textBox.PreviewMouseDown += TextBoxOnPreviewMouseDown;
            }

        }
    }

