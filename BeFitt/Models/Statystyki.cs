using System.ComponentModel.DataAnnotations;

namespace BeFitt.Models
{
    public class Statystyki
    {
        [Display(Name = "Nazwa Ćwiczenia")]
        public string NazwaCwiczenia { get; set; }
        [Display(Name = "Ile razy zostało wykonane")]
        public int Ile_razy { get; set; }
        [Display(Name = "Jaka była liczba powtórzeń")]
        public int Ile_powtorzen { get; set; }
        [Display(Name = "Średnia")]
        public double Srednia { get; set; }
        [Display(Name = "Maksymalne obciążenie")]
        public double Maks { get; set; }
    }
}
