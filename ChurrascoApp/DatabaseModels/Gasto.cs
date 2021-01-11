using SQLite;
using System;

namespace ChurrascoApp.DatabaseModels
{
    /// <summary>
    /// Classe que modela entidade do banco
    /// </summary>
    [Table("Gasto")]
    public class Gasto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int IdChurrasco { get; set; }

        [MaxLength(15)]
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public string Tipo { get; set; }
        public DateTime DataCompra { get; set; }
    }
}
