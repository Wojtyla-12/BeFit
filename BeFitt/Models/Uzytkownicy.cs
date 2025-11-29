

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeFitt.Models
    
{
    public class Uzytkownicy: IdentityUser
    {
        [InverseProperty("CreatedBy")]
        public virtual ICollection<Sesja> Sesje { get; set; }

        [InverseProperty("CreatedBy")]
        public virtual ICollection<Trening_K> WykonaneCwiczenia { get; set; }
    }
}
