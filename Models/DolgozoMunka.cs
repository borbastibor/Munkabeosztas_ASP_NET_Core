using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Munkabeosztas_ASP_NET_Core.Models
{
    public class DolgozoMunka
    {
        public int DolgozoId { get; set; }

        [ForeignKey("DolgozoId")]
        public Dolgozo Dolgozo { get; set; }


        public int MunkaId { get; set; }

        [ForeignKey("MunkaId")]
        public Munka Munka { get; set; }
    }
}
