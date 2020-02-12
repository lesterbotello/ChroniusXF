using System;
using Xamarin.Forms;

namespace ChroniusXF.Renderers
{
    public enum IconPosition { Left, Right }

    public class IconifiedEntry : Entry
    {
        public static readonly BindableProperty IconProperty =
            BindableProperty.Create("Icon", typeof(string), typeof(IconifiedEntry), "", BindingMode.OneWay);

        public static readonly BindableProperty IconPositionProperty =
            BindableProperty.Create("IconPosition", typeof(IconPosition), typeof(IconifiedEntry), IconPosition.Left, BindingMode.OneWay);

        public event EventHandler IconClicked;

        public void SendIconClicked()
        {
            var eventHandler = IconClicked;
            eventHandler?.Invoke(this, EventArgs.Empty);
        }

        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public IconPosition IconPosition
        {
            get => (IconPosition)GetValue(IconPositionProperty);
            set => SetValue(IconPositionProperty, value);
        }
    }
}
