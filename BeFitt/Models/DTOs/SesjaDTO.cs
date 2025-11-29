using System.ComponentModel.DataAnnotations;

namespace BeFitt.Models.DTOs
{
    public class SesjaDTO
    {
        [Required(ErrorMessage = "Podaj czas startu")]
        [DataType(DataType.Time)]
        [Display(Name = "Początek treningu")]
        public TimeSpan Start_Czas { get; set; }

        [Required(ErrorMessage = "Podaj czas końca")]
        [DataType(DataType.Time)]
        [Display(Name = "Koniec treningu")]
        public TimeSpan Koniec_Czas { get; set; }

        [Required(ErrorMessage = "Podaj datę rozpoczęcia")]
        [DataType(DataType.Date)]
        [Display(Name = "Data rozpoczęcia")]
        public DateTime Dzien_Start { get; set; }

        [Required(ErrorMessage = "Podaj datę zakończenia")]
        [DataType(DataType.Date)]
        [Display(Name = "Data zakończenia")]
        public DateTime Dzien_Koniec { get; set; }
    }
}