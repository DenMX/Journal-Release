using System.Windows;

namespace Journal_Release
{
    /// <summary>
    /// Логика взаимодействия для ChangePass.xaml
    /// </summary>
    public partial class ChangePass : Window
    {
        public ChangePass()
        {
            InitializeComponent();
        }

        private void ChangePass_Click(object sender, RoutedEventArgs e)
        {
            if (OldPassword.Password != "" && NewPassword.Password != "" && ConfirmPassword.Password != "")
            {
                if (Crypter.CheckPass(OldPassword.Password) && NewPassword.Password == ConfirmPassword.Password)
                {
                    if (OldPassword.Password != NewPassword.Password)
                    {
                        Crypter.changePass(NewPassword.Password);
                        MessageBox.Show("Пароль изменен");
                        OldPassword.Password = "";
                        NewPassword.Password = "";
                        ConfirmPassword.Password = "";
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Пароли не должны повторяться");
                        return;
                    }
                }
            }
            if (!Crypter.CheckPass(OldPassword.Password))
            {
                MessageBox.Show("Неправильно указан текущий пароль");
                return;
            }
            else if (NewPassword.Password != ConfirmPassword.Password && NewPassword.Password != "" && ConfirmPassword.Password != "")
            {
                MessageBox.Show("Новый пароль и подтверждение не совпадают.");
                return;
            }
            else
            {
                MessageBox.Show("Необходимо заполнить все поля");
            }

        }
    }
}
