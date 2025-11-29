using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeFitt.Models
{
    public class Trening_K
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Sesja treningowa")]
        public int SesjaId { get; set; }

        [ForeignKey("SesjaId")]
        public virtual Sesja? Sesja{ get; set; }

        [Display(Name = "Typ ćwiczenia")]
        public int TypyId { get; set; }

        [ForeignKey("TypyId")]
        public virtual Typy? Typy { get; set; }

        [Range(0, 1000, ErrorMessage = "Obciążenie musi być liczbą dodatnią (max 1000)")]
        [Display(Name = "Obciążenie (kg)")]
        public double Ciezar { get; set; }

        [Required(ErrorMessage = "Podaj liczbę serii")]
        [Range(1, 100, ErrorMessage = "Musisz wykonać przynajmniej 1 serię")]
        [Display(Name = "Liczba serii")]
        public int Seria {  get; set; }

        [Required (ErrorMessage = "Podaj liczbę powtórzeń" )]
        [Range(1,1000, ErrorMessage = "Liczba powtórzeń musi być większa od 0")]
        [Display(Name = "Licza powtórzeń w serii")]
        public int Powtorzenia { get; set; }

        [Display(Name = "Utworzone przez")]
        public string? CreatedById { get; set; }

        [Display(Name = "Utworzone przez")]
        public virtual Uzytkownicy? CreatedBy { get; set; }


    }
}
