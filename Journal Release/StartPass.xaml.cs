using System.Windows;
using System.Windows.Input;

namespace Journal_Release
{
    /// <summary>
    /// Логика взаимодействия для StartPass.xaml
    /// </summary>
    public partial class StartPass : Window
    {

        public StartPass()
        {
            InitializeComponent();
        }

        private void OnEnteredPass(object sender, RoutedEventArgs e)
        {
            if (Crypter.CheckPass(PassBox.Password))
            {
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Неверный пароль!");
                PassBox.Password = "";
            }
        }

        private void PassEnter_Click(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                OnEnteredPass(sender, e);
            }
        }

    }
}
