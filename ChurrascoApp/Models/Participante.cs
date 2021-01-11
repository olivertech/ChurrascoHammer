using System;

namespace ChurrascoApp.Models
{
    /// <summary>
    /// Classe Model de Participante
    /// </summary>
    public class Participante
    {
        public Participante()
        {
            Funcionario = new Funcionario();
            Convidado = new Convidado
            {
                 Nome = "*****"
            };
        }

        public int Id { get; set; }
        public int IdChurrasco { get; set; }
        public Funcionario Funcionario { get; set; }
        public Convidado Convidado { get; set; }

        public decimal ValorParticipante { get; set; } = 0;
        public bool ParticipanteBebe { get; set; } = false;

        public decimal ValorConvidado { get; set; } = 0;
        public bool ConvidadoBebe { get; set; } = false;

        public bool TemConvidado { get; set; } = false;
    }
}
