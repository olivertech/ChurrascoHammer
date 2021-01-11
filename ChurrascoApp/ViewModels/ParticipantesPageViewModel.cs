using Acr.UserDialogs;
using ChurrascoApp.Classes;
using ChurrascoApp.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ChurrascoApp.ViewModels
{
    public class ParticipantesPageViewModel : ViewModelBase, IDisposable
    {
        #region Variáveis

        private string _totalConvidados;
        private string _totalParticipantes;
        private string _totalArrecadado;
        private Churrasco _churrasco;

        public DelegateCommand<Participante> _itemTappedCommand = null;

        public DelegateCommand<Participante> ItemTappedCommand => _itemTappedCommand ?? (_itemTappedCommand = new DelegateCommand<Participante>(OnItemTappedCommandExecuted));

        #endregion

        #region Propriedades

        private TipoPessoa Tipo { get; set; }
        public DelegateCommand NovoParticipanteCommand { get; set; }

        #endregion

        #region Propriedades de Binding

        public Churrasco Churrasco
        {
            get => _churrasco;
            set => SetProperty(ref _churrasco, value);
        }

        public string TotalParticipantes
        {
            get => _totalParticipantes;
            set => SetProperty(ref _totalParticipantes, value);
        }

        public string TotalConvidados
        {
            get => _totalConvidados;
            set => SetProperty(ref _totalConvidados, value);
        }

        public string TotalArrecadado
        {
            get => _totalArrecadado;
            set => SetProperty(ref _totalArrecadado, value);
        }

        #endregion

        #region Construtor

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="navigationService"></param>
        /// <param name="dialogService"></param>
        public ParticipantesPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService, dialogService)
        {
            Title = "Lista de Participantes";
            NovoParticipanteCommand = new DelegateCommand(NovoParticipante).ObservesCanExecute(() => CanNavigateHome);
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

            GetParticipantes();
        }

        /// <summary>
        /// Método que trata o evento de tap na lista de participantes
        /// </summary>
        /// <param name="participante"></param>
        private void OnItemTappedCommandExecuted(Participante participante)
        {
            if (participante != null)
            {
                var navigationParams = new NavigationParameters
                {
                    { "churrasco", Churrasco }, { "participante", participante }
                };

                CanNavigateHome = false;
                _navigationService.NavigateAsync("ParticipantePage", navigationParams);
                CanNavigateHome = true;
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método que chama a view de cadastro de novo participante
        /// </summary>
        private void NovoParticipante()
        {
            var navigationParams = new NavigationParameters
            {
                { "churrasco", Churrasco }
            };

            CanNavigateHome = false;
            _navigationService.NavigateAsync("ParticipantePage", navigationParams);
            CanNavigateHome = true;
        }

        /// <summary>
        /// Método que recupera os participantes e convidados
        /// </summary>
        private async void GetParticipantes()
        {
            try
            {
                using (UserDialogs.Instance.Loading("Carregando..."))
                {
                    try
                    {
                        await Task.Run(() => GetListaParticipantes()).ContinueWith((t) =>
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

                if (ParticipantesCollection != null)
                {
                    TotalParticipantes = "Total de " + ParticipantesCollection.Count() + " Participantes";
                    TotalConvidados = "Total de " + ParticipantesCollection.Where(x => x.TemConvidado == true).Count() + " Convidados";

                    Util.GreenToast("Participantes carregados com sucesso.");
                }
                else
                {
                    Util.RedToast("Erro no carregamento dos participantes.");
                }
            }
            catch (Exception)
            {
                Util.RedToast("Erro no carregamento dos participantes.");
            }
        }

        /// <summary>
        /// Método q efetiva o carregamento da lista de participantes
        /// </summary>
        private void GetListaParticipantes()
        {
            List<Participante> listaView = new List<Participante>();
            List<DatabaseModels.Participante> listaParticipantes =_dataAccess.GetParticipantes();

            foreach (var item in listaParticipantes)
            {
                var convidado = new Convidado();
                var participante = new Participante();

                var funcionarioDb = _dataAccess.GetFuncionario(item.IdFuncionario);

                var funcionario = new Funcionario
                {
                    Id = funcionarioDb.Id,
                    Nome = funcionarioDb.Nome,
                    Sobrenome = funcionarioDb.Sobrenome,
                    Idade = funcionarioDb.Idade
                };

                if (item.TemConvidado)
                {
                    var convidadoDb = _dataAccess.GetConvidado(item.IdConvidado);

                    convidado = new Convidado
                    {
                        Id = convidadoDb.Id,
                        Nome = convidadoDb.Nome,
                        Sobrenome = convidadoDb.Sobrenome,
                        Idade = convidadoDb.Idade,
                        Sexo = convidadoDb.Sexo
                    };

                    participante = new Participante
                    {
                        Id = item.Id,
                        IdChurrasco = item.IdChurrasco,
                        Funcionario = funcionario,
                        Convidado = convidado,
                        ValorParticipante = item.ValorParticipante,
                        ValorConvidado = item.ValorConvidado,
                        ParticipanteBebe = item.ParticipanteBebe,
                        ConvidadoBebe = item.ConvidadoBebe,
                        TemConvidado = item.TemConvidado
                    };
                }
                else
                {
                    participante = new Participante
                    {
                        Id = item.Id,
                        IdChurrasco = item.IdChurrasco,
                        Funcionario = funcionario,
                        ValorParticipante = item.ValorParticipante,
                        ValorConvidado = item.ValorConvidado,
                        ParticipanteBebe = item.ParticipanteBebe,
                        ConvidadoBebe = item.ConvidadoBebe,
                        TemConvidado = item.TemConvidado
                    };
                }

                listaView.Add(participante);
            }

            ParticipantesCollection = new ObservableCollection<Participante>(listaView);

            CalcularTotalArrecadado();
        }

        /// <summary>
        /// Método que calcula os totais arrecadados
        /// </summary>
        private void CalcularTotalArrecadado()
        {
            var totais = (from participante in ParticipantesCollection
                          group participante by participante.IdChurrasco into grupoParticipantes
                          select new
                          {
                             totalParticipante = grupoParticipantes.Sum(x => x.ValorParticipante),
                             totalConvidado = grupoParticipantes.Sum(x => x.ValorConvidado),
                          }).First();

            var soma = totais.totalParticipante + totais.totalConvidado;
            TotalArrecadado = "Total Arrecadado - " + decimal.Parse(soma.ToString()).ToString("C", nfi);
        }

        public void Dispose()
        {
            GC.Collect();
        }

        #endregion
    }
}
