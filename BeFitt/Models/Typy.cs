using System.ComponentModel.DataAnnotations;

namespace BeFitt.Models
{
    public class Typy
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(30)]
       public string Name { get; set; }
        

    }
}
