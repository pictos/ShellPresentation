using Xamarin.Forms;
using ShellPresentation.Services;
using static System.String;

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
            var lastLocation = Xamarin.Essentials.Preferences.Get("LastKnownUrl", string.Empty);
            if (IsNullOrEmpty(lastLocation))
                return;
            await nav.GoToAsync(new ShellNavigationState(lastLocation));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
