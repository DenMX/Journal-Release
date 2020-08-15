using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Journal_Release
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        
        Info infoWindow;
        SQL sqlWindow;
        SettingsWindow SettingsWindow;
        List<ComboBox> comboType = new List<ComboBox>();
        List<Frame> pages = new List<Frame>();
        public MainWindow()
        {
            InitializeComponent();

            StartPass startPass = new StartPass();
            if (startPass.ShowDialog() == true)
            {
                Comps.NewComp += UpdateCompsListGrid;
                DataBase.LoadDB();
                GetComboList();
                GetPageList();
                //DataBase.GetModelList();
                string tempPath = $@"{Environment.CurrentDirectory}\backupPassword";
                if (File.Exists(tempPath))
                {
                    File.Delete(tempPath);
                }
            }
            else
            {
                this.Close();
            }

            
        }

        private void UpdateCompsListGrid(Comps comp)
        {
            listGrid.Items.Add(comp);
        }

        private void GetComboList()
        {
            comboType.Add(pcType01);
            comboType.Add(pcType02);
            comboType.Add(pcType03);
            comboType.Add(pcType04);
            comboType.Add(pcType05);
            comboType.Add(pcType06);
            comboType.Add(pcType07);
            comboType.Add(pcType08);
        }

        private void GetPageList()
        {
            pages.Add(Page01);
            pages.Add(Page02);
            pages.Add(Page03);
            pages.Add(Page04);
            pages.Add(Page05);
            pages.Add(Page06);
            pages.Add(Page07);
            pages.Add(Page08);
        }

        #region Menu

        private void ImportWord_click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Ms Word 2003 (*.doc) |*.doc| MS Word 2007 (*.docx)|*docx";
            open.Title = "Выберите документ для импорта данных";
            open.FilterIndex = 2;
            if (open.ShowDialog() == true)
                ImportExport.ImportWord(open.FileName);
        }

        private void ImportDB_click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "SQL DataBase(*.mdf) |*.mdf";
            open.Title = "Выберите документ для импорта данных";
            open.FilterIndex = 2;
            if (open.ShowDialog() == true)
            {
                Task.Run(()=>DataBase.ImportDB(open.FileName));
            }
        }

        private void ExportPDF_click(object sender, RoutedEventArgs e)
        {
            ImportExport.ExportToPdf();
        }

        private void Path_click(object sender, RoutedEventArgs e)
        {
            DataBase.ChangePath();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow = new SettingsWindow();
            SettingsWindow.Show();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            listGrid.Items.Clear();
            DataBase.LoadDB();
        }

        private void Sql_click(object sender, RoutedEventArgs e)
        {
            sqlWindow = new SQL();
            sqlWindow.Show();
        }

        private void Info_click(object sender, RoutedEventArgs e)
        {
            infoWindow = new Info();
            infoWindow.Show();
        }

        #endregion

        #region 1st Tab


        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            Regex regex = new Regex(@$"\w*{SearchName.Text}\w*".ToUpper());
            listGrid.Items.Clear();
            foreach (Comps comp in Comps.compsList)
            {
                if (regex.IsMatch(comp.PcName))
                {
                    
                    listGrid.Items.Add(comp);
                }
            }
        }

        private void Search(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SearchButton_Click(this, e);
            }
        }

        private void onSearch(object sender, RoutedEventArgs e)
        {
            SearchName.Text = "";
        }
        #endregion

        #region 2nd Tab
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)//Page01.NavigationService.Navigate(new Uri("Manual.xaml", UriKind.Relative));
        {
            ComboBox combo = (ComboBox)sender;
            LoadPage(combo, combo.Text);
        }

        private void LoadPage(ComboBox sender, string ComboValue)
        {
            for (int i = 0; i < comboType.Count - 1; i++)
            {
                if (sender.Name == comboType[i].Name)
                {
                    if(ComboValue == "Manual")
                    {
                        pages[i].NavigationService.Navigate(new Uri("Auto.xaml", UriKind.Relative));
                    }
                    else
                    {
                        pages[i].NavigationService.Navigate(new Uri("Manual.xaml", UriKind.Relative));
                    }
                    return;
                }
            }

        }


        #endregion

        private void CellCopy_Click(object sender, MouseButtonEventArgs e)//Copy cell when left button click in the grid
        {
            try
            {
                int selectedColumn = listGrid.CurrentCell.Column.DisplayIndex;
                var selectedCell = listGrid.SelectedCells[selectedColumn];
                var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);
                if (cellContent != null)
                {
                    if (cellContent is TextBlock)
                    {
                        Clipboard.Clear();
                        Clipboard.SetText((cellContent as TextBlock).Text);
                    }
                }
            }
            catch(Exception ex)
            {
                File.AppendAllText($@"{Environment.CurrentDirectory}\Logs", ex.ToString() + ex.Message);
            }
        }

        private void CopyPasswords_Click(object sender, MouseButtonEventArgs e)//Copy passwords when right button click
        {
            try
            {
                if (listGrid.SelectedItem != null)
                {
                    Clipboard.Clear();
                    Comps comp = (Comps)listGrid.SelectedItem;
                    Clipboard.SetText($"{comp.AdmPass} {comp.BiosPass}");
                }
            }
            catch(Exception ex)
            {
                File.AppendAllText($@"{Environment.CurrentDirectory}\Logs", ex.ToString() + ex.Message);
            }
        }

        private void ProgramClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DataBase.CloseConnection();
        }
    }
}
