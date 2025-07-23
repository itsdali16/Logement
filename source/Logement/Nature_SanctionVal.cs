using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logement
{
    class Nature_LogementVal
    {
        public IList<Nature_Logement> list;
        public Nature_LogementVal()
        {
            list = new List<Nature_Logement>();
            var conn = Val.data;
            conn.open();
            var cmd = conn.cmd;
            cmd = conn.conn.CreateCommand();
            cmd.CommandText = "select * from Nature_Logement";
            var result = conn.result;
            result = cmd.ExecuteReader();


            while (result.Read())
            {
                list.Add(
                    new Nature_Logement()
                    {
                        code = result["code"].ToString(),
                        designation = result["designation"].ToString()
                    }
                    );

            }
            conn.close();
            //System.Windows.MessageBox.Show(list.Count.ToString());
        }

        public string add(Nature_Logement Nature_Logement)
        {
            try
            {

                var conn = Val.data;
                conn.open();
                var cmd = conn.cmd;
                cmd = conn.conn.CreateCommand();
                cmd.CommandText = @"insert into Nature_Logement  values(
                                    @code,
                                    @designation) ";
                cmd.Parameters.AddWithValue("@code", Nature_Logement.code);
                cmd.Parameters.AddWithValue("@designation", Nature_Logement.designation);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
                list.Add(Nature_Logement);
                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string edit(string old_code, Nature_Logement Nature_Logement)
        {
            try
            {

                var conn = Val.data;
                conn.open();
                var cmd = conn.cmd;
                cmd = conn.conn.CreateCommand();
                cmd.CommandText = @"update Nature_Logement set designation = @designation, code=@code where code=@oldcode ";
                cmd.Parameters.AddWithValue("@designation", Nature_Logement.designation);
                cmd.Parameters.AddWithValue("@code", Nature_Logement.code);
                cmd.Parameters.AddWithValue("@oldcode", old_code);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
                //list.Add(Nature_Logement);

                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string remove(Nature_Logement Nature_Logement)
        {
            try
            {

                var conn = Val.data;
                conn.open();
                var cmd = conn.cmd;
                cmd = conn.conn.CreateCommand();
                cmd.CommandText = @"delete from Nature_Logement where code=@code";
                cmd.Parameters.AddWithValue("@code", Nature_Logement.code);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
                list.Remove(Nature_Logement);
                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
