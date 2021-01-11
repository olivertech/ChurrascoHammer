using ChurrascoApp.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChurrascoApp.ViewModels
{
    public class TotaisPageViewModel : ViewModelBase
    {
        #region Variáveis

        private string _totalArrecadado;
        private string _totalGastos;
        private string _totalGastoComida;
        private string _totalGastoBebida;

        public DelegateCommand AtualizarGastosCommand { get; set; }

        #endregion

        #region Propriedades

        public Churrasco Churrasco { get; set; }

        #endregion

        #region Propriedades de Binding

        public string TotalArrecadado
        {
            get => _totalArrecadado;
            set => SetProperty(ref _totalArrecadado, value);
        }

        public string TotalGastos
        {
            get => _totalGastos;
            set => SetProperty(ref _totalGastos, value);
        }

        public string TotalGastoComida
        {
            get => _totalGastoComida;
            set => SetProperty(ref _totalGastoComida, value);
        }

        public string TotalGastoBebida
        {
            get => _totalGastoBebida;
            set => SetProperty(ref _totalGastoBebida, value);
        }

        #endregion

        #region Construtor

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="navigationService"></param>
        /// <param name="dialogService"></param>
        public TotaisPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService, dialogService)
        {
            Title = "Totais do Churrasco";

            AtualizarGastosCommand = new DelegateCommand(AtualizarTotais).ObservesCanExecute(() => CanNavigateHome);
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
            }

            CalculaTotais();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método que realiza o calculo de todos os totais associados ao churrasco
        /// </summary>
        private void CalculaTotais()
        {
            decimal totalComidas = 0;
            decimal totalBebidas = 0;

            var listaGastosComidaDb = _dataAccess.GetGastosPorTipo(Churrasco.Id, "C");
            var listaGastosBebidaDb = _dataAccess.GetGastosPorTipo(Churrasco.Id, "B");

            foreach (var item in listaGastosComidaDb)
            {
                var gasto = new Gasto
                {
                    Id = item.Id,
                    Descricao = item.Descricao,
                    Tipo = item.Tipo,
                    DataCompra = item.DataCompra,
                    Valor = item.Valor
                };

                totalComidas += gasto.Valor;
            }

            foreach (var item in listaGastosBebidaDb)
            {
                var gasto = new Gasto
                {
                    Id = item.Id,
                    Descricao = item.Descricao,
                    Tipo = item.Tipo,
                    DataCompra = item.DataCompra,
                    Valor = item.Valor
                };

                totalBebidas += gasto.Valor;
            }

            var participantes = new ObservableCollection<DatabaseModels.Participante>(_dataAccess.GetParticipantes(Churrasco.Id));

            var totais = (from participante in participantes
                          group participante by participante.IdChurrasco into grupoParticipantes
                          select new
                          {
                              totalParticipante = grupoParticipantes.Sum(x => x.ValorParticipante),
                              totalConvidado = grupoParticipantes.Sum(x => x.ValorConvidado),
                          }).First();

            TotalArrecadado = (totais.totalParticipante + totais.totalConvidado).ToString("C", nfi);
            TotalGastoComida = totalComidas.ToString("C", nfi);
            TotalGastoBebida = totalBebidas.ToString("C", nfi);

            var totalGastos = totalComidas + totalBebidas;

            TotalGastos = totalGastos.ToString("C", nfi);
        }

        /// <summary>
        /// Método que navega para a view com totais do churrasco
        /// </summary>
        private void AtualizarTotais()
        {
            var navigationParams = new NavigationParameters
            {
                { "churrasco", Churrasco }
            };

            CanNavigateHome = false;
            _navigationService.NavigateAsync("TotaisListaPage", navigationParams);
            CanNavigateHome = true;
        }

        #endregion
    }
}
