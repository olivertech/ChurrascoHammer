using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace ChurrascoApp.ViewModels
{
    public class PrismMasterDetailPageViewModel : ViewModelBase, INavigationAware
    {
        #region Propriedades

        public DelegateCommand ChurrascosCommand { get; set; }
        public DelegateCommand ParticipantesCommand { get; set; }
        public DelegateCommand ConvidadosCommand { get; set; }
        public DelegateCommand TotaisCommand { get; set; }

        #endregion

        #region Contrutor

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="navigationService"></param>
        /// <param name="dialogService"></param>
        public PrismMasterDetailPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService, dialogService)
        {
            ChurrascosCommand = new DelegateCommand(Churrascos).ObservesCanExecute(() => CanNavigateHome);
            ParticipantesCommand = new DelegateCommand(Participantes).ObservesCanExecute(() => CanNavigateHome);
            ConvidadosCommand = new DelegateCommand(Convidados).ObservesCanExecute(() => CanNavigateHome);
            TotaisCommand = new DelegateCommand(Totais).ObservesCanExecute(() => CanNavigateHome);
        }

        #endregion

        #region Métodos Privados

        private void Churrascos()
        {
            CanNavigateHome = false;
            _navigationService.NavigateAsync("/PrismMasterDetailPage/NavigationMenuPage/MainPage/ChurrascosPage");
            CanNavigateHome = true;
        }

        private void Participantes()
        {
            //CanNavigateHome = false;
            //_navigationService.NavigateAsync("/PrismMasterDetailPage/NavigationMenuPage/MainPage/ParticipantesPage");
            //CanNavigateHome = true;
        }

        private void Convidados()
        {
            //CanNavigateHome = false;
            //_navigationService.NavigateAsync("/PrismMasterDetailPage/NavigationMenuPage/MainPage/ConvidadosPage");
            //CanNavigateHome = true;
        }

        private void Totais()
        {
            //CanNavigateHome = false;
            //_navigationService.NavigateAsync("/PrismMasterDetailPage/NavigationMenuPage/MainPage/TotaisPage");
            //CanNavigateHome = true;
        }
        
        #endregion
    }
}
