using System.Windows;

namespace japanese
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        Database db = new Database("C:\\Users\\levik\\OneDrive\\Документы\\ТУСУР\\Курсач\\japanese\\japaneseLoginBase.db");
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string res = db.CheckUser(loginTextBox.Text, pwdTextBox.Text);
            if (res == "auth")
            {
                new MainWindow(loginTextBox.Text).Show();
                this.Close();
            }
            else
            {
                MessageBox.Show(caption: "Ошибка", messageBoxText: res);
            }
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            bool legit = true;
            foreach (char c in loginTextBox.Text)
            {
                if (!(c <= 'z' && c >= 'a' || c >= '0' && c <= '9' || c >= 'A' && c <= 'Z'))
                {
                    legit = false;
                    break;
                }
            }
            if (legit)
            {
                string res = db.AddUser(loginTextBox.Text, pwdTextBox.Text);
                MessageBox.Show(res);
            }
            else
            {
                MessageBox.Show(caption: "Ошибка", messageBoxText: "Некорректный логин");
            }

        }
    }
}
