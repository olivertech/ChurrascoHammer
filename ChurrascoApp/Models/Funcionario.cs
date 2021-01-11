using System;

namespace ChurrascoApp.Models
{
    /// <summary>
    /// Classe Model de Funcionario
    /// </summary>
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string NomeCompleto
        {
            get
            {
                return Nome + " " + Sobrenome;
            }
        }
        public string Idade { get; set; }
    }
}
