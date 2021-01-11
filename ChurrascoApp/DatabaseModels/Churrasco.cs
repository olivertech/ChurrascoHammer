using SQLite;
using System;

namespace ChurrascoApp.DatabaseModels
{
    /// <summary>
    /// Classe que modela entidade do banco
    /// </summary>
    [Table("Churrasco")]
    public class Churrasco
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(25)]
        public string Titulo { get; set; }
        public DateTime DataRealizacao { get; set; }
        public string Ativo { get; set; }
    }
}
