using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logement
{
    class ApparetementVal
    {
        public IList<Appartement> list;
        public ApparetementVal()
        {
            var conn = Val.data;
            //try
            //{

            list = new List<Appartement>();
            conn.open();
            var cmd = conn.cmd;
            cmd = conn.conn.CreateCommand();
            cmd.CommandText = "select * from Appartement";
            var result = conn.result;
            result = cmd.ExecuteReader();

            while (result.Read())
            {
                list.Add(
                    new Appartement()
                    {
                        id = Int64.Parse(result["id"].ToString()),
                        batiment = result["batiment"].ToString(),
                        num_porte = Int32.Parse(result["num_porte"].ToString()),
                        nbr_piece = Int32.Parse(result["nbr_piece"].ToString()),
                        id_locataire = Function.ConvertInt64(result["id_locataire"].ToString()),
                        nom_complet = result["nom_complet"].ToString(),
                        matricule = result["matricule"].ToString(),
                        grade = result["grade"].ToString(),
                        etat_locataire = result["etat_locataire"].ToString(),
                        position = result["position"].ToString(),
                        charge = Function.ConvertDouble(result["charge"].ToString()),
                        date_entree = Function.ConvertDateTime(result["date_entree"].ToString()),
                        date_sortie = Function.ConvertDateTime(result["date_sortie"].ToString()),
                        observation = result["observation"].ToString()
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

        public void add(Appartement Appartement)
        {
            var conn = Val.data;
            try
            {

                conn.open();
                var cmd = conn.cmd;
                cmd = conn.conn.CreateCommand();
                cmd.CommandText = @"insert into Appartement  values(
                                    null,
                                    @batiment,
                                    @num_porte,
                                    @nbr_piece,
                                    @id_locataire,
                                    @nom_complet,
                                    @matricule,
                                    @grade,
                                    @etat_locataire,
                                    @position,
                                    @charge,
                                    @date_entree,
                                    @date_sortie,
                                    @observation) ";


                cmd.Parameters.AddWithValue("@batiment", Appartement.batiment);
                cmd.Parameters.AddWithValue("@num_porte", Appartement.num_porte);
                cmd.Parameters.AddWithValue("@nbr_piece", Appartement.nbr_piece);
                cmd.Parameters.AddWithValue("@id_locataire", Appartement.id_locataire);
                cmd.Parameters.AddWithValue("@nom_complet", Appartement.nom_complet);
                cmd.Parameters.AddWithValue("@matricule", Appartement.matricule);
                cmd.Parameters.AddWithValue("@grade", Appartement.grade);
                cmd.Parameters.AddWithValue("@etat_locataire", Appartement.etat_locataire);
                cmd.Parameters.AddWithValue("@position", Appartement.position);
                cmd.Parameters.AddWithValue("@charge", Appartement.charge);
                cmd.Parameters.AddWithValue("@date_entree", Appartement.date_entree);
                cmd.Parameters.AddWithValue("@date_sortie", Appartement.date_sortie);
                cmd.Parameters.AddWithValue("@observation", Appartement.observation);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
                Appartement.id = conn.conn.LastInsertRowId;
                list.Add(Appartement);
                System.Windows.MessageBox.Show("Opération terminée avec Succès");


                conn.close();
            }
            catch (Exception e)
            {
                conn.close();
                System.Windows.MessageBox.Show(e.Message);
            }
        }

        public string edit(Appartement Appartement)
        {
            var conn = Val.data;
            try
            {

                conn.open();
                var cmd = conn.cmd;
                cmd = conn.conn.CreateCommand();
                cmd.CommandText = @"update Appartement set batiment = @batiment,
                                    num_porte = @num_porte,
                                    nbr_piece = @nbr_piece,
                                    id_locataire = @id_locataire,
                                    nom_complet = @nom_complet, 
                                    matricule = @matricule,
                                    grade = @grade,
                                    etat_locataire = @etat_locataire,
                                    position = @position,
                                    charge = @charge,
                                    date_entree=@date_entree,
                                    date_sortie=@date_sortie,
                                    observation = @observation where id=@id ";
                cmd.Parameters.AddWithValue("@id", Appartement.id);
                cmd.Parameters.AddWithValue("@batiment", Appartement.batiment);
                cmd.Parameters.AddWithValue("@num_porte", Appartement.num_porte);
                cmd.Parameters.AddWithValue("@nbr_piece", Appartement.nbr_piece);
                cmd.Parameters.AddWithValue("@id_locataire", Appartement.id_locataire);
                cmd.Parameters.AddWithValue("@nom_complet", Appartement.nom_complet);
                cmd.Parameters.AddWithValue("@matricule", Appartement.matricule);
                cmd.Parameters.AddWithValue("@grade", Appartement.grade);
                cmd.Parameters.AddWithValue("@etat_locataire", Appartement.etat_locataire);
                cmd.Parameters.AddWithValue("@position", Appartement.position);
                cmd.Parameters.AddWithValue("@charge", Appartement.charge);
                cmd.Parameters.AddWithValue("@date_entree", Appartement.date_entree);
                cmd.Parameters.AddWithValue("@date_sortie", Appartement.date_sortie);
                cmd.Parameters.AddWithValue("@observation", Appartement.observation);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
                //list.Add(Appartement);


                conn.close();

                return "";
            }


            catch (Exception e)
            {
                conn.close();
                return e.Message;
            }
        }
        public string editAppLoc(Appartement Appartement)
        {
            var conn = Val.data;


            try
            {
                conn.open();


                SQLiteTransaction transaction = Val.data.conn.BeginTransaction(IsolationLevel.ReadCommitted);

                var cmd = conn.cmd;
                cmd = conn.conn.CreateCommand();
                cmd.Transaction = transaction;
                cmd.CommandText = @"update Appartement set batiment = @batiment,
                                    num_porte = @num_porte,
                                    nbr_piece = @nbr_piece,
                                    id_locataire = @id_locataire,
                                    nom_complet = @nom_complet, 
                                    matricule = @matricule,
                                    grade = @grade,
                                    etat_locataire = @etat_locataire,
                                    position = @position,
                                    charge = @charge,
                                    date_entree=@date_entree,
                                    date_sortie=@date_sortie,
                                    observation = @observation where id=@id ";
                cmd.Parameters.AddWithValue("@id", Appartement.id);
                cmd.Parameters.AddWithValue("@batiment", Appartement.batiment);
                cmd.Parameters.AddWithValue("@num_porte", Appartement.num_porte);
                cmd.Parameters.AddWithValue("@nbr_piece", Appartement.nbr_piece);
                cmd.Parameters.AddWithValue("@id_locataire", Appartement.id_locataire);
                cmd.Parameters.AddWithValue("@nom_complet", Appartement.nom_complet);
                cmd.Parameters.AddWithValue("@matricule", Appartement.matricule);
                cmd.Parameters.AddWithValue("@grade", Appartement.grade);
                cmd.Parameters.AddWithValue("@etat_locataire", Appartement.etat_locataire);
                cmd.Parameters.AddWithValue("@position", Appartement.position);
                cmd.Parameters.AddWithValue("@charge", Appartement.charge);
                cmd.Parameters.AddWithValue("@date_entree", Appartement.date_entree);
                cmd.Parameters.AddWithValue("@date_sortie", Appartement.date_sortie);
                cmd.Parameters.AddWithValue("@observation", Appartement.observation);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
                //list.Add(Locataire);
                //System.Windows.MessageBox.Show("Opération terminée avec Succès");

                /*************************/

                var cmd2 = conn.conn.CreateCommand();
                cmd2.Transaction = transaction;
                cmd2.CommandText = @"update Locataire set nom_complet = @nom_complet, 
                                    matricule = @matricule,
                                    grade = @grade,
                                    etat_locataire = @etat_locataire,
                                    position = @position where id=@id ";
                cmd2.Parameters.AddWithValue("@id", Appartement.id_locataire);
                cmd2.Parameters.AddWithValue("@nom_complet", Appartement.nom_complet);
                cmd2.Parameters.AddWithValue("@matricule", Appartement.matricule);
                cmd2.Parameters.AddWithValue("@grade", Appartement.grade);
                cmd2.Parameters.AddWithValue("@etat_locataire", Appartement.etat_locataire);
                cmd2.Parameters.AddWithValue("@position", Appartement.position);
                cmd2.Prepare();

                cmd2.ExecuteNonQuery();

                /*************************/
                transaction.Commit();


                IList<Locataire> locataires = Val.locataires.list.Where(ap => ap.id == Appartement.id_locataire).ToList();
                foreach (Locataire loc in locataires)
                {
                    loc.matricule = Appartement.matricule;
                    loc.nom_complet = Appartement.nom_complet;
                    loc.grade = Appartement.grade;
                    loc.position = Appartement.position;
                    loc.etat_locataire = Appartement.etat_locataire;
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

        public string editAppAndAddLoc(Appartement Appartement)
        {
            var conn = Val.data;


            try
            {
                conn.open();


            SQLiteTransaction transaction = Val.data.conn.BeginTransaction(IsolationLevel.ReadCommitted);

            var cmd2 = conn.conn.CreateCommand();
            cmd2.Transaction = transaction;

            Locataire locataire = new Locataire()
            {
                nom_complet = Appartement.nom_complet,
                matricule = Appartement.matricule,
                grade = Appartement.grade,
                position = Appartement.position,
                etat_locataire = Appartement.etat_locataire
            };
            cmd2.CommandText = @"insert into Locataire  values(
                                    null,
                                    @nom_complet,
                                    @matricule,
                                    @grade,
                                    @etat_locataire,
                                    @position)";
            cmd2.Parameters.AddWithValue("@id", Appartement.id_locataire);
            cmd2.Parameters.AddWithValue("@nom_complet", Appartement.nom_complet);
            cmd2.Parameters.AddWithValue("@matricule", Appartement.matricule);
            cmd2.Parameters.AddWithValue("@grade", Appartement.grade);
            cmd2.Parameters.AddWithValue("@etat_locataire", Appartement.etat_locataire);
            cmd2.Parameters.AddWithValue("@position", Appartement.position);
            cmd2.Prepare();

            cmd2.ExecuteNonQuery();
            locataire.id = conn.conn.LastInsertRowId;
            Appartement.id_locataire = locataire.id;
            if (Val.locataires != null)
                Val.locataires.list.Add(locataire);

            /*************************/

            var cmd = conn.cmd;
            cmd = conn.conn.CreateCommand();
            cmd.Transaction = transaction;
            cmd.CommandText = @"update Appartement set batiment = @batiment,
                                    num_porte = @num_porte,
                                    nbr_piece = @nbr_piece,
                                    id_locataire = @id_locataire,
                                    nom_complet = @nom_complet, 
                                    matricule = @matricule,
                                    grade = @grade,
                                    etat_locataire = @etat_locataire,
                                    position = @position,
                                    charge = @charge,
                                    date_entree=@date_entree,
                                    date_sortie=@date_sortie,
                                    observation = @observation where id=@id ";
            cmd.Parameters.AddWithValue("@id", Appartement.id);
            cmd.Parameters.AddWithValue("@batiment", Appartement.batiment);
            cmd.Parameters.AddWithValue("@num_porte", Appartement.num_porte);
            cmd.Parameters.AddWithValue("@nbr_piece", Appartement.nbr_piece);
            cmd.Parameters.AddWithValue("@id_locataire", Appartement.id_locataire);
            cmd.Parameters.AddWithValue("@nom_complet", Appartement.nom_complet);
            cmd.Parameters.AddWithValue("@matricule", Appartement.matricule);
            cmd.Parameters.AddWithValue("@grade", Appartement.grade);
            cmd.Parameters.AddWithValue("@etat_locataire", Appartement.etat_locataire);
            cmd.Parameters.AddWithValue("@position", Appartement.position);
            cmd.Parameters.AddWithValue("@charge", Appartement.charge);
            cmd.Parameters.AddWithValue("@date_entree", Appartement.date_entree);
            cmd.Parameters.AddWithValue("@date_sortie", Appartement.date_sortie);
            cmd.Parameters.AddWithValue("@observation", Appartement.observation);
            cmd.Prepare();

            cmd.ExecuteNonQuery();
            //list.Add(Locataire);
            //System.Windows.MessageBox.Show("Opération terminée avec Succès");




            /*************************/
            transaction.Commit();
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

        public void remove(Appartement Appartement)
        {
            try
            {

                var conn = Val.data;
                conn.open();
                var cmd = conn.cmd;
                cmd = conn.conn.CreateCommand();
                cmd.CommandText = @"delete from Appartement where id=@id";
                cmd.Parameters.AddWithValue("@id", Appartement.id);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
                list.Remove(Appartement);
                System.Windows.MessageBox.Show("Opération terminée avec Succès");
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
            }
        }


        public void vider(Appartement Appartement)
        {
            try
            {

                var conn = Val.data;
                conn.open();
                var cmd = conn.cmd;
                cmd = conn.conn.CreateCommand();
                cmd.CommandText = @"update Appartement set id_locataire=null, nom_complet='', matricule='', grade='', etat_locataire='', position='', date_entree = null, date_sortie=null where id=@id";
                cmd.Parameters.AddWithValue("@id", Appartement.id);
                cmd.Prepare();

                cmd.ExecuteNonQuery();


                Appartement.id_locataire = null;
                Appartement.nom_complet = "";
                Appartement.matricule = "";
                Appartement.grade = "";
                Appartement.position = "";
                Appartement.etat_locataire = "";
                Appartement.date_entree = null;
                Appartement.date_sortie = null;
                Appartement.date_sortie = null;

                //System.Windows.MessageBox.Show("Opération terminée avec Succès");
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
            }
        }


        public string archiver(Appartement Appartement)
        {
            var conn = Val.data;
            try
            {

                conn.open();

                SQLiteTransaction transaction = Val.data.conn.BeginTransaction(IsolationLevel.ReadCommitted);

                var cmd = conn.cmd;
                cmd = conn.conn.CreateCommand();
                cmd.Transaction = transaction;
                cmd.CommandText = @"update Appartement set id_locataire=null, nom_complet='', matricule='', grade='', etat_locataire='', position='', date_entree = null, date_sortie=null where id=@id";
                cmd.Parameters.AddWithValue("@id", Appartement.id);
                cmd.Prepare();

                cmd.ExecuteNonQuery();

                /*************************/

                var cmd2 = conn.conn.CreateCommand();
                cmd2.Transaction = transaction;
                cmd2.CommandText = @"insert into historique_appartement values(@id_locataire, @id_appartement,@date_entree,@date_sortie)";
                cmd2.Parameters.AddWithValue("@id_appartement", Appartement.id);
                cmd2.Parameters.AddWithValue("@id_locataire", Appartement.id_locataire);
                cmd2.Parameters.AddWithValue("@date_entree", Appartement.date_entree);
                cmd2.Parameters.AddWithValue("@date_sortie", Appartement.date_sortie);
                cmd2.Prepare();
                cmd2.ExecuteNonQuery();


                Appartement.id_locataire = null;
                Appartement.nom_complet = "";
                Appartement.matricule = "";
                Appartement.grade = "";
                Appartement.position = "";
                Appartement.etat_locataire = "";
                Appartement.date_entree = null;
                Appartement.date_sortie = null;

                /*************************/

                transaction.Commit();

                //list.Add(Appartement);
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

    }
}
