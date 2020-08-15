using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace Journal_Release
{
    /// <summary>
    /// Логика взаимодействия для Manual.xaml
    /// </summary>
    public partial class Manual : Page
    {
        public Manual()
        {
            InitializeComponent();
            DataBase.CreateNewModel += UpdateModelsList;

        }

        private void UpdateModelsList(string modelName)
        {
            pcModel01.Items.Add(modelName);
        }

        private bool CheckList()
        {
            List<CheckBox> checkList = new List<CheckBox>();

            checkList.Add(Image01);
            checkList.Add(Windows01);
            checkList.Add(locAdm01);
            checkList.Add(Domain01);
            checkList.Add(AdmGroup01);
            checkList.Add(DelUser01);
            checkList.Add(Drivers01);
            checkList.Add(Soft01);
            checkList.Add(Services01);
            checkList.Add(Wsus01);
            checkList.Add(AdmPassCheck01);
            checkList.Add(BiosPassCheck01);

            foreach(CheckBox check in checkList)
            {
                if (check.IsChecked == false)
                    return false;
            }
            return true;
        }

        private void AddPc1(object sender, RoutedEventArgs e)
        {            
            if (CheckList() && !Comps.isNew(pcName01.Text))
                DataBase.Update(pcName01.Text, AdmPass01.Text, BiosPass01.Text, pcModel01.SelectedItem.ToString());
            else if (CheckList() && Comps.isNew(pcName01.Text))
                DataBase.Add(pcName01.Text, AdmPass01.Text, BiosPass01.Text, pcModel01.SelectedItem.ToString());
            else if (!CheckList() && !Comps.isNew(pcName01.Text))
            {
                if (MessageBox.Show("Не все пункты отмечены. Вы уверены что хотите добавить", "Замечены пропуски.", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    DataBase.Update(pcName01.Text, AdmPass01.Text, BiosPass01.Text, pcModel01.Text);
                }
                else
                    return;
            }
            else if (!CheckList() && Comps.isNew(pcName01.Text))
            {
                if (MessageBox.Show("Не все пункты отмечены. Вы уверены что хотите добавить", "Замечены пропуски.", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    DataBase.Add(pcName01.Text, AdmPass01.Text, BiosPass01.Text, pcModel01.SelectedItem.ToString());
                }

            }
            pcName01.IsReadOnly = false;
            AdmPass01.Text = "";
            BiosPass01.Text = "";
        }


        private void PcName1(object sender, RoutedEventArgs e)
        {
            pcName01.Text = "";
        }


        private void TextCheck1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                pcName01.IsReadOnly = true;
                isNew(pcName01.Text);
                if(pcModel01.Items.Count == 0)
                {
                    foreach (string model in DataBase.GetModelList())
                    {
                        pcModel01.Items.Add(model);
                    }
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
        }

        private void Cancel1_Click(object sender, RoutedEventArgs e)
        {
            pcName01.IsReadOnly = false;
            pcName01.Text = "";
            AdmPass01.Text = "";
            BiosPass01.Text = "";

        }

        private void isNew(string PcName)
        {
            pcName01.IsReadOnly = true;
            if (!Comps.isNew(PcName))
            {
                Comps comp = Comps.GetPasswords(pcName01.Text);
                if (comp.AdmPass.Length < Settings2.Default.admPassLenght)
                {
                    if (MessageBox.Show("Длинна пароля локального админа меньше установленной длинны. Хотите сгенерировать новый пароль?", "Пароль не соответствует длинне", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
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

                if (comp.BiosPass.Length < Settings2.Default.biosPassLenght)
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
            }
            else
                PassGenerator.GetPass(AdmPass01, BiosPass01);
        }
    }
}
