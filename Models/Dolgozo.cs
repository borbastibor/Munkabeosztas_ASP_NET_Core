using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Munkabeosztas_ASP_NET_Core.Models
{
    public class Dolgozo
    {
        [Key]
        public int DolgozoId { get; set; }

        [Required(ErrorMessage = "Nincs megadva a családnév!")]
        [Display(Name = "Családnév")]
        public string Csaladnev { get; set; }

        [Required(ErrorMessage = "Nincs megadva a keresztnév!")]
        [Display(Name = "Keresztnév")]
        public string Keresztnev { get; set; }

        public List<DolgozoMunka> DolgozoMunkak { get; set; }
    }
}
