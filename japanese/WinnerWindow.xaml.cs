using System;
using System.Windows;

namespace japanese
{
    /// <summary>
    /// Логика взаимодействия для WinnerWindow.xaml
    /// </summary>
    public partial class WinnerWindow : Window
    {
        public WinnerWindow(string user, TimeSpan time)
        {
            InitializeComponent();
            TimeSpan winTime = time;
            string winner = user;
            string res = $"Пользователь {winner} решил головоломку за {winTime.Minutes} мин {winTime.Seconds} сек";
            winTextBox.Text = res;
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {

            Environment.Exit(0);
        }
    }
}
