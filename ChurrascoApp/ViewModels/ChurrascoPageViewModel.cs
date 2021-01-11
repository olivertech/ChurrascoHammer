using ChurrascoApp.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;

namespace ChurrascoApp.ViewModels
{
    public class ChurrascoPageViewModel : ViewModelBase, IDisposable
    {
        #region Variáveis

        private Churrasco _churrasco;
        private bool _ativo;
        private bool _habilitado;
        private DateTime _minDate;
        private string _textoStatus;

        public DelegateCommand ParticipantesCommand { get; set; }
        public DelegateCommand TotaisCommand { get; set; }

        #endregion

        #region Propriedades de Binding

        public Churrasco Churrasco
        {
            get => _churrasco;
            set => SetProperty(ref _churrasco, value);
        }

        public bool Ativo
        {
            get => _ativo;
            set => SetProperty(ref _ativo, value);
        }

        public string TextoStatus
        {
            get => _textoStatus;
            set => SetProperty(ref _textoStatus, value);
        }

        public DateTime MinDate
        {
            get => _minDate;
            set => SetProperty(ref _minDate, value);
        }

        public bool Habilitado
        {
            get => _habilitado;
            set => SetProperty(ref _habilitado, value);
        }

        #endregion

        #region Construtor

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="navigationService"></param>
        /// <param name="dialogService"></param>
        public ChurrascoPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService, dialogService)
        {
            Title = "Detalhes do Churrasco";
            MinDate = DateTime.Today;

            ParticipantesCommand = new DelegateCommand(Participantes).ObservesCanExecute(() => CanNavigateHome);
            TotaisCommand = new DelegateCommand(Totais).ObservesCanExecute(() => CanNavigateHome);
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Método para o evento de nevagação
        /// </summary>
        /// <param name="parameters"></param>
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("churrasco"))
            {
                Churrasco = (Churrasco)parameters["churrasco"];

                Habilitado = Churrasco.Ativo != "Encerrado";
            }

            if (Churrasco.Ativo == "Ativo")
            {
                Ativo = true;
                TextoStatus = "Churrasco Ativo";
            }
            else
            {
                Ativo = false;
                TextoStatus = "Churrasco Encerrado";
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método que redireciona para view de participantes do churrasco
        /// </summary>
        private void Participantes()
        {
            var navigationParams = new NavigationParameters
            {
                { "churrasco", Churrasco }
            };

            CanNavigateHome = false;
            _navigationService.NavigateAsync("ParticipantesPage", navigationParams);
            CanNavigateHome = true;
        }

        /// <summary>
        /// Método que redireciona para view de totais do churrasco
        /// </summary>
        private void Totais()
        {
            var navigationParams = new NavigationParameters
            {
                { "churrasco", Churrasco }
            };

            CanNavigateHome = false;
            _navigationService.NavigateAsync("TotaisPage", navigationParams);
            CanNavigateHome = true;
        }

        public void Dispose()
        {
            GC.Collect();
        }

        #endregion
    }
}
