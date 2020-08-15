using System;
using System.Windows;

namespace Journal_Release
{
    /// <summary>
    /// Логика взаимодействия для NewElement.xaml
    /// </summary>
    public partial class NewElement : Window
    {

        public NewElement()
        {
            InitializeComponent();
        }

        private void Confirm_Click(object sender, EventArgs e)
        {
            if (!char.IsDigit(NameBox.Text[0]) && NameBox.Text.Length >= 3)
            {
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Название не должно начинаться с цифры и быть короче 3 символов");
            }
        }
    }
}
