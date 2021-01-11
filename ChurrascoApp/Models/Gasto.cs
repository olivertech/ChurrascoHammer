using System;

namespace ChurrascoApp.Models
{
    /// <summary>
    /// Classe Model de Gasto
    /// </summary>
    public class Gasto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public string Tipo { get; set; }
        public DateTime DataCompra { get; set; }
    }
}
