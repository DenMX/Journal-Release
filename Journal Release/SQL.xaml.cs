using System.Windows;

namespace Journal_Release
{
    /// <summary>
    /// Логика взаимодействия для SQL.xaml
    /// </summary>
    public partial class SQL : Window
    {
        public SQL()
        {
            InitializeComponent();
        }

        private void ExecuteSql(object sender, RoutedEventArgs e)
        {
            if (SqlText.Text != null)
            {
                DataBase.SqlWindow(SqlText.Text);
            }
        }
    }
}
