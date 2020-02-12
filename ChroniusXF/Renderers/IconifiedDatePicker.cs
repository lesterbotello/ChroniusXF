using System;
using Xamarin.Forms;

namespace ChroniusXF.Renderers
{
    public class IconifiedDatePicker : DatePicker
    {
        public static readonly BindableProperty IconProperty =
            BindableProperty.Create("Icon", typeof(string), typeof(IconifiedEntry), "", BindingMode.OneWay);

        public static readonly BindableProperty IconPositionProperty =
            BindableProperty.Create("IconPosition", typeof(IconPosition), typeof(IconifiedEntry), IconPosition.Left, BindingMode.OneWay);

        public IconPosition IconPosition
        {
            get => (IconPosition)GetValue(IconPositionProperty);
            set => SetValue(IconPositionProperty, value);
        }

        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public event EventHandler IconClicked;

        public void SendIconClicked()
        {
            var eventHandler = IconClicked;
            eventHandler?.Invoke(this, EventArgs.Empty);
        }

        public IconifiedDatePicker()
        {
        }
    }
}
