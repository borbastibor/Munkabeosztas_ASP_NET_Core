using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munkabeosztas_ASP_NET_Core.Models
{
    public class DolgozoMunka
    {
        public int DolgozoId { get; set; }
        public Dolgozo Dolgozo { get; set; }

        public string MunkaId { get; set; }
        public Munka Munka { get; set; }
    }
}
