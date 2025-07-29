using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Logique d'interaction pour AppartementEdit.xaml
    /// </summary>
    public partial class AppartementEdit : Window
    {
        Appartement appartement;
        public string filePath;
        public string ext;
        Locataire locataire;
        string mode;
        public AppartementEdit(Appartement appartement, string mode = "")
        {
            InitializeComponent();

            //ext = Appartement.photo_ext;
            ext = "";
            filePath = "";
            //setImage(Appartement.num_passport + "_" + Appartement.id.ToString() + "." + Appartement.photo_ext);

            matricule.Focus();
            this.appartement = appartement;
            this.mode = mode;
            fillForm();
            if (appartement.batiment == "VILLA")
                appInfo.Text = $"Villa {appartement.num_porte}";
            else
                appInfo.Text = $" Batimenet {appartement.batiment} Porte {appartement.num_porte} (F{appartement.nbr_piece})\n";

            if (mode != "locataire" && mode != "edit" && appartement.id_locataire != null)
                appInfo.Text += $" Nom de Locatiare : {appartement.nom_complet} \n Matricule : {appartement.matricule} \n Grade : {appartement.grade} \n Etat : {appartement.etat_locataire}";
            if (mode == "edit" && appartement.id_locataire == null)
                appInfo.Text += " Appartement Vide";
            editMode();
        }
        public void editMode()
        {

            if (mode == "vider")
            {
                //locInfo.Visibility = Visibility.Visible;

                browseLocataire.Visibility = Visibility.Collapsed;
                nom_complet.Visibility = Visibility.Collapsed;
                matricule.Visibility = Visibility.Collapsed;
                grade.Visibility = Visibility.Collapsed;
                etat_locataire.Visibility = Visibility.Collapsed;
                charge.Visibility = Visibility.Collapsed;
                position.Visibility = Visibility.Collapsed;
                date_entree.Visibility = Visibility.Collapsed;
                observation.Visibility = Visibility.Collapsed;
                this.Height = 350;

                date_sortie.Focus();
            }
            else if (mode == "edit")
            {
                if (appartement.id_locataire == null)
                {
                    browseLocataire.Visibility = Visibility.Collapsed;
                    nom_complet.Visibility = Visibility.Collapsed;
                    matricule.Visibility = Visibility.Collapsed;
                    grade.Visibility = Visibility.Collapsed;
                    etat_locataire.Visibility = Visibility.Collapsed;
                    charge.Visibility = Visibility.Collapsed;
                    position.Visibility = Visibility.Collapsed;
                    date_entree.Visibility = Visibility.Collapsed;
                    date_sortie.Visibility = Visibility.Collapsed;
                    //observation.Visibility = Visibility.Collapsed;
                    this.Height = 350;
                    observation.Focus();
                }
                else
                {

                    browseLocataire.Visibility = Visibility.Collapsed;
                    date_sortie.Visibility = Visibility.Collapsed;
                    this.Height = 720;
                    matricule.Focus();
                }
            }
            else if (mode == "add_locataire")
            {
                //locInfo.Visibility = Visibility.Collapsed;
                charge.Visibility = Visibility.Collapsed;
                //date_entree.Visibility = Visibility.Collapsed;
                date_sortie.Visibility = Visibility.Collapsed;
                observation.Visibility = Visibility.Collapsed;
                this.Height = 580;
                matricule.Focus();
            }
        }
        public void fillForm()
        {
            batiment.Text = appartement.batiment;
            num_porte.Text = appartement.num_porte.ToString();
            nbr_piece.Text = appartement.nbr_piece.ToString();
            if (appartement.id_locataire != null)
            {

                nom_complet.Text = appartement.nom_complet;
                matricule.Text = appartement.matricule;
                grade.Text = appartement.grade;
                etat_locataire.Text = appartement.etat_locataire;
                position.Text = appartement.position;

                locataire = new Locataire()
                {
                    nom_complet = appartement.nom_complet,
                    matricule = appartement.matricule,
                    grade = appartement.grade,
                    etat_locataire = appartement.etat_locataire,
                    position = appartement.position
                };
            }
            charge.Text = appartement.charge.ToString();
            date_entree.Text = appartement.date_entree.ToString();
            date_sortie.Text = appartement.date_sortie.ToString();
            observation.Text = appartement.observation.ToString();
        }
        public void fillLocataire(Locataire loc)
        {
            if (loc != null)
            {

                appartement.id_locataire = loc.id;
                nom_complet.Text = loc.nom_complet;
                matricule.Text = loc.matricule;
                grade.Text = loc.grade;
                etat_locataire.Text = loc.etat_locataire;
                position.Text = loc.position;

                locataire = loc;
            }
        }
        private void validerEvent(object sender, RoutedEventArgs e)
        {
            string message = "";
            if (mode == "vider")
            {
                appartement.date_sortie = Function.ConvertDateTime(date_sortie.Text);
                message = Val.apparetements.archiver(appartement);
            }
            else if (mode == "edit")
            {

                appartement.charge = Function.ConvertDouble(charge.Text);
                appartement.date_entree = Function.ConvertDateTime(date_entree.Text);
                appartement.date_sortie = Function.ConvertDateTime(date_sortie.Text);
                appartement.observation = observation.Text;


                if (appartement.nom_complet != nom_complet.Text ||
                    appartement.matricule != matricule.Text ||
                    appartement.grade != grade.Text ||
                    appartement.position != position.Text ||
                    appartement.etat_locataire != etat_locataire.Text)
                {
                    appartement.nom_complet = nom_complet.Text;
                    appartement.matricule = matricule.Text;
                    appartement.position = position.Text;
                    appartement.grade = grade.Text;
                    appartement.etat_locataire = etat_locataire.Text;

                    message = Val.apparetements.editAppLoc(appartement);
                }
                else
                    message = Val.apparetements.edit(appartement);

            }
            else if (mode == "add_locataire")
            {

                //appartement.charge = Function.ConvertDouble(charge.Text);
                appartement.matricule = matricule.Text;
                appartement.nom_complet = nom_complet.Text;
                appartement.grade = grade.Text;
                appartement.position = position.Text;
                appartement.etat_locataire = etat_locataire.Text;

                appartement.date_entree = Function.ConvertDateTime(date_entree.Text);
                appartement.observation = observation.Text;

                if (locataire == null)
                {
                    message = Val.apparetements.editAppAndAddLoc(appartement);
                }
                else
                {
                    message = Val.apparetements.edit(appartement);
                }
            }
            if (message == "")
            {
                System.Windows.MessageBox.Show("Opération terminée avec Succès");
                Val.main.refresh();
            }
            else MessageBox.Show(message);
        }
        public void resetField()
        {

            //nom_complet.Text = "";
            //num_passport.Text = "";
            //objet.Text = "";
            //nationalite.Text = "";
            //observation.Text = "";
            //contrat.Text = "";
            //entreprise.Text = "";

        }
        private void enter_key_down(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (sender is TextBox && ((TextBox)sender).Name == "matricule")
                {
                    grade.Focus();
                    return;
                }
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

        private void openLocataire(object sender, RoutedEventArgs e)
        {
            new LocataireTable(this).Show();
        }
    }
}
