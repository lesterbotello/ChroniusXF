using ChroniusXF.Persistence;
using ChroniusXF.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;

namespace ChroniusXF
{
    public partial class App : PrismApplication
    {
        private static ChroniusDatabase _database;

        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
            MainPage = new NavigationPage(new HomePage());

            Device.SetFlags(new[] {
                "CarouselView_Experimental",
                "IndicatorView_Experimental"
            });
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<HomePage>();
            containerRegistry.RegisterForNavigation<EditChronius>();
            containerRegistry.RegisterForNavigation<AddLocation>();
            containerRegistry.RegisterSingleton<IChroniusDatabase, ChroniusDatabase>();
        }

        protected override void OnInitialized()
        {
            InitializeComponent();
        }

        public static ChroniusDatabase Database
        {
            get
            {
                if(_database == null)
                {
                    _database = new ChroniusDatabase();
                }

                return _database;
            }
        }
    }
}
