using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Munkabeosztas_ASP_NET_Core.Models
{
    public class Gepjarmu
    {
        [Key]
        public int GepjarmuId { get; set; }

        [Required(ErrorMessage = "Nincs megadva a típus!")]
        [Display(Name = "Típus")]
        public string Tipus { get; set; }

        [RegularExpression(@"^([A-Z]{3})\-(\d{3})$", ErrorMessage = "A rendszám formátuma rossz (ABC-123)!")]
        [StringLength(7, ErrorMessage = "A rendszám száma nem lehet több 7 karakternél!")]
        [Required(ErrorMessage = "Nincs megadva a rendszám!")]
        [Display(Name = "Rendszám")]
        public string Rendszam { get; set; }

        public virtual List<Munka> Munkak { get; set; }
    }
}
