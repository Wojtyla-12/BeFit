using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeFitt.Models
{
    public class Sesja
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage = "Podaj czas startu")]
        [DataType(DataType.Time)]
        [Display(Name  = "Początek treningu")]
        public TimeSpan Start_Czas {  get; set; }

        [Required(ErrorMessage = "Podaj czas końca")]
        [DataType(DataType.Time)]
        [Display(Name = "Koniec treningu")]
        public TimeSpan Koniec_Czas { get; set; }

        [Required(ErrorMessage = "Podaj datę rozpoczęcia")]
        [DataType(DataType.Date)]
        [Display(Name = "Data rozpoczęcia")]
        public DateTime Dzien_Start {  get; set; }

        [Required(ErrorMessage = "Podaj datę zakończenia")]
        [DataType(DataType.Date)] 
        [Display(Name = "Data zakończenia")]
        public DateTime Dzien_Koniec {  get; set; }

        [Display(Name = "Utworzone przez")]
        public string? CreatedById { get; set; }

        [Display(Name = "Utworzone przez")]
        public virtual Uzytkownicy? CreatedBy { get; set; }

    }
}
