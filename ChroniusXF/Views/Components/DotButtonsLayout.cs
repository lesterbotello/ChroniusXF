using System;
using Xamarin.Forms;

namespace ChroniusXF.Views.Components
{
    public class DotButtonsLayout : StackLayout
    {
        public DotButton[] Dots { get; set; }

        public DotButtonsLayout(int dotCount, Color dotColor, int Dotsize)
        {
            //Create as many buttons as desired.
            Dots = new DotButton[dotCount];

            // Stack the buttons from left to right...
            Orientation = StackOrientation.Horizontal;
            VerticalOptions = LayoutOptions.Center;
            HorizontalOptions = LayoutOptions.Center;

            for (int i = 0; i < dotCount; i++)
            {
                Dots[i] = new DotButton
                {
                    HeightRequest = Dotsize,
                    WidthRequest = Dotsize,
                    BackgroundColor = dotColor,

                    //All buttons except the first one will get an opacity
                    //of 0.5 to visualize the first one is selected.
                    Opacity = 0.5
                };
                Dots[i].Index = i;
                Dots[i].DotLayout = this;

                Children.Add(Dots[i]);
            }
            Dots[0].Opacity = 1;
        }
    }
}
