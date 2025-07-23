using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Logement
{
    class ExcelExport
    {


        public static void exportDataGrid(DataGrid datagrid)
        {


            Microsoft.Office.Interop.Excel.Application APP = null;
            Microsoft.Office.Interop.Excel.Workbook WB = null;
            Microsoft.Office.Interop.Excel.Worksheet WS = null;

            //try
            //{

                APP = new Microsoft.Office.Interop.Excel.Application();
                WB = APP.Workbooks.Add(1);
                WS = (Microsoft.Office.Interop.Excel.Worksheet)WB.Sheets[1];

                int ind = 0;
                foreach (object ob in datagrid.Columns.Select(cs => cs.Header).ToList())
                {
                    if (ob.ToString().ToLower() != "id")
                        WS.Cells[1, ind] = ob.ToString();
                    ind++;
                }


                //for (int i = 0; i < 12; i++)
                for (int i = 0; i < datagrid.Items.Count; i++)
                {

                    for (int j = 1; j < datagrid.Columns.Count; j++)
                    {
                        WS.Cells[i + 2, j] = DataGridFunct.getCellStr(datagrid, i, j);
                    }
                }
                string savepath = "";
                SaveFileDialog filedialog = new SaveFileDialog();
                //filedialog.FileName = "doc";
                filedialog.FileName = "Liste des appartements " + DateTime.Now.ToString().Replace(":","_").Replace("/","-");
                filedialog.DefaultExt = "xls";
                filedialog.Filter = "EXCEL file(*.xls)|*.xls";
                if (filedialog.ShowDialog() == true)
                    savepath = filedialog.FileName;
                else return;

                if (APP.ActiveWorkbook != null)
                {
                    //APP.ActiveWorkbook.Save();
                    APP.ActiveWorkbook.SaveAs(savepath, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }
                if (APP != null)
                {
                    if (WB != null)
                    {
                        if (WS != null)
                            Marshal.ReleaseComObject(WS);

                        WB.Close(false, Type.Missing, Type.Missing);

                        Marshal.ReleaseComObject(WB);

                    }

                    APP.Quit();

                    Marshal.ReleaseComObject(APP);
                }

                System.Windows.MessageBox.Show("Exportation terminee");
            //}
            //catch (Exception e)
            //{
            //    // FREE RESOURCE
            //    if (APP != null)
            //    {
            //        if (WB != null)
            //        {
            //            if (WS != null)
            //                Marshal.ReleaseComObject(WS);

            //            WB.Close(false, Type.Missing, Type.Missing);

            //            Marshal.ReleaseComObject(WB);

            //        }

            //        APP.Quit();

            //        Marshal.ReleaseComObject(APP);
            //    }


            //    System.Windows.MessageBox.Show("exception : " + e.Message);
            //}
        }


        /*********************/

        //public static void exportInvest(IList<InvestissementItem> list)
        //{


        //    Microsoft.Office.Interop.Excel.Application APP = null;
        //    Microsoft.Office.Interop.Excel.Workbook WB = null;
        //    Microsoft.Office.Interop.Excel.Worksheet WS = null;

        //    try
        //    {

        //        APP = new Microsoft.Office.Interop.Excel.Application();
        //        WB = APP.Workbooks.Add(1);
        //        WS = (Microsoft.Office.Interop.Excel.Worksheet)WB.Sheets[1];

        //        int ind = 2;

        //        // set header 
        //        WS.Cells[1, 1] = "EMPLACEMENT";
        //        WS.Cells[1, 2] = "ENREG";
        //        WS.Cells[1, 3] = "LC";
        //        WS.Cells[1, 4] = "DESIGNATION";
        //        WS.Cells[1, 5] = "REFERENCE";
        //        WS.Cells[1, 6] = "QUANTITE";
        //        WS.Cells[1, 7] = "S_EMPL";
        //        WS.Cells[1, 8] = "ORIGINE";
        //        WS.Cells[1, 9] = "DATE_ACHAT";
        //        WS.Cells[1, 10] = "ETAT";
        //        WS.Cells[1, 11] = "TYPE";
        //        WS.Cells[1, 12] = "VAL_REEL";
        //        WS.Cells[1, 13] = "VAL_CUM";
        //        WS.Cells[1, 14] = "PRIX";
        //        WS.Cells[1, 15] = "DOTEC";
        //        WS.Cells[1, 16] = "CUMUL";
        //        WS.Cells[1, 17] = "VALEUR";
        //        WS.Cells[1, 18] = "COMPTE";

        //        foreach (InvestissementItem item in list)
        //        {
        //            WS.Cells[ind, 1] = item.EMPLACEMENT;
        //            WS.Cells[ind, 2] = item.ENREG;
        //            WS.Cells[ind, 3] = item.LC;
        //            WS.Cells[ind, 4] = item.DESIGNATION;
        //            WS.Cells[ind, 5] = item.REFERENCE;
        //            WS.Cells[ind, 6] = item.QUANTITE;
        //            WS.Cells[ind, 7] = item.S_EMPL;
        //            WS.Cells[ind, 8] = item.ORIGINE;
        //            WS.Cells[ind, 9] = item.DATE_ACHAT;
        //            WS.Cells[ind, 10] = item.ETAT;
        //            WS.Cells[ind, 11] = item.TYPE;
        //            WS.Cells[ind, 12] = item.VAL_REEL;
        //            WS.Cells[ind, 13] = item.VAL_CUM;
        //            WS.Cells[ind, 14] = item.PRIX;
        //            WS.Cells[ind, 15] = item.DOTEC;
        //            WS.Cells[ind, 16] = item.CUMUL;
        //            WS.Cells[ind, 17] = item.VALEUR;
        //            WS.Cells[ind, 18] = item.COMPTE;
        //            ind++;
        //        }

        //        // save path
        //        string savepath = "";
        //        SaveFileDialog filedialog = new SaveFileDialog();
        //        filedialog.FileName = "INVESTISSEMENT " + DateTime.Now.ToString().Replace(":", "_").Replace("/", "-");
        //        filedialog.DefaultExt = "xls";
        //        filedialog.Filter = "EXCEL file(*.xls)|*.xls";
        //        if (filedialog.ShowDialog() == true)
        //            savepath = filedialog.FileName;
        //        else return;

        //        if (APP.ActiveWorkbook != null)
        //        {
        //            //APP.ActiveWorkbook.Save();
        //            APP.ActiveWorkbook.SaveAs(savepath, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        //        }
        //        if (APP != null)
        //        {
        //            if (WB != null)
        //            {
        //                if (WS != null)
        //                    Marshal.ReleaseComObject(WS);

        //                WB.Close(false, Type.Missing, Type.Missing);

        //                Marshal.ReleaseComObject(WB);

        //            }

        //            APP.Quit();

        //            Marshal.ReleaseComObject(APP);


        //        }
        //        System.Windows.MessageBox.Show("Exportation terminee");
        //    }
        //    catch (Exception e)
        //    {
        //        // FREE RESOURCE
        //        if (APP != null)
        //        {
        //            if (WB != null)
        //            {
        //                if (WS != null)
        //                    Marshal.ReleaseComObject(WS);

        //                WB.Close(false, Type.Missing, Type.Missing);

        //                Marshal.ReleaseComObject(WB);

        //            }

        //            APP.Quit();

        //            Marshal.ReleaseComObject(APP);
        //        }


        //        System.Windows.MessageBox.Show("exception : " + e.Message);
        //    }
        //}





    }
}