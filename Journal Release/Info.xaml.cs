using Journal_Release;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Journal_Release
{
    /// <summary>
    /// Логика взаимодействия для Info.xaml
    /// </summary>
    public partial class Info : Window
    {
        public Info()
        {
            InitializeComponent();
            AdmPassLenght.Text = Settings2.Default.admPassLenght.ToString();
            BiosPassLenght.Text = Settings2.Default.biosPassLenght.ToString();
        }

        private void OnImageClick(object sender, MouseButtonEventArgs e)
        {
            ChangePass changePass = new ChangePass();
            changePass.Show();
        }

        private void On_chAdmPassLenghtClick(object sender, RoutedEventArgs e)
        {
            if (Int32.Parse(AdmPassLenght.Text) >= 10)
            {
                try
                {
                    Settings2.Default.admPassLenght = Int32.Parse(AdmPassLenght.Text);
                    Settings2.Default.Save();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($" Ошибка сохранения {ex}");
                }
            }
            else if (Int32.Parse(AdmPassLenght.Text) == 0)
                MessageBox.Show("Ты серьезно?!");
            else
                MessageBox.Show("Нельзя установить меньше 10 знаков");
        }

        private void On_chBiosPassLenghtClick(object sender, RoutedEventArgs e)
        {
            if (Int32.Parse(BiosPassLenght.Text) >= 10)
            {
                try
                {
                    Settings2.Default.biosPassLenght = Int32.Parse(BiosPassLenght.Text);
                    Settings2.Default.Save();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка {ex}");
                }
            }
            else if (Int32.Parse(BiosPassLenght.Text) == 0)
                MessageBox.Show("Ты серьезно?!");
            else
                MessageBox.Show("Нельзя установить меньше 10 знаков");
        }
    }
}
