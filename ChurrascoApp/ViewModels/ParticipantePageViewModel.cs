using Acr.UserDialogs;
using ChurrascoApp.Classes;
using ChurrascoApp.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;

namespace ChurrascoApp.ViewModels
{
    public class ParticipantePageViewModel : ViewModelBase
    {
        #region Variáveis

        public DelegateCommand SalvarCommand { get; set; }
        public DelegateCommand RemoverCommand { get; set; }

        private int _idParticipante;
        private Participante _participante;

        private int _idFuncionario;
        private string _nomeFuncionario;
        private string _sobrenomeFuncionario;
        private string _idadeFuncionario;
        private bool _funcionarioBebe;

        private int _idConvidado;
        private string _nomeConvidado;
        private string _sobrenomeConvidado;
        private string _idadeConvidado;
        private string _sexoConvidado;
        private bool _convidadoBebe;

        private bool _temConvidado;
        private bool _showBotaoExcluir;
        private List<string> _sexos;

        #endregion

        #region Propriedades

        public Churrasco Churrasco { get; set; }

        #endregion

        #region Propriedades de Binding

        public Participante Participante
        {
            get => _participante;
            set => SetProperty(ref _participante, value);
        }

        public int IdParticipante
        {
            get => _idParticipante;
            set => SetProperty(ref _idParticipante, value);
        }

        public int IdFuncionario
        {
            get => _idFuncionario;
            set => SetProperty(ref _idFuncionario, value);
        }

        public string NomeFuncionario
        {
            get => _nomeFuncionario;
            set => SetProperty(ref _nomeFuncionario, value);
        }

        public string SobrenomeFuncionario
        {
            get => _sobrenomeFuncionario;
            set => SetProperty(ref _sobrenomeFuncionario, value);
        }

        public string IdadeFuncionario
        {
            get => _idadeFuncionario;
            set => SetProperty(ref _idadeFuncionario, value);
        }

        public bool FuncionarioBebe
        {
            get => _funcionarioBebe;
            set => SetProperty(ref _funcionarioBebe, value);
        }

        public int IdConvidado
        {
            get => _idConvidado;
            set => SetProperty(ref _idConvidado, value);
        }

        public string NomeConvidado
        {
            get => _nomeConvidado;
            set => SetProperty(ref _nomeConvidado, value);
        }

        public string SobrenomeConvidado
        {
            get => _sobrenomeConvidado;
            set => SetProperty(ref _sobrenomeConvidado, value);
        }

        public string IdadeConvidado
        {
            get => _idadeConvidado;
            set => SetProperty(ref _idadeConvidado, value);
        }

        public string SexoConvidado
        {
            get => _sexoConvidado;
            set => SetProperty(ref _sexoConvidado, value);
        }

        public bool ConvidadoBebe
        {
            get => _convidadoBebe;
            set => SetProperty(ref _convidadoBebe, value);
        }

        public bool ShowBotaoExcluir
        {
            get => _showBotaoExcluir;
            set => SetProperty(ref _showBotaoExcluir, value);
        }

        public bool TemConvidado
        {
            get => _temConvidado;
            set
            {
                SetProperty(ref _temConvidado, value);

                NomeConvidado = string.Empty;
                SobrenomeConvidado = string.Empty;
                IdadeConvidado = string.Empty;
                SexoConvidado = string.Empty;
                ConvidadoBebe = false;
            }
        }

        public List<string> Sexos
        {
            get => _sexos;
            set => SetProperty(ref _sexos, value);
        }

        #endregion

        #region Construtor

        public ParticipantePageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService, dialogService)
        {
            SalvarCommand = new DelegateCommand(SalvarParticipante).ObservesCanExecute(() => CanNavigateHome);
            RemoverCommand = new DelegateCommand(RemoverParticipante).ObservesCanExecute(() => CanNavigateHome);

            TemConvidado = false;

            CarregarSexos();
        }

        #endregion

