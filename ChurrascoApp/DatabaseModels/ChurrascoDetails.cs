using SQLite;

namespace ChurrascoApp.DatabaseModels
{
    /// <summary>
    /// Classe que modela entidade do banco
    /// </summary>
    [Table("ChurrascoDetail")]
    public class ChurrascoDetails
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int ChurrascoId { get; set; }
        public decimal TotalArrecadado { get; set; }
        public decimal TotalGastoComida { get; set; }
        public decimal TotalGastoBebida { get; set; }
    }
}
