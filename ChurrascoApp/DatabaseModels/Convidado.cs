using SQLite;

namespace ChurrascoApp.DatabaseModels
{
    [Table("Convidado")]
    public class Convidado
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(15)]
        public string Nome{ get; set; }
        [MaxLength(15)]
        public string Sobrenome{ get; set; }
        public string Idade{ get; set; }
        public string Sexo{ get; set; }
    }
}
