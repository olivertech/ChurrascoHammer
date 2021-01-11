using ChurrascoApp.Database;
using ChurrascoApp.Models;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System.Collections.ObjectModel;
using System.Globalization;

namespace ChurrascoApp.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        #region Variáveis

        protected readonly NumberFormatInfo nfi = new CultureInfo("pt-BR").NumberFormat;
        protected readonly DataAccess _dataAccess = new DataAccess();

        protected INavigationService NavigationService { get; private set; }
        private string _title;
        private bool _canNavigateHome = true;
        private IPageDialogService _dialogService;
        private ObservableCollection<Churrasco> _churrascosCollection;
        private ObservableCollection<Participante> _participantesCollection;
        private ObservableCollection<Participante> _convidadosCollection;
        private ObservableCollection<Gasto> _gastosComidaCollection;
        private ObservableCollection<Gasto> _gastosBebidaCollection;

        protected enum TipoPessoa
        {
            Participante, Convidado
        }

        protected enum TipoGasto
        {
            Comida, Bebida
        }

        #endregion

        #region Propriedades

        protected INavigationService _navigationService { get; private set; }

        public string CurrentPlatform { get; private set; }

        #endregion

        #region Propriedades de Binding

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public bool CanNavigateHome
        {
            get => _canNavigateHome;
            set => SetProperty(ref _canNavigateHome, value);
        }

        public ObservableCollection<Churrasco> ChurrascosCollection
        {
            get => _churrascosCollection;
            set => SetProperty(ref _churrascosCollection, value);
        }

        public ObservableCollection<Participante> ParticipantesCollection
        {
            get => _participantesCollection;
            set => SetProperty(ref _participantesCollection, value);
        }

        public ObservableCollection<Gasto> GastosComidaCollection
        {
            get => _gastosComidaCollection;
            set => SetProperty(ref _gastosComidaCollection, value);
        }

        public ObservableCollection<Gasto> GastosBebidaCollection
        {
            get => _gastosBebidaCollection;
            set => SetProperty(ref _gastosBebidaCollection, value);
        }

        #endregion

        #region Construtor

        protected ViewModelBase(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
        }

        #endregion

        #region Métodos

        public decimal RetornaValorPagoParticipante(bool participanteBebe)
        {
            return participanteBebe ? 40 : 20;
        }

        public decimal RetornaValorPagoConvidado(bool convidadoBebe)
        {
            return convidadoBebe ? 20 : 10;
        }

        #endregion

        #region Métodos de Navegação

        public virtual void Initialize(INavigationParameters parameters)
        {
            //Implementar diretamente nas ViewModels
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
            //Implementar diretamente nas ViewModels
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
            //Implementar diretamente nas ViewModels
        }

        public virtual void Destroy()
        {
            //Implementar diretamente nas ViewModels
        }

        #endregion
    }
}
