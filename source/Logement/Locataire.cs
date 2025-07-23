using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logement
{
    public class Locataire
    {
        public Int64 id { get; set; }
        public string nom_complet { get; set; }
        public string matricule { get; set; }
        public string grade { get; set; }
        public string etat_locataire { get; set; }
        public string position { get; set; }
    }
}
