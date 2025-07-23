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
    public partial class LocataireTable : Window
    {
        public IList<Locataire> list_globale, list_datagrid;
        AppartementEdit appartementEdit;

        Locataire current_locataire;
        public LocataireTable(AppartementEdit appartementEdit = null)
        {
            InitializeComponent();

            Val.data = new DbContext();
            this.appartementEdit = appartementEdit;
            Val.initLocataire();
            list_globale = Val.locataires.list;
            //list_datagrid = list_globale.ToList();
            list_datagrid = list_globale.Take(Val.limit).ToList();

            datagrid.ItemsSource = list_datagrid;


            datagrid.SelectedItem = Val.apparetements.list.FirstOrDefault();


            mainGrid.ColumnDefinitions.ElementAt(0).Width = (appartementEdit == null) ? new GridLength(350, GridUnitType.Pixel) : new GridLength(0, GridUnitType.Pixel);

            current_locataire = list_datagrid.FirstOrDefault();

            clearFields();

        }


        private void edit_locataire_click(object sender, RoutedEventArgs e)
        {


            editer_locataire();

        }
        private void editer_locataire()
        {
            if (current_locataire == null) return;



            locataire_form_mode.Content = "Editer";

            matricule.Text = current_locataire.matricule;
            nom_complet.Text = current_locataire.nom_complet;
            grade.Text = current_locataire.grade;
            position.Text = current_locataire.position;
            etat_locataire.Text = current_locataire.etat_locataire;

        }
        private void addHandler(object sender, RoutedEventArgs e)
        {
            //new locataireAdd().Show();

            locataire_form_mode.Content = "Ajouter";
            clearFields();
            matricule.Focus();
        }

        public void clearFields()
        {
            if (grade.Items.Count > 0)
                grade.SelectedIndex = 0;
            if (etat_locataire.Items.Count > 0)
                etat_locataire.SelectedIndex = 0;
            if (position.Items.Count > 0)
                position.SelectedIndex = 0;
            matricule.Text = "";
            nom_complet.Text = "";
        }

        private void listDataGridDbClick(Object sender, MouseButtonEventArgs e)
        {
            Locataire loc = (Locataire)datagrid.SelectedItem;
            if (loc != null && appartementEdit != null)
            {
                appartementEdit.fillLocataire(loc);
                this.Close();
            }
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
        private void delete_locataire_click(object sender, RoutedEventArgs e)
        {
            if (current_locataire == null) return;
            if (MessageBox.Show("Voulez-vous supprimer la locataire " + current_locataire.nom_complet,
                "message", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Val.locataires.remove(current_locataire);

                datagrid.SelectedItem = list_datagrid.FirstOrDefault();
                //setlocataireInfo();
                refresh();
            }
        }



        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            current_locataire = (Locataire)datagrid.SelectedItem;
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
            ).Take(Val.limit).ToList();
            //).ToList();
        }

        private void filterAll()
        {

            list_globale = Val.locataires.list;
            list_datagrid = list_globale;

            recherche();
            datagrid.ItemsSource = list_datagrid;
            datagrid.Items.Refresh();
        }


        public void refresh()
        {

            //list_globale = Val.locataires.list;
            filterAll();

            datagrid.SelectedItem = Val.apparetements.list.FirstOrDefault();

        }


        private void validerEvent(object sender, RoutedEventArgs e)
        {

            string message;

            //message = Function.emtyFields(FORM, new String[] { "num_ordre" });
            //if (!Function.isDateTime(date.Text))
            //    message += "- date invalide \n";
            //if (message != "")
            //{
            //    matricule.Focus();
            //    MessageBox.Show(message);
            //    return;
            //}

            if (locataire_form_mode.Content.ToString() == "Ajouter")
                current_locataire = new Locataire();
            else if (current_locataire == null)
            {
                locataire_form_mode.Content = "Ajouter";
                MessageBox.Show("Veuillez selectinner un locataire");
                clearFields();
                return;
            }

            datagrid.SelectionChanged -= DataGrid_SelectionChanged;

            current_locataire.matricule = matricule.Text;
            current_locataire.nom_complet = nom_complet.Text;
            current_locataire.grade = grade.Text;
            current_locataire.position = position.Text;
            current_locataire.etat_locataire = etat_locataire.Text;

            if (locataire_form_mode.Content.ToString() == "Ajouter")
            {
                message = Val.locataires.add(current_locataire);
                if (message == "")
                    refresh();
                else
                    MessageBox.Show(message);
            }
            else
                message = Val.locataires.edit(current_locataire);
            if (message == "")
            {
                datagrid.ItemsSource = Val.locataires.list;
                datagrid.Items.Refresh();
                
                Val.main.refresh();
                clearFields();
                locataire_form_mode.Content = "Ajouter";
                matricule.Focus();
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
            //bool vcond = (e.Text == "," || e.Text == ".") && (!str.Contains(",") && !str.Contains("."));
            bool vcond = e.Text == "," && !str.Contains(",");
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
