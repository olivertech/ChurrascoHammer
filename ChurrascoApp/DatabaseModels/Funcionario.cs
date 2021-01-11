using SQLite;

namespace ChurrascoApp.DatabaseModels
{
    [Table("Funcionario")]
    public class Funcionario
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(15)]
        public string Nome { get; set; }
        [MaxLength(15)]
        public string Sobrenome { get; set; }
        public string Idade { get; set; }
    }
}
