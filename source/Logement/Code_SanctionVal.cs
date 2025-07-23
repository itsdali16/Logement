using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logement
{
    class Code_LogementVal
    {
        public IList<Code_Logement> list;
        public Code_LogementVal()
        {
            list = new List<Code_Logement>();
            var conn = Val.data;
            conn.open();
            var cmd = conn.cmd;
            cmd = conn.conn.CreateCommand();
            cmd.CommandText = "select * from Code_Logement";
            var result = conn.result;
            result = cmd.ExecuteReader();

               
            while (result.Read())
            {
                list.Add(
                    new Code_Logement()
                    {
                        id = Int64.Parse(result["id"].ToString()),
                        designation = result["designation"].ToString()
                    }
                    );

            }
            conn.close();
            //System.Windows.MessageBox.Show(list.Count.ToString());
        }

        public string add(Code_Logement Code_Logement)
        {
            try
            {

                var conn = Val.data;
                conn.open();
                var cmd = conn.cmd;
                cmd = conn.conn.CreateCommand();
                cmd.CommandText = @"insert into Code_Logement  values(
                                    null,
                                    @designation) ";
                cmd.Parameters.AddWithValue("@designation", Code_Logement.designation);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
                Code_Logement.id = Val.data.conn.LastInsertRowId;
                list.Add(Code_Logement);
                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string edit(Code_Logement Code_Logement)
        {
            try
            {

                var conn = Val.data;
                conn.open();
                var cmd = conn.cmd;
                cmd = conn.conn.CreateCommand();
                cmd.CommandText = @"update Code_Logement set designation = @designation where id=@id ";
                cmd.Parameters.AddWithValue("@designation", Code_Logement.designation);
                cmd.Parameters.AddWithValue("@id", Code_Logement.id);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
                //list.Add(Code_Logement);
                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string remove(Code_Logement Code_Logement)
        {
            try
            {

                var conn = Val.data;
                conn.open();
                var cmd = conn.cmd;
                cmd = conn.conn.CreateCommand();
                cmd.CommandText = @"delete from Code_Logement where id=@id";
                cmd.Parameters.AddWithValue("@id", Code_Logement.id);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
                list.Remove(Code_Logement);
                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
