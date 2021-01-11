using ChurrascoApp.Classes;
using ChurrascoApp.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.ObjectModel;

namespace ChurrascoApp.ViewModels
{
    public class TotaisListaPageViewModel : ViewModelBase
    {
        #region Variáveis

        public DelegateCommand IncluirGastoComidaCommand { get; set; }
        public DelegateCommand IncluirGastoBebidaCommand { get; set; }

        #endregion

        #region Propriedades

        public Churrasco Churrasco { get; set; }

        #endregion

        #region Construtor

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="navigationService"></param>
        /// <param name="dialogService"></param>
        public TotaisListaPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService, dialogService)
        {
            Title = "Gastos do Churrasco";

            IncluirGastoComidaCommand = new DelegateCommand(IncluirGastoComida).ObservesCanExecute(() => CanNavigateHome);
            IncluirGastoBebidaCommand = new DelegateCommand(IncluirGastoBebida).ObservesCanExecute(() => CanNavigateHome);
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

                CarregarListasGastos();

                if (parameters.ContainsKey("inclusao"))
                {
                    var inclusao = (bool)parameters["inclusao"];
                    var tipo = (TipoGasto)parameters["tipo"];

                    if (inclusao)
                    {
                        switch (tipo)
                        {
                            case TipoGasto.Comida:
                                Util.GreenToast("Novo gasto de comida incluído com sucesso.");
                                break;
                            case TipoGasto.Bebida:
                                Util.GreenToast("Novo gasto de bebida incluído com sucesso.");
                                break;
                        }
                    }
                }
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método que carrega as listas de gastos com comida e bebida
        /// </summary>
        private void CarregarListasGastos()
        {
            var listaGastosComidaDb = _dataAccess.GetGastosPorTipo(Churrasco.Id, "C");
            var listaGastosBebidaDb = _dataAccess.GetGastosPorTipo(Churrasco.Id, "B");

            GastosComidaCollection = new ObservableCollection<Gasto>();
            GastosBebidaCollection = new ObservableCollection<Gasto>();

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

                GastosComidaCollection.Add(gasto);
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

                GastosBebidaCollection.Add(gasto);
            }
        }

        /// <summary>
        /// Método para inclusão de novo gasto com comida
        /// </summary>
        private void IncluirGastoComida()
        {
            var navigationParams = new NavigationParameters
            {
                { "churrasco", Churrasco }, { "tipo", TipoGasto.Comida }
            };

            CanNavigateHome = false;
            _navigationService.NavigateAsync("TotaisInclusaoGastoPage", navigationParams);
            CanNavigateHome = true;
        }

        /// <summary>
        /// Método para inclusão de novo gasto com bebida
        /// </summary>
        private void IncluirGastoBebida()
        {
            var navigationParams = new NavigationParameters
            {
                { "churrasco", Churrasco }, { "tipo", TipoGasto.Bebida }
            };

            CanNavigateHome = false;
            _navigationService.NavigateAsync("TotaisInclusaoGastoPage", navigationParams);
            CanNavigateHome = true;
        }

        #endregion
    }
}
