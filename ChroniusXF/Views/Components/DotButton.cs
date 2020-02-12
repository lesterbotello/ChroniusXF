using Xamarin.Forms;

namespace ChroniusXF.Views.Components
{
    public class DotButton : BoxView
    {
        public int Index { get; set;  }
        public DotButtonsLayout DotLayout { get; set; }
        public event ClickHandler Clicked;
        public delegate void ClickHandler(DotButton sender);

        public DotButton()
        {
            var clickCheck = new TapGestureRecognizer() { Command = new Command(() => Clicked?.Invoke(this)) };
            GestureRecognizers.Add(clickCheck);
        }
    }
}
