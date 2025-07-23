using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logement
{
    class BatimentVal
    {
        public IList<Batiment> list;
        public BatimentVal()
        {
            Val.initApparetements();


            var result = from ap in Val.apparetements.list
                         group ap by ap.batiment into res
                         select new Batiment()
                         {
                             numero = res.Key,
                             appartements = res.ToList()
                         };
            list = result.ToList<Batiment>();
        }
    }
}
