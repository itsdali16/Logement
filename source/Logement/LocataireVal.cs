using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logement
{
    class LocataireVal
    {
        public IList<Locataire> list;
        public LocataireVal()
        {
            var conn = Val.data;
            //try
            //{

            list = new List<Locataire>();
            conn.open();
            var cmd = conn.cmd;
            cmd = conn.conn.CreateCommand();
            cmd.CommandText = "select * from Locataire";
            var result = conn.result;
            result = cmd.ExecuteReader();

            while (result.Read())
            {
                list.Add(
                    new Locataire()
                    {
                        id = Int64.Parse(result["id"].ToString()),
                        nom_complet = result["nom_complet"].ToString(),
                        matricule = result["matricule"].ToString(),
                        grade = result["grade"].ToString(),
                        etat_locataire = result["etat_locataire"].ToString(),
                        position = result["position"].ToString()
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

        public string add(Locataire Locataire)
        {
            var conn = Val.data;
            try
            {

                conn.open();
                var cmd = conn.cmd;
                cmd = conn.conn.CreateCommand();
                cmd.CommandText = @"insert into Locataire  values(
                                    null,
                                    @nom_complet,
                                    @matricule,
                                    @grade,
                                    @etat_locataire,
                                    @position) ";


                cmd.Parameters.AddWithValue("@nom_complet", Locataire.nom_complet);
                cmd.Parameters.AddWithValue("@matricule", Locataire.matricule);
                cmd.Parameters.AddWithValue("@grade", Locataire.grade);
                cmd.Parameters.AddWithValue("@etat_locataire", Locataire.etat_locataire);
                cmd.Parameters.AddWithValue("@position", Locataire.position);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
                Locataire.id = conn.conn.LastInsertRowId;
                list.Add(Locataire);
                //System.Windows.MessageBox.Show("Opération terminée avec Succès");


                conn.close();
                return "";
            }
            catch (Exception e)
            {
                conn.close();
                //System.Windows.MessageBox.Show(e.Message);
                return e.Message;
            }
        }

        public string edit(Locataire Locataire)
        {
            var conn = Val.data;


            try
            {
                conn.open();


                SQLiteTransaction transaction = Val.data.conn.BeginTransaction(IsolationLevel.ReadCommitted);

                var cmd = conn.cmd;
                cmd = conn.conn.CreateCommand();
                cmd.Transaction = transaction;
                cmd.CommandText = @"update Locataire set nom_complet = @nom_complet, 
                                    matricule = @matricule,
                                    grade = @grade,
                                    etat_locataire = @etat_locataire,
                                    position = @position where id=@id ";
                cmd.Parameters.AddWithValue("@id", Locataire.id);
                cmd.Parameters.AddWithValue("@nom_complet", Locataire.nom_complet);
                cmd.Parameters.AddWithValue("@matricule", Locataire.matricule);
                cmd.Parameters.AddWithValue("@grade", Locataire.grade);
                cmd.Parameters.AddWithValue("@etat_locataire", Locataire.etat_locataire);
                cmd.Parameters.AddWithValue("@position", Locataire.position);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
                //list.Add(Locataire);
                //System.Windows.MessageBox.Show("Opération terminée avec Succès");

                /*************************/

                var cmd2 = conn.conn.CreateCommand();
                cmd2.Transaction = transaction;
                cmd2.CommandText = @"update Appartement set nom_complet = @nom_complet, 
                                    matricule = @matricule,
                                    grade = @grade,
                                    etat_locataire = @etat_locataire,
                                    position = @position where id_locataire=@id ";
                cmd2.Parameters.AddWithValue("@id", Locataire.id);
                cmd2.Parameters.AddWithValue("@nom_complet", Locataire.nom_complet);
                cmd2.Parameters.AddWithValue("@matricule", Locataire.matricule);
                cmd2.Parameters.AddWithValue("@grade", Locataire.grade);
                cmd2.Parameters.AddWithValue("@etat_locataire", Locataire.etat_locataire);
                cmd2.Parameters.AddWithValue("@position", Locataire.position);
                cmd2.Prepare();

                cmd2.ExecuteNonQuery();

                /*************************/
                transaction.Commit();

                IList<Appartement> apps = Val.apparetements.list.Where(ap => ap.id_locataire == Locataire.id).ToList();
                foreach (Appartement app in apps)
                {
                    app.matricule = Locataire.matricule;
                    app.nom_complet = Locataire.nom_complet;
                    app.grade = Locataire.grade;
                    app.position = Locataire.position;
                    app.etat_locataire = Locataire.etat_locataire;
                }
                conn.close();

                return "";
            }
            catch (Exception e)
            {
                conn.close();
                //System.Windows.MessageBox.Show(e.Message);
                return e.Message;
            }
        }

        public void edit(Int64 id, string nom_complet, string matricule, string grade, string etat_locataire, string position)
        {
            var conn = Val.data;
            try
            {
                conn.open();
                var cmd = conn.cmd;
                cmd = conn.conn.CreateCommand();
                cmd.CommandText = @"update Locataire set nom_complet = @nom_complet, 
                                    matricule = @matricule,
                                    grade = @grade,
                                    etat_locataire = @etat_locataire,
                                    position = @position where id=@id ";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nom_complet", nom_complet);
                cmd.Parameters.AddWithValue("@matricule", matricule);
                cmd.Parameters.AddWithValue("@grade", grade);
                cmd.Parameters.AddWithValue("@etat_locataire", etat_locataire);
                cmd.Parameters.AddWithValue("@position", position);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
                //list.Add(Locataire);
                //System.Windows.MessageBox.Show("Opération terminée avec Succès");

                conn.close();
            }
            catch (Exception e)
            {
                conn.close();
                //System.Windows.MessageBox.Show(e.Message);
            }
        }

        public void remove(Locataire Locataire)
        {
            try
            {

                var conn = Val.data;
                conn.open();
                var cmd = conn.cmd;
                cmd = conn.conn.CreateCommand();
                cmd.CommandText = @"delete from Locataire where id=@id";
                cmd.Parameters.AddWithValue("@id", Locataire.id);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
                list.Remove(Locataire);
                System.Windows.MessageBox.Show("Opération terminée avec Succès");
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
            }
        }



    }
}
