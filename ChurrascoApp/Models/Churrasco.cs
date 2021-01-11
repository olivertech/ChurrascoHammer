using System;
using System.Collections.Generic;

namespace ChurrascoApp.Models
{
    /// <summary>
    /// Classe model de Churrasco
    /// </summary>
    public class Churrasco
    {
        public Churrasco()
        {
            ChurrascoDetails = new ChurrascoDetails();
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateTime DataRealizacao { get; set; }
        public string Ativo { get; set; }
        public ChurrascoDetails ChurrascoDetails { get; set; }
    }

    public class ChurrascoDetails
    {
        public ChurrascoDetails()
        {
            ListaParticipantes = new List<Participante>();
        }

        public decimal TotalArrecadado { get; set; }
        public decimal TotalGastoComida { get; set; }
        public decimal TotalGastoBebida { get; set; }
        public List<Participante> ListaParticipantes { get; set; }
        public List<Gasto> ListaGastosComida { get; set; }
        public List<Gasto> ListaGastosBebida { get; set; }
    }
}
