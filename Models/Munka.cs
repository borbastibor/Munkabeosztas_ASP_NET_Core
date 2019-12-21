using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Munkabeosztas_ASP_NET_Core.Models
{
    public class Munka
    {
        [Key]
        public int MunkaId { get; set; }

        [Required]
        [Display(Name = "Helyszín")]
        public string Helyszin { get; set; }

        [Required]
        [Display(Name = "Dátum")]
        public string Datum { get; set; }

        [Required]
        [Display(Name = "Leírás")]
        public string Leiras { get; set; }

        public int GepjarmuId { get; set; }

        [ForeignKey("GepjarmuId")]
        public virtual Gepjarmu Gepjarmu { get; set; }

        public virtual ICollection<DolgozoMunka> DolgozoMunkak { get; set; }
    }
}
