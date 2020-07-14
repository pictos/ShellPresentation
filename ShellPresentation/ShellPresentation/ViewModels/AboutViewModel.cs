using ShellPresentation.Models;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ShellPresentation.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private string name;

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        private uint age;

        public uint Age
        {
            get => age;
            set => SetProperty(ref age, value);
        }

        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new AsyncCommand(OpenWebCommandExecute);
        }

        Task OpenWebCommandExecute()
        {
            return Navigation.GoToAsync("InfoViewModel", new User
            {
                UserName = Name,
                UserAge = Age
            });
        }

        public AsyncCommand OpenWebCommand { get; }
    }
}