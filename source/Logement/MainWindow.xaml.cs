using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Logement
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IList<Appartement> list_globale, list_datagrid;
        public MainWindow()
        {
            InitializeComponent();

            Val.data = new DbContext();


            //Val.apparetements = new ApparetementVal();
            Val.initApparetements();
            Val.main = this;
            list_globale = Val.apparetements.list;
            list_datagrid = list_globale.ToList();
            //list_datagrid = list_globale.Take(Val.limit).ToList();

            datagrid.ItemsSource = list_datagrid;


            datagrid.SelectedItem = Val.apparetements.list.FirstOrDefault();
            //setappartementInfo();
            //datagrid.Items.Refresh();


            refreshTotal();

            //Val.initBatiments();

            //foreach (Appartement ap in Val.batiments.list.Where(bt => bt.numero == "25").First().appartements)
            //    MessageBox.Show(ap.nom_complet);

        }


        private void addHandler(object sender, RoutedEventArgs e)
        {
            //new appartementAdd().Show();
        }


        //private void listDataGridDbClick(Object sender, MouseButtonEventArgs e)
        //{
        //    edit_fichier();
        //}



        private void delete_appartement_click(object sender, RoutedEventArgs e)
        {
            Appartement appartement = (Appartement)datagrid.SelectedItem;
            if (appartement == null) return;
            if (MessageBox.Show("Voulez-vous supprimer la appartement ",
                "message", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Val.apparetements.remove(appartement);

                datagrid.SelectedItem = Val.apparetements.list.FirstOrDefault();
                //setappartementInfo();
                refresh();
            }
        }




        /* recherche et filtre */
        private void recherche(object sender, TextChangedEventArgs e)
        {
            filterAll();
        }
        private void recherche()
        {
            string str = filter.Text;
            //if (str.Length < 3)
            //{
            //    return;
            //}

            if (str == "") return;

            list_datagrid = list_datagrid.Where(f => f.nom_complet.Contains(str)
            || f.matricule == str
            //|| f.matricule.StartsWith(str)
            || f.batiment == str
            ).ToList();
            //).Take(Val.limit).ToList();

            //datagrid.ItemsSource = list_datagrid;
            //datagrid.Items.Refresh();
        }
        private void filterChoice()
        {
            IList<string> grade_selection = new List<string>() {
                gradecb1.Content.ToString(),
                gradecb2.Content.ToString(),
                gradecb3.Content.ToString()
            };
            int grade_selection_count = grade_selection.Count;

            IList<string> etat_locataire_selection = new List<string>() {
                etat_locatairecb1.Content.ToString(),
                etat_locatairecb2.Content.ToString(),
                etat_locatairecb3.Content.ToString(),
                etat_locatairecb4.Content.ToString(),
                etat_locatairecb5.Content.ToString()
            };
            int etat_locataire_selection_count = etat_locataire_selection.Count;

            // traitement different de type villa
            IList<int> type_logement_selection = new List<int>() {
                1,
                2,
                3,
                4,
                5
            };



            if (gradecb1.IsChecked == false) grade_selection.Remove(gradecb1.Content.ToString());
            if (gradecb2.IsChecked == false) grade_selection.Remove(gradecb2.Content.ToString());
            if (gradecb3.IsChecked == false) grade_selection.Remove(gradecb3.Content.ToString());



            if (gradecbTous.IsChecked != null && grade_selection.Count != 0)
                list_datagrid = list_datagrid.Where(f => grade_selection.Contains(f.grade)).ToList();


            /***************************/


            if (etat_locatairecb1.IsChecked == false) etat_locataire_selection.Remove(etat_locatairecb1.Content.ToString());
            if (etat_locatairecb2.IsChecked == false) etat_locataire_selection.Remove(etat_locatairecb2.Content.ToString());
            if (etat_locatairecb3.IsChecked == false) etat_locataire_selection.Remove(etat_locatairecb3.Content.ToString());
            if (etat_locatairecb4.IsChecked == false) etat_locataire_selection.Remove(etat_locatairecb4.Content.ToString());
            if (etat_locatairecb5.IsChecked == false) etat_locataire_selection.Remove(etat_locatairecb5.Content.ToString());



            if (etat_locatairecbTous.IsChecked != null && etat_locataire_selection.Count != 0)
                list_datagrid = list_datagrid.Where(f => etat_locataire_selection.Contains(f.etat_locataire)).ToList();


            /***************************/

            if (type_logementcb1.IsChecked == false) type_logement_selection.Remove(1);
            if (type_logementcb2.IsChecked == false) type_logement_selection.Remove(2);
            if (type_logementcb3.IsChecked == false) type_logement_selection.Remove(3);
            if (type_logementcb4.IsChecked == false) type_logement_selection.Remove(4);
            if (type_logementcb5.IsChecked == false) type_logement_selection.Remove(5);

            // pour la condition soit villa est selectionner soit l'un des type d'apparetement 
            if (type_logementcbTous.IsChecked != null)
            {
                if (type_logement_selection.Count != 0 && type_logementcb6.IsChecked == true)
                    list_datagrid = list_datagrid.Where(f => f.batiment == "VILLA" || type_logement_selection.Contains(f.nbr_piece)).ToList();

                else
                {

                    if (type_logementcb6.IsChecked == true) // l'autre condition = false
                        list_datagrid = list_datagrid.Where(f => f.batiment == "VILLA").ToList();
                    else if (type_logement_selection.Count != 0) // villa = false
                        list_datagrid = list_datagrid.Where(f => f.batiment != "VILLA" && type_logement_selection.Contains(f.nbr_piece)).ToList();
                }
            }


        }
        private void filterAll()
        {

            list_globale = Val.apparetements.list;
            list_datagrid = list_globale;

            filterChoice();
            recherche();
            datagrid.ItemsSource = list_datagrid;
            datagrid.Items.Refresh();

            refreshTotal();
        }


        public void refresh()
        {

            //list_globale = Val.apparetements.list;
            filterAll();

            datagrid.SelectedItem = Val.apparetements.list.FirstOrDefault();
            //setappartementInfo();
        }


        private void refresh(object sender, RoutedEventArgs e)
        {
            filterAll();
        }

        private void datagridSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //setappartementInfo();
        }


        //private void gradeTous_Click(object sender, RoutedEventArgs e)
        //{
        //    grade_filtre.UnselectAll();
        //}

        private void imprimer_appartement_click(object sender, RoutedEventArgs e)
        {
            //Appartement appartement = (Appartement)datagrid.SelectedItem;
            //if (appartement != null)
            //    new appartementPrint(appartement).Show();
        }


        private void gradecb_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;

            if (cb.Content.ToString() != "Tous")
                gradecbTous.IsChecked = false;
            else
            if (cb.IsChecked == true) // Tous
            {
                gradecb1.IsChecked = false;
                gradecb2.IsChecked = false;
                gradecb3.IsChecked = false;
            }
            else
            {
                gradecb1.IsChecked = cb.IsChecked;
                gradecb2.IsChecked = cb.IsChecked;
                gradecb3.IsChecked = cb.IsChecked;
            }
        }

        private void etat_locatairecb_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;

            if (cb.Content.ToString() != "Tous")
                etat_locatairecbTous.IsChecked = false;
            else
            if (cb.IsChecked == true) // Tous
            {
                etat_locatairecb1.IsChecked = false;
                etat_locatairecb2.IsChecked = false;
                etat_locatairecb3.IsChecked = false;
                etat_locatairecb4.IsChecked = false;
                etat_locatairecb5.IsChecked = false;
            }

            //else
            //{
            //    etat_locatairecb1.IsChecked = cb.IsChecked;
            //    etat_locatairecb2.IsChecked = cb.IsChecked;
            //    etat_locatairecb3.IsChecked = cb.IsChecked;
            //    etat_locatairecb4.IsChecked = cb.IsChecked;
            //    etat_locatairecb5.IsChecked = cb.IsChecked;
            //}
        }

        private void type_logementcb_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;

            if (cb.Content.ToString() != "Tous")
                type_logementcbTous.IsChecked = false;
            else
            if (cb.IsChecked == true) // Tous
            {
                type_logementcb1.IsChecked = false;
                type_logementcb2.IsChecked = false;
                type_logementcb3.IsChecked = false;
                type_logementcb4.IsChecked = false;
                type_logementcb5.IsChecked = false;
                type_logementcb6.IsChecked = false;
            }
        }

        private void imprimer_table_click(object sender, RoutedEventArgs e)
        {
            new TablePrint(list_datagrid).Show();
        }

        public void refreshTotal()
        {
            Total.Text = "Total : " + list_datagrid.Count().ToString();
        }

        private void vider_appartement_click(object sender, RoutedEventArgs e)
        {

            Appartement app = (Appartement)datagrid.SelectedItem;
            if (app == null) return;

            if (app.id_locataire == null)
            {
                MessageBox.Show("apparetement déja vide.");
                return;
            }
            if (MessageBox.Show("Voulez-vous Archiver le locataire actuel ?",
                        "message", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                new AppartementEdit(app, "vider").Show();
            else
            {
                Val.apparetements.vider(app);
                refresh();
            }


        }

        private void ajouter_locataire_click(object sender, RoutedEventArgs e)
        {

            Appartement app = (Appartement)datagrid.SelectedItem;
            if (app == null) return;
            if (app.id_locataire != null)
            {
                MessageBox.Show("Veuillez vider l'apparetement.");
                return;
            }
            new AppartementEdit(app, "add_locataire").Show();

        }

        private void locatairesHandler(object sender, RoutedEventArgs e)
        {
            new LocataireTable().Show();
        }

        //private void edit_locataire_click(object sender, RoutedEventArgs e)
        //{

        //    Appartement app = (Appartement)datagrid.SelectedItem;
        //    if (app == null) return;
        //    new AppartementEdit(app, "locataire").Show();
        //}

        private void historique_appartement_click(object sender, RoutedEventArgs e)
        {
            Appartement app = (Appartement)datagrid.SelectedItem;
            if (app == null) return;
            //Val.initBatiments();
            new HistoriqueTable(app).Show();
        }
        private void edit_appartement_click(object sender, RoutedEventArgs e)
        {
            Appartement app = (Appartement)datagrid.SelectedItem;
            if (app == null) return;
            //Val.initBatiments();
            new AppartementEdit(app,"edit").Show();
        }
        private void export_excel_item_click(object sender, RoutedEventArgs e)
        {
            //ExcelExport.exportInvest(investissement_Dg);
            ExcelExport.exportDataGrid(datagrid);
        }
    }
}
