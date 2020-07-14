using ShellPresentation.Models;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ShellPresentation.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string itemName;

        public string ItemName
        {
            get => itemName;
            set => SetProperty(ref itemName, value);
        }

        private string itemDescription;

        public string ItemDescription
        {
            get => itemDescription;
            set => SetProperty(ref itemDescription, value);
        }

        public Command CancelCommand => new AsyncCommand(CancelCommandExecute);

        Task CancelCommandExecute() =>
            Navigation.GoToAsync("..");

        public Command AddCommand => new AsyncCommand(AddCommandExecute);

        async Task AddCommandExecute()
        {
            var item = new Item
            {
                Text = ItemName,
                Description = ItemDescription,
                Id = Guid.NewGuid().ToString()
            };

            await Navigation.GoToAsync("..", item).ConfigureAwait(false);
        }

        public NewItemViewModel()
        {
        }
    }
}
