using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Munkabeosztas_ASP_NET_Core.Models
{
    public class MunkaListViewModel
    {
        public int MunkaId { get; set; }

        [Required(ErrorMessage = "Nincs megadva a helyszín!")]
        [Display(Name = "Helyszín")]
        public string Helyszin { get; set; }

        [Required(ErrorMessage = "Nincs megadva a leírás!")]
        [Display(Name = "Leírás")]
        public string Leiras { get; set; }

        [Required(ErrorMessage = "Nincs megadva a dátum!")]
        [Display(Name = "Dátum")]
        public string Datum { get; set; }

        public int GepjarmuId { get; set; }

        public virtual Gepjarmu Gepjarmu { get; set; }

        [Display(Name = "Dolgozók")]
        public List<Dolgozo> DolgozoList { get; set; }
    }
}
