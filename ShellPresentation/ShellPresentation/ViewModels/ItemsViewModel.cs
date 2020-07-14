using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using ShellPresentation.Models;

namespace ShellPresentation.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new AsyncCommand(ExecuteLoadItemsCommand);
            AddItemCommand = new AsyncCommand(ExecuteAddItemCommand);
        }

        public override async Task BackAsync(object args)
        {
            if(!(args is Item newItem))
                return;
            
            Items.Add(newItem);
            await DataStore.AddItemAsync(newItem);
        }

        Task ExecuteAddItemCommand() =>
            Navigation.GoToAsync(nameof(NewItemViewModel));

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true).ConfigureAwait(false);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}