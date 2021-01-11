using Bogus;
using ChurrascoApp.Database;
using ChurrascoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace ChurrascoApp.Services
{
    /// <summary>
    /// Classe de serviços fakes pra retorno de dados
    /// </summary>
    public static class ChurrascoServices
    {
        #region Variáveis

        private static DataAccess _dataAccess = new DataAccess();
        private static List<Gasto> listaGastosComida = new List<Gasto>();
        private static List<Gasto> listaGastosBebida = new List<Gasto>();
        private static decimal TotalArrecadado = 0;
        public static decimal totalGastosComida = 0;
        public static decimal totalGastosBebida = 0;

        private const int ValorParticipanteQueNaoBebe = 20;
        private const int ValorParticipanteQueBebe = 40;

        private const int ValorConvidadoQueNaoBebe = 10;
        private const int ValorConvidadoQueBebe = 20;

        #endregion

        /// <summary>
        /// Método q retorna lista fake de churrascos em formato json
        /// </summary>
        /// <returns></returns>
        public static string GetChurrascosJson()
        {
            try
            {
                List<DatabaseModels.Churrasco> lista = _dataAccess.GetChurrascos();

                var jsonReturn = JsonSerializer.Serialize(lista);

                return jsonReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método q retorna uma lista de churrascos, recebida por parametro
        /// em formato json
        /// </summary>
        /// <param name="lista"></param>
        /// <returns></returns>
        public static string GetChurrascosJson(IEnumerable<Churrasco> lista)
        {
            try
            {
                var jsonReturn = JsonSerializer.Serialize(lista);

                return jsonReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Métodos Privados

        /// <summary>
        /// Método que retorna lista de churrascos no formato de lista de objetos
        /// </summary>
        /// <returns></returns>
        public static void CreateChurrascos()
        {
            int churrascoId;
            int churrascoDetailsId;

            List<Churrasco> lista = new List<Churrasco>();

            var churrascoHammer = new DatabaseModels.Churrasco
            {
                Titulo = "Churrasco Hammer 2021",
                DataRealizacao = new DateTime(2021, 1, 14),
                Ativo = "Ativo"
            };

            churrascoId = _dataAccess.InsertChurrasco(churrascoHammer);

            var churrascoDetailsHammer = new DatabaseModels.ChurrascoDetails
            {
                ChurrascoId = churrascoId,
                TotalArrecadado = TotalArrecadado,
                TotalGastoComida = totalGastosComida,
                TotalGastoBebida = totalGastosBebida
            };

            try
            {
                for (int i = 0; i <= 5; i++)
                {
                    var fakeChurrasco = new Faker<DatabaseModels.Churrasco>()
                        .RuleFor(x => x.Titulo, f => "Churrasco " + f.Lorem.Word())
                        .RuleFor(x => x.DataRealizacao, f => f.Date.Future())
                        .RuleFor(x => x.Ativo, f => "Encerrado");

                    churrascoId = _dataAccess.InsertChurrasco(fakeChurrasco);

                    var fakeChurrascoDetails = new Faker<DatabaseModels.ChurrascoDetails>()
                        .RuleFor(x => x.ChurrascoId, f => churrascoId)
                        .RuleFor(x => x.TotalArrecadado, f => f.Random.Decimal())
                        .RuleFor(x => x.TotalGastoComida, f => f.Random.Decimal())
                        .RuleFor(x => x.TotalGastoBebida, f => f.Random.Decimal());

                    churrascoDetailsId = _dataAccess.InsertChurrascoDetails(fakeChurrascoDetails);
                }

                GetGastos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método que retorna lista de participantes no formato de lista de objetos
        /// </summary>
        /// <returns></returns>
        public static void CreateParticipantes()
        {
            int churrascoId = 0;
            int participanteId = 0;
            int funcionarioId = 0;
            int convidadoId = 0;
            bool temConvidado = false;

            var idades = new[] { "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30" };
            var sexo = new[] { "Masculino", "Feminino" };
            var bebe = new[] { true, false };

            var fakeParticipante = new Faker<DatabaseModels.Participante>();

            Random random = new Random();

            List<Participante> lista = new List<Participante>();

            try
            {
                for (int i = 0; i <= 9; i++)
                {
                    temConvidado = false;

                    var fakeFuncionario = new Faker<DatabaseModels.Funcionario>()
                        .RuleFor(x => x.Nome, f => f.Person.FirstName)
                        .RuleFor(x => x.Sobrenome, f => f.Person.LastName)
                        .RuleFor(x => x.Idade, f => f.PickRandom(idades) + " Anos");

                    //INSERE FUNCIONARIO
                    funcionarioId = _dataAccess.InsertFuncionario(fakeFuncionario.Generate());

                    //Entra aleatoriamente pra criar convidado
                    if (random.Next(1, 4) % 2 == 0)
                    {
                        temConvidado = true;

                        var fakeConvidado = new Faker<DatabaseModels.Convidado>()
                            .RuleFor(x => x.Nome, f => f.Person.FirstName)
                            .RuleFor(x => x.Sobrenome, f => f.Person.LastName)
                            .RuleFor(x => x.Idade, f => f.PickRandom(idades) + " Anos")
                            .RuleFor(x => x.Sexo, f => f.PickRandom(sexo));

                        //INSERE CONVIDADO
                        convidadoId = _dataAccess.InsertConvidado(fakeConvidado.Generate());
                    }

                    churrascoId = _dataAccess.GetChurrascos().Where(x => x.Titulo == "Churrasco Hammer 2021").FirstOrDefault().Id;

                    if (temConvidado)
                    {
                        fakeParticipante = new Faker<DatabaseModels.Participante>()
                            .RuleFor(x => x.IdChurrasco, f => churrascoId)
                            .RuleFor(x => x.IdFuncionario, f => funcionarioId)
                            .RuleFor(x => x.IdConvidado, f => convidadoId)
                            .RuleFor(x => x.TemConvidado, temConvidado)
                            .RuleFor(x => x.ParticipanteBebe, f => f.PickRandom(bebe))
                            .RuleFor(x => x.ConvidadoBebe, f => f.PickRandom(bebe));
                    }
                    else
                    {
                        fakeParticipante = new Faker<DatabaseModels.Participante>()
                            .RuleFor(x => x.IdChurrasco, f => churrascoId)
                            .RuleFor(x => x.IdFuncionario, f => funcionarioId)
                            .RuleFor(x => x.TemConvidado, temConvidado)
                            .RuleFor(x => x.ParticipanteBebe, f => f.PickRandom(bebe))
                            .RuleFor(x => x.ConvidadoBebe, f => false);
                    }

                    //INSERE PARTICIPANTE
                    participanteId = _dataAccess.InsertParticipante(fakeParticipante.Generate());

                    var participante = _dataAccess.GetParticipante(participanteId);

                    participante.ValorParticipante = participante.ParticipanteBebe ? ValorParticipanteQueBebe : ValorParticipanteQueNaoBebe;

                    if (temConvidado)
                        participante.ValorConvidado = participante.ConvidadoBebe ? ValorConvidadoQueBebe : ValorConvidadoQueNaoBebe;
                    else
                        participante.ValorConvidado = 0;

                    _dataAccess.UpdateParticipante(participante);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método q retorna lista de gastos no formato de lista de objetos
        /// </summary>
        /// <returns></returns>
        private static void GetGastos()
        {
            var tipos = new[] { "C", "B" }; // comida | bebida

            try
            {
                var churrascoId = _dataAccess.GetChurrascos().Where(x => x.Titulo == "Churrasco Hammer 2021").FirstOrDefault().Id;

                for (int i = 0; i <= 15; i++)
                {
                    var fakeGasto = new Faker<DatabaseModels.Gasto>()
                        .RuleFor(x => x.IdChurrasco, f => churrascoId)
                        .RuleFor(x => x.Descricao, f => f.Lorem.Word())
                        .RuleFor(x => x.Valor, f => f.Random.Number(5, 25))
                        .RuleFor(x => x.Tipo, f => f.PickRandom(tipos))
                        .RuleFor(x => x.DataCompra, f => f.Date.Past());

                    _dataAccess.InsertGasto(fakeGasto);
                }

                var listaGastosComidaDb = _dataAccess.GetGastosPorTipo(churrascoId, "C");
                var listaGastosBebidaDb = _dataAccess.GetGastosPorTipo(churrascoId, "B");

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

                    totalGastosComida += gasto.Valor;

                    listaGastosComida.Add(gasto);
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

                    totalGastosBebida += gasto.Valor;

                    listaGastosBebida.Add(gasto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
