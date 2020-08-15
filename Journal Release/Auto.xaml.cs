using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Journal_Release
{
    /// <summary>
    /// Логика взаимодействия для Auto.xaml
    /// </summary>
    public partial class Auto : Page
    {

        public Auto()
        {
            InitializeComponent();
            
            DataBase.CreateNewModel += ModelsListUpdate;
        }

        private void ModelsListUpdate(string modelName)
        {
            pcModel01.Items.Add(modelName);
        }

        private void Cancel1_Click(object sender, RoutedEventArgs e)
        {
            pcName01.IsReadOnly = false;
            pcName01.Text = "";
            AdmPass01.Text = "";
            BiosPass01.Text = "";
        }

        private void OnEnter_Click(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Status.Content = "";
                ProgressBar.Value = 0;
                SoftType.Items.Clear();
                pcModel01.Items.Clear();
                foreach(string type in XMLWorker.GetProgramTypes())
                {
                    SoftType.Items.Add(type);
                }
                foreach(string model in DataBase.GetModelList())
                {
                    pcModel01.Items.Add(model);
                }
                pcName01.IsReadOnly = true;
                if (!Comps.isNew(pcName01.Text))
                {
                    Comps comp = Comps.GetPasswords(pcName01.Text);
                    if(comp.AdmPass.Length < Settings2.Default.admPassLenght)
                    {
                        if(MessageBox.Show("Длинна пароля локального админа меньше установленной длинны. Хотите сгенерировать новый пароль?", "Пароль не соответствует длинне", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            AdmPass01.Text = PassGenerator.GetAdmPass();
                        }
                        else
                        {
                            AdmPass01.Text = comp.AdmPass;
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("Хотите создать новый пароль?", "Пароль локального адина", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                            AdmPass01.Text = PassGenerator.GetAdmPass();
                        else
                            AdmPass01.Text = comp.AdmPass;
                    }

                    if(comp.BiosPass.Length < Settings2.Default.biosPassLenght)
                    {
                        if (MessageBox.Show("Длинна пароля BIOS меньше установленной длинны. Хотите сгенерировать новый пароль?", "Пароль не соответствует длинне", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            BiosPass01.Text = PassGenerator.GetBiosPass();
                        }
                        else
                        {
                            BiosPass01.Text = comp.BiosPass;
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("Хотите создать новый пароль?", "Пароль BIOS", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                            BiosPass01.Text = PassGenerator.GetBiosPass();
                        else
                            BiosPass01.Text = comp.BiosPass;
                    }
                    string backup = $@"{pcName01.Text} {AdmPass01.Text} {BiosPass01.Text}";
                    byte[] array = Encoding.Default.GetBytes(backup);
                    string tempPath = $@"{Environment.CurrentDirectory}\backupPassword";
                    using (FileStream fs = new FileStream(tempPath, FileMode.Append))
                    {
                        fs.Write(array, 0, array.Length);
                    }
                    File.SetAttributes(tempPath, FileAttributes.Hidden);
                }
                else
                {
                    AdmPass01.Text = PassGenerator.GetAdmPass();
                    BiosPass01.Text = PassGenerator.GetBiosPass();
                }
            }
        }

        private void OnReady(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Для автоматической настройки необходимо иметь права доступа на удаленной машине, а так же включить службу 'Удаленный реестр' и отключить брэндмауэр", 
                "Подготовка установки", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                try
                {
                    if (Comps.isNew(pcName01.Text))
                    {
                        DataBase.Add(pcName01.Text, AdmPass01.Text, BiosPass01.Text, pcModel01.Text);
                    }
                    else
                    {
                        DataBase.Update(pcName01.Text, AdmPass01.Text, BiosPass01.Text, pcModel01.Text);
                    }

                    RemoteInstall install = new RemoteInstall(AdmPass01.Text, BiosPass01.Text);
                    install.InstallFinished += ClearPage;
                    install.ProgressChange += ProgressChecking;
                    install.Install(pcName01.Text, SoftType.Text, pcModel01.Text);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    File.AppendAllText($@"{Environment.CurrentDirectory}\Logs", $"{DateTime.Now}:{ex.Message}");
                }
            }
        }

        private void ProgressChecking(string message, int percent)
        {
            Status.Content = message;
            ProgressBar.Value = percent;
        }

        private void ClearPage(object sender, EventArgs e)
        {
            pcName01.IsReadOnly = false;
            pcName01.Text = "";
            AdmPass01.Text = "";
            BiosPass01.Text = "";
            foreach(UIElement element in Panel.Children)
            {
                if(element is CheckBox)
                {
                    (element as CheckBox).IsChecked = false;
                }
            }
        }
    }
}
