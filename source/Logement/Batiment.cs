using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logement
{
    public class Batiment
    {
        public string numero { get; set; }
        public IList<Appartement> appartements { get; set; }
    }
}
