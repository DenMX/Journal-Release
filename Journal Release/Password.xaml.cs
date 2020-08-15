using System;
using System.Windows;
using System.Windows.Controls;

namespace Journal_Release
{
    /// <summary>
    /// Логика взаимодействия для Password.xaml
    /// </summary>
    public partial class Password : Window
    {
        public EventHandler Click;
        public string Pass { get { return PasswordBox.Password; } }
        public Password()
        {
            InitializeComponent();
        }

        private void SetPassword(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
