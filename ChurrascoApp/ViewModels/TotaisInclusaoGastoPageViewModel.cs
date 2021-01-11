using ChurrascoApp.Classes;
using ChurrascoApp.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;

namespace ChurrascoApp.ViewModels
{
    public class TotaisInclusaoGastoPageViewModel : ViewModelBase
    {
        #region Variáveis

        private string _descricao;
        private string _valor;
        private string _tipo;

        public DelegateCommand SalvarGastoCommand { get; set; }

        #endregion

        #region Propriedades

        protected Churrasco Churrasco { get; set; }
        protected Gasto NovoGasto { get; set; }
        protected TipoGasto Tipo { get; set; }

        #endregion

        #region Propriedades de Binding

        public string Descricao
        {
            get => _descricao;
            set => SetProperty(ref _descricao, value);
        }

        public string Valor
        {
            get => _valor;
            set => SetProperty(ref _valor, value);
        }

        public string TipoFormatado
        {
            get => _tipo;
            set => SetProperty(ref _tipo, value);
        }

        #endregion

        #region Construtor

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="navigationService"></param>
        /// <param name="dialogService"></param>
        public TotaisInclusaoGastoPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService, dialogService)
        {
            Title = "Totais do Churrasco";

            SalvarGastoCommand = new DelegateCommand(SalvarGasto).ObservesCanExecute(() => CanNavigateHome);
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
                Tipo = (TipoGasto)parameters["tipo"];

                switch (Tipo)
                {
                    case TipoGasto.Comida:
                        TipoFormatado = "Novo Gasto - Comida";
                        break;
                    case TipoGasto.Bebida:
                        TipoFormatado = "Novo Gasto - Bebida";
                        break;
                }
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método que salva um novo gasto
        /// </summary>
        private void SalvarGasto()
        {
            DatabaseModels.Gasto gasto = null;

            try
            {
                if (Descricao.Length == 0 || Valor.Length == 0)
                {
                    Util.RedToast("Informe corretamente os dados do gasto.");
                    return;
                }

                if (Tipo == TipoGasto.Comida)
                {
                    gasto = new DatabaseModels.Gasto
                    {
                        IdChurrasco = Churrasco.Id,
                        Descricao = Descricao,
                        Tipo = "C",
                        Valor = decimal.Parse(Valor),
                        DataCompra = DateTime.Today
                    };

                    _dataAccess.InsertGasto(gasto);
                }
                else
                {
                    gasto = new DatabaseModels.Gasto
                    {
                        IdChurrasco = Churrasco.Id,
                        Descricao = Descricao,
                        Tipo = "B",
                        Valor = decimal.Parse(Valor),
                        DataCompra = DateTime.Today
                    };

                    _dataAccess.InsertGasto(gasto);
                }

                var navigationParams = new NavigationParameters
                {
                    { "churrasco", Churrasco }, {"inclusao", true}, {"tipo", Tipo}
                };

                _navigationService.GoBackAsync(navigationParams);

            }
            catch (Exception)
            {
                Util.RedToast("Não foi possível incluir o novo gasto.");
            }
        }

        #endregion
    }
}
