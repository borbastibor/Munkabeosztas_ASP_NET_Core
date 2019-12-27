using System.ComponentModel.DataAnnotations;

namespace Munkabeosztas_ASP_NET_Core.Models
{
    public class Adminuser
    {
        [Key]
        public int AdminuserId { get; set; }

        [Required(ErrorMessage = "Nincs megadva admin felhasználónév!")]
        [Display(Name = "Admin felhaszálónév")]
        public string Username { get; set; }
    }
}
