using Foundation;
using UIKit;
using Prism;
using Prism.Ioc;

namespace ChroniusXF.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Xamarin.Forms.Forms.SetFlags("IndicatorView_Experimental", "CarouselView_Experimental");
            Xamarin.Forms.Forms.Init();

            // TODO: Check if this neccesary for the official version of
            // CarouselView included in Forms 2.0...
            //var cv = typeof(Xamarin.Forms.CarouselView);
            //var assembly = Assembly.Load(cv.FullName);

            LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(app, options);
        }
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry) { }
    }
}
