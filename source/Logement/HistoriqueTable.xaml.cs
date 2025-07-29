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
    public partial class HistoriqueTable : Window
    {
        public IList<Historique> list_hist_globale;
        public IList<Historique_Locataire_Join> list_datagrid, list_globale;
        Appartement appartement;
        AppartementEdit appartementEdit;

        Historique_Locataire_Join current_Historique;
        public HistoriqueTable(Appartement appartement)
        {
            InitializeComponent();

            Val.initHistorique();
            Val.initLocataire();
            Val.data = new DbContext();
            this.appartement = appartement;
            list_hist_globale = Val.historiques.list.Where(h => h.id_appartement == appartement.id).ToList();

            var result = from hist in list_hist_globale
                         join loc in Val.locataires.list on hist.id_locataire equals loc.id
                         select new Historique_Locataire_Join()
                         {
                             id = hist.id,
                             id_appartement = hist.id_appartement,
                             id_locataire = hist.id_locataire,
                             matricule = loc.matricule,
                             nom_complet = loc.nom_complet,
                             grade = loc.grade,
                             etat_locataire = loc.etat_locataire,
                             position = loc.position,
                             date_entree = hist.date_entree,
                             date_sortie = hist.date_sortie
                         };

            list_globale = result.ToList();
            list_datagrid = list_globale;

            //list_datagrid = from list_globale;
            datagrid.ItemsSource = list_datagrid;



            current_Historique = list_datagrid.FirstOrDefault();

            clearFields();

            title.Text = " Liste des Locataires \n";


            if (appartement.batiment == "VILLA")
                title.Text  += $"Villa {appartement.num_porte}";
            else
                title.Text += $" Batimenet {appartement.batiment} Porte {appartement.num_porte} (F{appartement.nbr_piece})\n";

        }


        private void edit_historique_click(object sender, RoutedEventArgs e)
        {


            editer_historique();

        }
        private void editer_historique()
        {
            if (current_Historique == null) return;



            form_mode.Content = "Editer";

            date_entree.DisplayDate = current_Historique.date_entree.Value;
            date_sortie.DisplayDate = current_Historique.date_sortie.Value;
            date_entree.Text = date_entree.DisplayDate.ToString();
            date_sortie.Text = date_sortie.DisplayDate.ToString();

            date_entree.Focus();
            //date_entree.Text = (current_Historique.date_entree == null)? current_Historique.date_entree.Value.ToString():"";
            //date_sortie.Text = (current_Historique.date_sortie == null)? current_Historique.date_sortie.Value.ToString():"";



        }
        private void addHandler(object sender, RoutedEventArgs e)
        {
            //new HistoriqueAdd().Show();

            form_mode.Content = "Ajouter";
            clearFields();
            date_entree.Focus();
        }

        public void clearFields()
        {
            //if (grade.Items.Count > 0)
            //    grade.SelectedIndex = 0;
            //if (etat_Historique.Items.Count > 0)
            //    etat_Historique.SelectedIndex = 0;
            //if (position.Items.Count > 0)
            //    position.SelectedIndex = 0;
            //matricule.Text = "";
            //nom_complet.Text = "";
        }

        private void listDataGridDbClick(Object sender, MouseButtonEventArgs e)
        {
            //Historique loc = (Historique)datagrid.SelectedItem;
            //if (loc != null && appartementEdit != null)
            //{
            //    appartementEdit.fillHistorique(loc);
            //    this.Close();
            //}
        }



        private void enter_key_down(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                var ue = e.OriginalSource as UIElement;
                var origin = sender as FrameworkElement;
                if (origin.Tag != null && origin.Tag.ToString() == "IgnoreEnterKeyTraversal")
                {
                    //ignore
                }
                else
                {
                    e.Handled = true;
                    ue.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                }
            }
        }
        private void delete_historique_click(object sender, RoutedEventArgs e)
        {
            if (current_Historique == null) return;
            if (MessageBox.Show("Voulez-vous supprimer " + current_Historique.nom_complet,
                "message", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Historique hist = list_hist_globale.Where(hs => hs.id == current_Historique.id).FirstOrDefault();
                if (hist == null) return;
                if (Val.historiques.remove(hist) == "")
                {
                    list_globale.Remove(current_Historique);
                    list_datagrid = list_globale;
                    datagrid.Items.Refresh();
                }


                datagrid.SelectedItem = list_datagrid.FirstOrDefault();
                refresh();
            }
        }



        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            current_Historique = (Historique_Locataire_Join)datagrid.SelectedItem;
        }

        /* recherche et filtre */
        private void recherche(object sender, TextChangedEventArgs e)
        {
            filterAll();
        }
        private void recherche()
        {
            string str = filter.Text;
            if (str.Length < 3)
            {
                return;
            }

            //if (str == "") return;

            list_datagrid = list_datagrid.Where(f => f.nom_complet.Contains(str)
            || f.matricule == str
            ).ToList();
            //).Take(Val.limit).ToList();
        }

        private void filterAll()
        {

            //list_globale = Val.historiques.list;
            list_datagrid = list_globale;

            recherche();
            datagrid.ItemsSource = list_datagrid;
            datagrid.Items.Refresh();
        }


        public void refresh()
        {

            //list_globale = Val.historiques.list;
            filterAll();

            datagrid.SelectedItem = Val.apparetements.list.FirstOrDefault();

        }


        private void validerEvent(object sender, RoutedEventArgs e)
        {

            string message;

            if (form_mode.Content.ToString() == "Ajouter")
                current_Historique = new Historique_Locataire_Join();
            else if (current_Historique == null)
            {
                form_mode.Content = "Editer";
                MessageBox.Show("Veuillez selectinner une ligne");
                clearFields();
                return;
            }

            datagrid.SelectionChanged -= DataGrid_SelectionChanged;

            current_Historique.date_entree = Function.ConvertDateTime(date_entree.Text);
            current_Historique.date_sortie = Function.ConvertDateTime(date_sortie.Text);


            if (form_mode.Content.ToString() == "Editer")
            {

                Historique hist = list_hist_globale.Where(hs => hs.id == current_Historique.id).FirstOrDefault();
                hist.date_entree = current_Historique.date_entree;
                hist.date_sortie = current_Historique.date_sortie;
                message = Val.historiques.edit(hist);
            }
            else
            {
                Historique hist = new Historique()
                {
                    id_appartement = current_Historique.id_appartement,
                    id_locataire = current_Historique.id_locataire,
                    date_entree = current_Historique.date_entree,
                    date_sortie = current_Historique.date_sortie
                };
                message = Val.historiques.add(hist);
                if (message == "")
                    refresh();
                else
                    MessageBox.Show(message);
            }
            if (message == "")
            {

                datagrid.Items.Refresh();
                //datagrid.ItemsSource = Val.historiques.list;
                //datagrid.Items.Refresh();

                //Val.main.refresh();
                clearFields();
                form_mode.Content = "Editer";
                date_entree.Focus();
            }
            else
                MessageBox.Show(message);
            datagrid.SelectionChanged += DataGrid_SelectionChanged;
        }

        private void refresh(object sender, RoutedEventArgs e)
        {
            filterAll();
        }

        private void checkInt(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Function.isInt(e.Text);
        }

        private void checkDouble(object sender, TextCompositionEventArgs e)
        {
            string str = ((TextBox)sender).Text;
            string separator = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            //bool vcond = (e.Text == "," || e.Text == ".") && (!str.Contains(",") && !str.Contains("."));
            bool vcond = e.Text == separator && !str.Contains(separator);
            //bool vcond = e.Text == "," && !str.Contains(",");
            e.Handled = !(Function.isInt(e.Text) || vcond);
        }

        private void combo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            ComboBox tb = sender as ComboBox;
            foreach (char ch in e.Text)
                if (Char.IsLower(ch))
                {
                    tb.Text += Char.ToUpper(ch);
                    e.Handled = true;
                }
        }

    }
}
