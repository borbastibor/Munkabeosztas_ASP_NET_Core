using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Munkabeosztas_ASP_NET_Core.Models
{
    public class MunkaViewModel
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

        [Display(Name = "Gépjárművek")]
        public string SelectedGepjarmu { get; set; }

        public IEnumerable<SelectListItem> GepjarmuList { get; set; }

        [Display(Name = "Dolgozók")]
        public List<DolgozoMunkaViewModel> DolgozoList { get; set; }
    }
}
