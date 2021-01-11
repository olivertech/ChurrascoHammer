using Prism.Navigation;
using Prism.Services;

namespace ChurrascoApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService, dialogService)
        {
            Title = "Controle de Churrasco - Hammer";
        }
    }
}
