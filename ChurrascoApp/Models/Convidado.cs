using System;

namespace ChurrascoApp.Models
{
    /// <summary>
    /// Classe Model de Convidado
    /// </summary>
    public class Convidado
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
        public string Sexo { get; set; }
    }
}
