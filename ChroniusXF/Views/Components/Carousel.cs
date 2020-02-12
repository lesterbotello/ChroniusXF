using System.Collections.Generic;
using Xamarin.Forms;

namespace ChroniusXF.Views.Components
{
    public class Carousel : AbsoluteLayout
    {
        private DotButtonsLayout dotLayout;
        private CarouselView carouselView;

        public Carousel(ICollection<CarouselContent> pages)
        {
            //var page1 = new CarouselContent();
            //var page2 = new CarouselContent();
            //var page3 = new CarouselContent();
            //var page4 = new CarouselContent();

            //var pages = new ObservableCollection<CarouselContent>();
            //pages.Add(page1);
            //pages.Add(page2);
            //pages.Add(page3);
            //pages.Add(page4);

            //Set the Layout to fill and expand to occupy its whole space.
            HorizontalOptions = LayoutOptions.FillAndExpand;
            VerticalOptions = LayoutOptions.FillAndExpand;
            //Create the CarouselView itself.
            carouselView = new CarouselView
            {
                //And make it expand to the whole Layout.
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            var template = new DataTemplate(() =>
            {
                var page1 = new AbsoluteLayout
                {
                    BackgroundColor = Color.FromHex("2C2E31"),
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };
                page1.SetBinding(BackgroundColorProperty, "BackgroundColor");

                var lab = new Label
                {
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) + 10,
                    FontAttributes = FontAttributes.Bold
                };
                lab.TextColor = Color.White;
                lab.HorizontalOptions = LayoutOptions.Center;
                lab.VerticalOptions = LayoutOptions.Center;

                // Bind it's content to the Header-attribute  
                lab.SetBinding(Label.TextProperty, "Header");

                var lab2 = new Label()
                {
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
                };
                lab2.TextColor = Color.White;
                lab2.HorizontalOptions = LayoutOptions.Center;
                lab2.VerticalOptions = LayoutOptions.Center;

                var lab3 = new Label()
                {
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
                };
                lab3.TextColor = Color.White;
                lab3.HorizontalOptions = LayoutOptions.Center;
                lab3.VerticalOptions = LayoutOptions.Center;
                page1.Children.Add(lab);
                page1.Children.Add(lab2);
                page1.Children.Add(lab3);

                SetLayoutBounds(lab, new Rectangle(0, 0.3, 1, 0.2));
                SetLayoutFlags(lab, AbsoluteLayoutFlags.All);
                SetLayoutBounds(lab2, new Rectangle(0, 0.4, 1, 0.2));
                SetLayoutFlags(lab2, AbsoluteLayoutFlags.All);
                SetLayoutBounds(lab3, new Rectangle(0, 0.5, 1, 0.2));
                SetLayoutFlags(lab3, AbsoluteLayoutFlags.All);

                return page1;
            });

            //Assign the passeg pages to the ItemsSource
            carouselView.ItemsSource = pages;
            //Assign the freshly created template
            carouselView.ItemTemplate = template;
            //Placeholder 3. Make sure to unsubscribe somewhere
            carouselView.PositionChanged += PageChanged;
            //Add the carousel to the abolsute layout and set its boundaries to fill
            //the entire layout
            Children.Add(carouselView);
            SetLayoutBounds(carouselView, new Rectangle(0, 0, 1, 1));
            SetLayoutFlags(carouselView, AbsoluteLayoutFlags.All);

            //Create the button layout with as many buttons as there are pages
            dotLayout = new DotButtonsLayout(pages.Count, Color.White, 10);

            foreach (DotButton dot in dotLayout.Dots)
                dot.Clicked += DotClicked;

            Children.Add(dotLayout);
            SetLayoutBounds(dotLayout, new Rectangle(0, 0.92, 1, .05));
            SetLayoutFlags(dotLayout, AbsoluteLayoutFlags.All);
        }

        //PLACEHOLDER 3 : PAGE EVENTS

        private void PageChanged(object sender, PositionChangedEventArgs e)
        {
            //Set all buttons opacity to 0.5 but the selected one, which we set to 1
            for (int i = 0; i < dotLayout.Dots.Length; i++)
                if (e.CurrentPosition == i)
                    dotLayout.Dots[i].Opacity = 1;
                else
                    dotLayout.Dots[i].Opacity = 0.5;
        }

        //The function called by the buttons clicked event
        private void DotClicked(object sender)
        {
            var button = (DotButton)sender;
            //Get the selected buttons index
            int index = button.Index;
            //Set the corresponding page as position of the carousel view
            carouselView.Position = index;
        }
    }


}
