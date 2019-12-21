using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Munkabeosztas_ASP_NET_Core.Models
{
    public class MunkaEditViewModel
    {
        public int MunkaId { get; set; }

        [Required(ErrorMessage = "Nincs megadva a helyszín!")]
        [Display(Name = "Helyszín")]
        public string Helyszin { get; set; }

        public string Leiras { get; set; }

        public string Datum { get; set; }


    }
}
