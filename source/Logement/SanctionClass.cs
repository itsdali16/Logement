using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logement
{
    public class LogementClass
    {
        public Int64 id { get; set; }
        public Int64 id_agent { get; set; }
        public Int64 code { get; set; }
        public DateTime date { get; set; }
        public string num_decision { get; set; }
        public string observation { get; set; }
        public string observation_ar { get; set; }
        public string num_ordre { get; set; }
        public int nbr_jours { get; set; }
        public string nature { get; set; }
        public Nature_Logement nature_Logement { get; set; }
        public Code_Logement code_Logement { get; set; }

        public void setCode_Logement()
        {
            Val.initCode_Logements();
            if(code_Logement == null)
            {
                code_Logement = Val.code_Logements.list.Where(cs => cs.id == code).FirstOrDefault();
            }
        }

        public void setNature_Logement()
        {
            Val.initNature_Logements();
            if (nature_Logement == null && nature != null)
            {
                nature_Logement = Val.nature_Logements.list.Where(ns => ns.code == nature).FirstOrDefault();
            }
        }
    }
}
