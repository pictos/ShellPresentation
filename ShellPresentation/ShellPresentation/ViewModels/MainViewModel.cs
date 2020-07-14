namespace ShellPresentation.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private bool isChecked;

        public bool IsChecked
        {
            get => isChecked;
            set => SetProperty(ref isChecked, value);
        }
    }
}