        #region Eventos

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("participante"))
            {
                Churrasco = (Churrasco)parameters["churrasco"];
                Participante = (Participante)parameters["participante"];
                Title = "Detalhes do Participante";
                ShowBotaoExcluir = true;

                CarregarParticipante();
            }
            else
            {
                Title = "Novo Participante";
                Churrasco = (Churrasco)parameters["churrasco"];
                ShowBotaoExcluir = false;
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método que carrega as opções da dropdown de sexo
        /// </summary>
        private void CarregarSexos()
        {
            Sexos = new List<string>
            {
                "Masculino",
                "Feminino"
            };
        }

        /// <summary>
        /// Carrega os campos bindables da view
        /// </summary>
        private void CarregarParticipante()
        {
            IdParticipante = Participante.Id;

            IdFuncionario = Participante.Funcionario.Id;
            NomeFuncionario = Participante.Funcionario.Nome;
            SobrenomeFuncionario = Participante.Funcionario.Sobrenome;
            IdadeFuncionario = Participante.Funcionario.Idade.Replace(" Anos", "");
            TemConvidado = Participante.TemConvidado;
            FuncionarioBebe = Participante.ParticipanteBebe;

            IdConvidado = Participante.Convidado.Id;
            NomeConvidado = Participante.Convidado.Nome;
            SobrenomeConvidado = Participante.Convidado.Sobrenome;
            IdadeConvidado = Participante.Convidado.Idade.Replace(" Anos", ""); ;
            SexoConvidado = Participante.Convidado.Sexo;
            ConvidadoBebe = Participante.ConvidadoBebe;
        }

        /// <summary>
        /// Método que direciona para a rotina correta de gravação
        /// </summary>
        private void SalvarParticipante()
        {
            try
            {
                if (IdParticipante == 0)
                    IncluirParticipante();
                else
                    AtualizaParticipante(IdParticipante);

                _navigationService.GoBackAsync();

                Util.GreenToast("Participante salvo com sucesso.");
            }
            catch (Exception ex)
            {
                Util.RedToast(ex.Message);
            }
        }

        /// <summary>
        /// Método que deleta um participante e convidado, se houver
        /// 
        /// Atenção: Faltou implementar o recurso de transação na deleção
        /// 
        /// </summary>
        private async void RemoverParticipante()
        {
            try
            {
                var result = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
                {
                    Title = "Atenção",
                    Message = "Confirma exclusão do participante e convidado (caso exista) ?",
                    OkText = "Sim",
                    CancelText = "Não"
                });

                if (result)
                {
                    var participante = _dataAccess.GetParticipante(Participante.Id);
                    var funcionario = _dataAccess.GetFuncionario(participante.IdFuncionario);

                    _dataAccess.DeleteFuncionario(funcionario);

                    if (participante.TemConvidado)
                    {
                        var convidado = _dataAccess.GetConvidado(participante.IdConvidado);
                        _dataAccess.DeleteConvidado(convidado);
                    }

                    _dataAccess.DeleteParticipante(participante);

                    await _navigationService.GoBackAsync();

                    Util.GreenToast("Participante excluído com sucesso.");
                }
            }
            catch (Exception)
            {
                Util.RedToast("Atenção: Não foi possível excluir o participante.");
            }
        }

        /// <summary>
        /// Método que inclui um novo participante
        /// 
        /// Atenção: Faltou implementar o recurso de transação na inclusão 
        /// 
        /// </summary>
        private void IncluirParticipante()
        {
            DatabaseModels.Funcionario funcionarioDb = null;
            DatabaseModels.Convidado convidadoDb = null;
            DatabaseModels.Participante participanteDb = null;

            try
            {
                if (!DadosParticipanteInvalidos())
                {
                    throw new Exception("Atenção: Não foi possível incluir o novo participante.");
                }

                funcionarioDb = new DatabaseModels.Funcionario
                {
                    Nome = NomeFuncionario,
                    Sobrenome = SobrenomeFuncionario,
                    Idade = IdadeFuncionario + " Anos"
                };

                //INSERE O FUNCIONARIO
                var funcionarioId = _dataAccess.InsertFuncionario(funcionarioDb);

                participanteDb = new DatabaseModels.Participante
                {
                    IdChurrasco = Churrasco.Id,
                    IdFuncionario = funcionarioId,
                    ValorParticipante = RetornaValorPagoParticipante(FuncionarioBebe),
                    ParticipanteBebe = FuncionarioBebe,
                    DataCadastro = DateTime.Today
                };

                if (TemConvidado)
                {
                    if (!DadosConvidadoInvalidos())
                    {
                        throw new Exception("Atenção: Preencha corretamente os dados do convidado.");
                    }

                    convidadoDb = new DatabaseModels.Convidado
                    {
                        Nome = NomeConvidado,
                        Sobrenome = SobrenomeConvidado,
                        Idade = IdadeConvidado + " Anos",
                        Sexo = SexoConvidado
                    };

                    //INSERE O CONVIDADO
                    var convidadoId = _dataAccess.InsertConvidado(convidadoDb);

                    participanteDb.ValorConvidado = RetornaValorPagoConvidado(ConvidadoBebe);
                    participanteDb.IdConvidado = convidadoId;
                    participanteDb.ConvidadoBebe = ConvidadoBebe;
                    participanteDb.TemConvidado = TemConvidado;
                }

                //INSERE PARTICIPANTE
                _dataAccess.InsertParticipante(participanteDb);

                var lista = _dataAccess.GetParticipantes();
            }
            catch (Exception)
            {
                throw new Exception("Atenção: Não foi possível incluir o novo participante. Verifique os dados informados.");
            }
        }

        /// <summary>
        /// Método que atualiza o participante
        /// </summary>
        private void AtualizaParticipante(int participanteId)
        {
            var participanteDb = _dataAccess.GetParticipante(participanteId);
            var funcionarioDb = _dataAccess.GetFuncionario(participanteDb.IdFuncionario);
            DatabaseModels.Convidado convidadoDb = new DatabaseModels.Convidado();

            if (TemConvidado)
            {
                if (!DadosConvidadoInvalidos())
                {
                    throw new Exception("Atenção: Preencha corretamente os dados do convidado.");
                }

                convidadoDb.Nome = NomeConvidado;
                convidadoDb.Sobrenome = SobrenomeConvidado;
                convidadoDb.Idade = IdadeConvidado;
                convidadoDb.Sexo = SexoConvidado;

                if (participanteDb.IdConvidado > 0)
                {
                    convidadoDb = _dataAccess.GetConvidado(participanteDb.IdConvidado);
                    participanteDb.IdConvidado = convidadoDb.Id;
                }
                else
                {
                    var newConvidadoId = _dataAccess.InsertConvidado(convidadoDb);
                    participanteDb.IdConvidado = newConvidadoId;
                }

                funcionarioDb.Id = participanteDb.IdFuncionario;
                funcionarioDb.Nome = NomeFuncionario;
                funcionarioDb.Sobrenome = SobrenomeFuncionario;
                funcionarioDb.Idade = IdadeFuncionario;

                participanteDb.Id = participanteId;
                participanteDb.IdChurrasco = Churrasco.Id;
                participanteDb.IdFuncionario = funcionarioDb.Id;
                participanteDb.ParticipanteBebe = FuncionarioBebe;
                participanteDb.ConvidadoBebe = ConvidadoBebe;
                participanteDb.ValorParticipante = RetornaValorPagoParticipante(FuncionarioBebe);
                participanteDb.ValorConvidado = RetornaValorPagoConvidado(ConvidadoBebe);
                participanteDb.TemConvidado = TemConvidado;
                participanteDb.DataCadastro = DateTime.Today;
            }
            else
            {
                funcionarioDb.Id = participanteDb.IdFuncionario;
                funcionarioDb.Nome = NomeFuncionario;
                funcionarioDb.Sobrenome = SobrenomeFuncionario;
                funcionarioDb.Idade = IdadeFuncionario;

                participanteDb.Id = participanteId;
                participanteDb.IdChurrasco = Churrasco.Id;
                participanteDb.IdFuncionario = funcionarioDb.Id;
                participanteDb.ParticipanteBebe = FuncionarioBebe;
                participanteDb.ConvidadoBebe = false;
                participanteDb.ValorParticipante = RetornaValorPagoParticipante(FuncionarioBebe);
                participanteDb.ValorConvidado = 0;
                participanteDb.TemConvidado = TemConvidado;
                participanteDb.DataCadastro = DateTime.Today;
            }

            _dataAccess.UpdateParticipante(participanteDb);
        }

        /// <summary>
        /// Método que valida preenchimento dos dados do participante
        /// </summary>
        /// <returns></returns>
        private bool DadosParticipanteInvalidos()
        {
            return string.IsNullOrEmpty(NomeFuncionario) || string.IsNullOrEmpty(SobrenomeFuncionario) || string.IsNullOrEmpty(IdadeFuncionario) || int.Parse(IdadeFuncionario) >= 18;
        }

        /// <summary>
        /// Método que valida preenchimento dos dados do convidado
        /// </summary>
        /// <returns></returns>
        private bool DadosConvidadoInvalidos()
        {
            return string.IsNullOrEmpty(NomeConvidado) || string.IsNullOrEmpty(SobrenomeConvidado) || string.IsNullOrEmpty(IdadeConvidado) || string.IsNullOrEmpty(SexoConvidado) || int.Parse(IdadeConvidado) >= 18;
        }

        #endregion
    }
}