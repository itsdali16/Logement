using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logement
{
    class Val
    {
        public static DbContext data;
        public static ApparetementVal apparetements;
        public static BatimentVal batiments;
        public static LocataireVal locataires;
        public static HistoriqueVal historiques;
        //public static AffectationVal AffectationVal;
        public static MainWindow main;
        public static int limit=20;


        public static void initApparetements()
        {
            if (apparetements == null)
                apparetements = new ApparetementVal();
        }
        public static void initBatiments()
        {
            if (batiments == null)
                batiments = new BatimentVal();
        }
        public static void initLocataire()
        {
            if (locataires == null)
                locataires = new LocataireVal();
        }
        public static void initHistorique()
        {
            if (historiques == null)
                historiques = new HistoriqueVal();
        }
    }
}
