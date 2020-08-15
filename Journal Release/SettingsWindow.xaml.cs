using Journal_Release.Constructor;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;



namespace Journal_Release
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        
        public SettingsWindow()
        {
            InitializeComponent();

            XMLWorker.GetXML();
            foreach(string s in XMLWorker.GetProgramTypes())
            {
                SoftType.Items.Add(s);
            }
            PsText.Text = XMLWorker.GetSettings();
            Path_PsExec.Text = Settings2.Default.psExec_Path;
            ScriptFolder.Text = Settings2.Default.scriptFolder;
            DataBase.CreateNewModel += UpdateModelsList;
            SetModels();
        }


        private void SetModels()
        {
            List<string>l = DataBase.GetModelList();
            foreach (string s in l)
            {
                DriverModel.Items.Add(s);
                biosModel.Items.Add(s);
            }
        }

        private void UpdateModelsList(string modelName)
        {
            DriverModel.Items.Add(modelName);
            biosModel.Items.Add(modelName);

        }

        #region Soft - 1st tab

        private void AddSoftType(object sender, RoutedEventArgs e)
        {
            NewElement element = new NewElement();
            if(element.ShowDialog() == true)
            {
                XMLWorker.AddSoftType(element.NameBox.Text);
                SoftType.Items.Add(element.NameBox.Text);
            }
        }

        private void AddSoft_Click(object sender, RoutedEventArgs e)
        {
            XMLWorker.AddXMLElement("programs", SoftType.Text, SoftName.Text, SoftPath.Text, SoftParametrs.Text);
            SoftName.Text = "";
            SoftPath.Text = "";
            SoftParametrs.Text = "";
            SoftGrid.Items.Clear();

            foreach (Soft soft in XMLWorker.GetPrograms(SoftType.SelectedItem.ToString()))
            {
                SoftGrid.Items.Add(soft);
            }
        }

        private void SoftPath_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "EXE(*.exe) |*.exe";
            open.Title = "Укажите размещение программы";
            open.FilterIndex = 2;
            if (open.ShowDialog() == true)
            {
                SoftPath.Text = open.FileName;
            }
        }
        public void SoftDelete(object sender, RoutedEventArgs e)
        {
            Soft soft = (Soft)SoftGrid.SelectedItem;
            var name = soft.SoftName;
            if(MessageBox.Show($"Вы действительно хотите удалить {name}", "Удаление элемента", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                XMLWorker.DeleteSoft(SoftType.Text, name);
                SoftGrid.Items.Remove(SoftGrid.SelectedItem);
            }
        }

         private void OnSoftTypeChange(object sender, SelectionChangedEventArgs e)
         {
            SoftGrid.Items.Clear();
            foreach (Soft soft in XMLWorker.GetPrograms(SoftType.SelectedItem.ToString()))
            {
                SoftGrid.Items.Add(soft);
            }
         }       
        
        #endregion
        #region Drivers - 2nd tab

        public void DriverDelete(object sender, RoutedEventArgs e)
        {
            Driver driver = (Driver)DriversGrid.SelectedItem;
            var name = driver.DriverName;
            if(MessageBox.Show($"Вы действительно хотите удалить {name}?", "Удаление элемента", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                XMLWorker.DeleteDriver(DriverModel.Text, name);
                DriversGrid.Items.Remove(DriversGrid.SelectedItem);
            }                
        }

        private void AddModel_Click(object sender, RoutedEventArgs e)
        {
            NewElement element = new NewElement();
            if(element.ShowDialog() == true)
            {
                DataBase.AddNewModel(element.NameBox.Text);
            }
        }

        private void AddDriver_Click(object sender, RoutedEventArgs e)
        {
            XMLWorker.AddXMLElement("drivers", DriverModel.Text, DriverName.Text, DriverPath.Text, DriverParametrs.Text);
            DriverName.Text = "";
            DriverPath.Text = "";
            DriverParametrs.Text = "";
            DriversGrid.Items.Clear();
            foreach(Driver driver in XMLWorker.GetDrivers(DriverModel.Text))
            {
                DriversGrid.Items.Add(driver);
            }
        }        
        
        private void DriverPath_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "EXE(*.exe) |*.exe";
            open.Title = "Укажите размещение драйвера";
            open.FilterIndex = 2;
            if(open.ShowDialog() == true)
            {
                DriverPath.Text = open.FileName;
            }
        }
        private void OnModelChange(object sender, SelectionChangedEventArgs e)
        {
            List<Driver> drivers = XMLWorker.GetDrivers(DriverModel.SelectedItem.ToString());
            
            DriversGrid.Items.Clear();
            if (drivers != null)
            {
                foreach (Driver driver in drivers)
                {
                    DriversGrid.Items.Add(driver);
                }
            }
                        
        }
        #endregion
        #region Settings - 3rd tab

        private void SettingsSave_Click(object sender, RoutedEventArgs e)
        {
            XMLWorker.UpdateSettings(PsText.Text);
            Settings2.Default.scriptFolder = ScriptFolder.Text;
            Settings2.Default.Save();
        }

        private void SetPsExecPath(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "EXE(*.exe) |*.exe";
            open.Title = "Укажите размещение psExec";
            open.FilterIndex = 2;
            if (open.ShowDialog() == true)
            {
                Settings2.Default.psExec_Path = open.FileName;
                Settings2.Default.Save();
                Path_PsExec.Text = open.FileName;
            }
        }


        #endregion
        #region Bios Settings - 4th tab
        private void OnBiosScriptChanged(object sender, RoutedEventArgs e)//Event changed bios script text
        {
            XMLWorker.BiosSettingsChange(biosModel.Text, biosSettingsBox.Text, setBiosPassword.Text, resetBiosPassword.Text);
        }

        private void OnBiosModelChange(object sender, SelectionChangedEventArgs e)
        {
            biosSettingsBox.Text = XMLWorker.GetBiosScript(biosModel.SelectedItem.ToString());
            setBiosPassword.Text = XMLWorker.GetInstallingBiosPassScript(biosModel.SelectedItem.ToString());
            resetBiosPassword.Text = XMLWorker.GetResetBiosPassScript(biosModel.SelectedItem.ToString());
        }

        #endregion

    }
}
