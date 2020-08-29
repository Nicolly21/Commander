using System.ComponentModel.DataAnnotations;

namespace Commander.Models
{
    public class Command
    {
        [Key] //Opcional pois Migrations considera a primeira prop como primary key e not null (padrão)
        public int Id { get; set; }

        [Required] //Para not null
        [MaxLength(100)] //Tamanho do varchar
        public string HowTo { get; set; }

        [Required]
        public string Line { get; set; }

        [Required]
        public string Platform { get; set; }
    }
}