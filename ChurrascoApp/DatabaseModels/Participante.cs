using SQLite;
using System;

namespace ChurrascoApp.DatabaseModels
{
    /// <summary>
    /// Classe que modela entidade do banco
    /// </summary>
    [Table("Participante")]
    public class Participante
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int IdChurrasco { get; set; }
        public int IdFuncionario { get; set; }
        public int IdConvidado { get; set; }
        public decimal ValorParticipante { get; set; } = 0;
        public decimal ValorConvidado { get; set; } = 0;
        public bool ParticipanteBebe { get; set; } = false;
        public bool ConvidadoBebe { get; set; } = false;
        public bool TemConvidado { get; set; } = false;
        public DateTime DataCadastro { get; set; }
    }
}
