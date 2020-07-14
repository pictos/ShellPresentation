using Xamarin.Forms;
using ShellPresentation.Services;

namespace ShellPresentation
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            var nav = NavigationService.Current;
            //var lastLocation = Preferences.Get("LastKnownUrl", string.Empty);
            //if (string.IsNullOrEmpty(lastLocation))
            //    return;
            //await nav.GoToAsync(new ShellNavigationState(lastLocation));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
