using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System;
using System.Data;
using System.IO;
using System.Windows.Controls;
using Word = Microsoft.Office.Interop.Word;

namespace Journal_Release
{
    class ImportExport
    {
        private static Word.Application word = new Word.Application();
        public static void ExportToPdf()
        {
            PdfPTable table = new PdfPTable(7);//Setting count columns in PDF file
            SaveFileDialog save = new SaveFileDialog();
            save.ShowDialog();

            
            //Add table headers
            table.AddCell(new Phrase("ID"));
            table.AddCell(new Phrase("PC name"));
            table.AddCell(new Phrase("Date"));
            table.AddCell(new Phrase("Responsible"));
            table.AddCell(new Phrase("Local Adm pass"));
            table.AddCell(new Phrase("Bios pass"));
            table.AddCell(new Phrase("Model"));

            foreach(Comps comp in Comps.compsList)
            {
                table.AddCell(new Phrase(comp.Id));
                table.AddCell(new Phrase(comp.PcName));
                table.AddCell(new Phrase(comp.Date));
                table.AddCell(new Phrase(comp.Responsibility));
                table.AddCell(new Phrase(comp.AdmPass));
                table.AddCell(new Phrase(comp.BiosPass));
                table.AddCell(new Phrase(comp.Model));
            }

            //foreach (string[] s in data)
            //{
            //    for (int i = 0; i < s.Length; i++)
            //    {
            //        table.AddCell(new Phrase(s[i].ToString()));
            //    }
            //}
            string folderPath = save.FileName;
            using (FileStream fileStream = new FileStream(folderPath + @".PDF", FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.A4, 5f, 5f, 5f, 0f);
                PdfWriter.GetInstance(pdfDoc, fileStream);
                pdfDoc.Open();
                pdfDoc.Add(table);
                pdfDoc.Close();
                fileStream.Close();
            }
        }

        public static void ImportWord(string path)
        {

            Word.Document doc = openDoc(path);
            Object begin = Type.Missing;
            Object end = Type.Missing;
            Word.Range wordrange = doc.Range(ref begin, ref end);
            DataTable data = new DataTable();
            Word.Table table = doc.Tables[1];


            for (int i = 0; i < table.Columns.Count; i++) data.Columns.Add();
            for (int i = 1; i <= table.Rows.Count; i++)
            {
                string[] cells = new string[table.Columns.Count];
                for (int j = 1; j <= table.Rows[i].Cells.Count; j++)
                {
                    string text = table.Cell(i, j).Range.Text;
                    cells[j - 1] = text.Substring(0, text.Length - 1);
                }
                data.Rows.Add(cells);
            }

            for (int i = 1; i < data.Rows.Count; i++)
            {
                DataBase.ImportFromWord(data.Rows[i][1].ToString(), data.Rows[i][4].ToString(), data.Rows[i][7].ToString(), data.Rows[i][2].ToString());
            }
            closeDoc(doc);

        }

        private static Microsoft.Office.Interop.Word.Document openDoc(string path)
        {
            Object filename = path;
            Object confirmConversions = Type.Missing;
            Object readOnly = Type.Missing;
            Object addToRecentFiles = Type.Missing;
            Object passwordDocument = Type.Missing;
            Object passwordTemplate = Type.Missing;
            Object revert = Type.Missing;
            Object writePasswordDocument = Type.Missing;
            Object writePasswordTemplate = Type.Missing;
            Object format = Type.Missing;
            Object encoding = Type.Missing;
            Object visible = Type.Missing;
            Object openConflictDocument = Type.Missing;
            Object openAndRepair = Type.Missing;
            Object documentDirection = Type.Missing;
            Object noEncodingDialog = Type.Missing;


            word.Documents.Open(ref filename,
            ref confirmConversions,
            ref readOnly,
            ref addToRecentFiles,
            ref passwordDocument,
            ref passwordTemplate,
            ref revert,
            ref writePasswordDocument,
            ref writePasswordTemplate,
            ref format,
            ref encoding,
            ref visible,
            ref openConflictDocument,
            ref openAndRepair,
            ref documentDirection,
            ref noEncodingDialog);

            Word.Document doc = word.Documents.Application.ActiveDocument; //new Microsoft.Office.Interop.Word.Document();
                                                                           // doc = officeWord.Documents.Application.ActiveDocument;
            return doc;
        }

        private static void closeDoc(Microsoft.Office.Interop.Word.Document doc)
        {

            object sch = Word.WdSaveOptions.wdDoNotSaveChanges;
            object aq = Type.Missing;
            object ab = Type.Missing;

            (doc as Microsoft.Office.Interop.Word._Document).Close(ref sch, ref aq, ref ab);
        }
    }
}
