using Microsoft.Reporting.WinForms;
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
using System.Windows.Shapes;

namespace Logement
{
    /// <summary>
    /// Logique d'interaction pour TablePrint.xaml
    /// </summary>
    public partial class TablePrint : Window
    {
        public string reportName;
        public IList<Appartement> items;
        public TablePrint(IList<Appartement> items)
        {
            InitializeComponent();

            this.items = items;
            this.reportName = "Table";

            fillReport();
        }

        private void refreshReport(object sender, RoutedEventArgs e)
        {
            refreshReport();
        }
        private void refreshReport()
        {
            fillReport();
        }
        public void fillReport()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            //System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));

            IEnumerable<ReportParameter> paramaitres = new List<ReportParameter>() {
            new ReportParameter("titre", titre.Text),
            };
            _reportviewer.LocalReport.ReportPath = path + "\\reports\\" + this.reportName + ".rdlc";
            _reportviewer.LocalReport.SetParameters(paramaitres);
            var rpds_model = new ReportDataSource() { Name = "DataSet1", Value = items };

            _reportviewer.LocalReport.SetParameters(paramaitres);
            _reportviewer.LocalReport.DataSources.Add(rpds_model);
            _reportviewer.LocalReport.EnableExternalImages = true;
            _reportviewer.SetDisplayMode(DisplayMode.PrintLayout);
            _reportviewer.Refresh();
            _reportviewer.RefreshReport();
        }

    }
}
