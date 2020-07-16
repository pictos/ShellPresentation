
using ShellPresentation.Models;
using System.Threading.Tasks;

namespace ShellPresentation.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }

        public override Task InitAsync(object args)
        {
            var item = (Item)args;
            Title = item?.Text;
            Item = item;
            OnPropertyChanged(nameof(Item));
            return Task.CompletedTask;
        }
    }
}
