using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logement
{
    class HistoriqueVal
    {
        public IList<Historique> list;
        public HistoriqueVal()
        {
            var conn = Val.data;
            //try
            //{

            list = new List<Historique>();
            conn.open();
            var cmd = conn.cmd;
            cmd = conn.conn.CreateCommand();
            cmd.CommandText = "select * from historique_appartement";
            var result = conn.result;
            result = cmd.ExecuteReader();

            while (result.Read())
            {
                list.Add(
                    new Historique()
                    {
                        id = Int64.Parse(result["id"].ToString()),
                        id_locataire = Int64.Parse(result["id_locataire"].ToString()),
                        id_appartement = Int64.Parse(result["id_appartement"].ToString()),
                        date_entree = Function.ConvertDateTime(result["date_entree"].ToString()),
                        date_sortie = Function.ConvertDateTime(result["date_sortie"].ToString())
                    }
                    );
            }
            conn.close();
            //}
            //catch (Exception e)
            //{
            //    conn.close();
            //    System.Windows.MessageBox.Show(e.Message);
            //}
            //System.Windows.MessageBox.Show(list.Count.ToString());
        }

        public string add(Historique Historique)
        {
            var conn = Val.data;
            try
            {

                conn.open();
                var cmd = conn.cmd;
                cmd = conn.conn.CreateCommand();
                cmd.CommandText = @"insert into historique_appartement  values(
                                    null,
                                    @id_locataire,
                                    @id_appartement,
                                    @date_entree,
                                    @date_sortie) ";


                cmd.Parameters.AddWithValue("@id_locataire", Historique.id_locataire);
                cmd.Parameters.AddWithValue("@id_locataire", Historique.id_locataire);
                cmd.Parameters.AddWithValue("@id_appartement", Historique.id_appartement);
                cmd.Parameters.AddWithValue("@date_entree", Historique.date_entree);
                cmd.Parameters.AddWithValue("@date_sortie", Historique.date_sortie);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
                Historique.id = conn.conn.LastInsertRowId;
                list.Add(Historique);
                //System.Windows.MessageBox.Show("Opération terminée avec Succès");


                conn.close();
                return "";
            }
            catch (Exception e)
            {
                conn.close();
                return e.Message;
                //System.Windows.MessageBox.Show(e.Message);
            }
        }

        public string edit(Historique Historique)
        {
            var conn = Val.data;
            try
            {

                conn.open();
                var cmd = conn.cmd;
                cmd = conn.conn.CreateCommand();
                cmd.CommandText = @"update historique_appartement set id_locataire = @id_locataire,
                                    id_appartement = @id_appartement,
                                    date_entree=@date_entree,
                                    date_sortie=@date_sortie where id=@id ";
                cmd.Parameters.AddWithValue("@id", Historique.id);
                cmd.Parameters.AddWithValue("@id_locataire", Historique.id_locataire);
                cmd.Parameters.AddWithValue("@id_appartement", Historique.id_appartement);
                cmd.Parameters.AddWithValue("@date_entree", Historique.date_entree);
                cmd.Parameters.AddWithValue("@date_sortie", Historique.date_sortie);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
                //list.Add(Historique);


                conn.close();

                return "";
            }


            catch (Exception e)
            {
                conn.close();
                return e.Message;
            }
        }

        public string remove(Historique Historique)
        {
            var conn = Val.data;
            try
            {
                conn.open();
                var cmd = conn.cmd;
                cmd = conn.conn.CreateCommand();
                cmd.CommandText = @"delete from historique_appartement where id=@id";
                cmd.Parameters.AddWithValue("@id", Historique.id);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
                list.Remove(Historique);
                //System.Windows.MessageBox.Show("Opération terminée avec Succès");

                conn.close();

                return "";

            }
            catch (Exception e)
            {
                conn.close();
                return e.Message;
            }
        }
        //public string remove(Int64 id)
        //{
        //    var conn = Val.data;
        //    try
        //    {
        //        conn.open();
        //        var cmd = conn.cmd;
        //        cmd = conn.conn.CreateCommand();
        //        cmd.CommandText = @"delete from historique_appartement where id=@id";
        //        cmd.Parameters.AddWithValue("@id", id);
        //        cmd.Prepare();

        //        cmd.ExecuteNonQuery();
        //        list.Remove(Historique);
        //        //System.Windows.MessageBox.Show("Opération terminée avec Succès");

        //        conn.close();

        //        return "";

        //    }
        //    catch (Exception e)
        //    {
        //        conn.close();
        //        return e.Message;
        //    }
        //}
    }
}
