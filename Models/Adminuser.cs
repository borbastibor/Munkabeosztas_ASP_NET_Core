using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Munkabeosztas_ASP_NET_Core.Models
{
    public class Adminuser
    {
        [Key]
        public int AdminuserId { get; set; }

        [Required]
        [Display(Name = "Admin felhaszálónév")]
        public string Username { get; set; }
    }
}
