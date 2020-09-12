using ShellPresentation.ViewModels;
using ShellPresentation.Views;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ShellPresentation.Services
{
    public sealed class NavigationService
    {
        static readonly Lazy<NavigationService> navigation =
            new Lazy<NavigationService>(() => new NavigationService());

        public static NavigationService Current => navigation.Value;

        Shell Shell => Shell.Current;

        Page CurrentPage { get; set; }

        NavigationService()
        {
            RegisterRoutes();
            Shell.Navigated += OnShellNavigated;
            Shell.Navigating += OnShellNavigating;
        }

        // doesn't work with async methods
        void OnShellNavigating(object sender, ShellNavigatingEventArgs e)
        {
            var current = e.Current;
            #region LetMeGO
            if (current.Location.OriginalString.Contains("MainViewModel"))
            {
                var vm = CurrentPage.BindingContext as MainViewModel;
                if (!vm.IsChecked)
                    e.Cancel();
            } 
            #endregion
        }

        void OnShellNavigated(object sender, ShellNavigatedEventArgs e)
        {
            var page = Shell.CurrentItem.CurrentItem as ShellSection;
            CurrentPage = ((IShellSectionController)page).PresentedPage;
            #region Secret
            Preferences.Set("LastKnownUrl", e.Current.Location.OriginalString);
            #endregion
        }

        void RegisterRoutes()
        {
            Routing.RegisterRoute(nameof(InfoViewModel), typeof(InfoPage));
            Routing.RegisterRoute(nameof(NewItemViewModel), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(FinalViewModel), typeof(FinalPage));
            Routing.RegisterRoute(nameof(ItemDetailViewModel), typeof(ItemDetailPage));
        }

        public async Task GoToAsync(string url, object args = null)
        {
            await Shell.GoToAsync(url);
            #region NavigatingBack
            if (url == ".." || url.Contains("\\") || url.Contains("/"))
            {
                await (CurrentPage.BindingContext as BaseViewModel).BackAsync(args).ConfigureAwait(false);
                return;
            } 
            #endregion
            var vm = CreateViewModel(url);
            CurrentPage.BindingContext = vm;
            await vm.InitAsync(args).ConfigureAwait(false);
        }

        public async Task GoToAsync(ShellNavigationState state, object args = null)
        {
            await Shell.GoToAsync(state);
            var vm = CreateViewModel(state.Location.OriginalString.Split('/').Last());
            CurrentPage.BindingContext = vm;
            await vm.InitAsync(args).ConfigureAwait(false);
        }

        BaseViewModel CreateViewModel(string url)
        {
            var name = typeof(NavigationService).AssemblyQualifiedName.Split('.')[0];
            var typeName = $"{name}.ViewModels.{url}";
            var viewModel = (BaseViewModel)Activator.CreateInstance(Type.GetType(typeName));
            return viewModel;
        }
    }
}
