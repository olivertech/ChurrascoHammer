using Acr.UserDialogs;
using ChurrascoApp.Classes;
using ChurrascoApp.Models;
using ChurrascoApp.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ChurrascoApp.ViewModels
{
    public class ChurrascosPageViewModel : ViewModelBase, IDisposable
    {
        #region Variáveis

        private DelegateCommand<Churrasco> _itemTappedCommand = null;
        public DelegateCommand<Churrasco> ItemTappedCommand => _itemTappedCommand ?? (_itemTappedCommand = new DelegateCommand<Churrasco>(OnItemTappedCommandExecuted));

        #endregion

        #region Construtor

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="navigationService"></param>
        /// <param name="dialogService"></param>
        public ChurrascosPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService, dialogService)
        {
            Title = "Lista de Churrascos";
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Método para o evento de nevagação
        /// </summary>
        /// <param name="parameters"></param>
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            GetChurrascos();
        }

        /// <summary>
        /// Método que trata o evento de tap da lista
        /// </summary>
        /// <param name="churrasco"></param>
        private void OnItemTappedCommandExecuted(Churrasco churrasco)
        {
            if (churrasco != null)
            {
                var navigationParams = new NavigationParameters
                {
                    {"churrasco", churrasco}
                };

                _navigationService.NavigateAsync("ChurrascoPage", navigationParams);
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método q carrega a lista de churrascos
        /// </summary>
        private async void GetChurrascos()
        {
            try
            {
                using (UserDialogs.Instance.Loading("Carregando..."))
                {
                    try
                    {
                        await Task.Run(() => GetListaChurrascos()).ContinueWith((t) =>
                        {
                            if (t.IsFaulted)
                            {
                                throw t.Exception;
                            }
                        });
                    }
                    catch (AggregateException ex)
                    {
                        throw ex;
                    }
                }

                if (ChurrascosCollection != null)
                {
                    Util.GreenToast("Churrascos carregados com sucesso.");
                }
                else
                {
                    Util.RedToast("Erro no carregamento dos churrascos.");
                }
            }
            catch (Exception)
            {
                Util.RedToast("Erro no carregamento dos churrascos.");
            }
        }

        /// <summary>
        /// Método q efetiva o carregamento da lista de churrascos
        /// </summary>
        private void GetListaChurrascos()
        {
            List<Churrasco> listaNaoSerializada = new List<Churrasco>();

            ChurrascosCollection = JsonConvert.DeserializeObject<ObservableCollection<Churrasco>>(ChurrascoServices.GetChurrascosJson());
        }

        public void Dispose()
        {
            GC.Collect();
        }

        #endregion
    }
}
