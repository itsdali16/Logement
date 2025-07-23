using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logement
{
    public class Historique
    {
        public Int64 id { get; set; }
        public Int64 id_locataire { get; set; }
        public Int64 id_appartement { get; set; }
        public DateTime? date_entree { get; set; }
        public DateTime? date_sortie { get; set; }

    }
}
