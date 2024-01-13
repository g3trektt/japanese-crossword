using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace japanese
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SolidColorBrush blackBg = new SolidColorBrush(Colors.Black);
        SolidColorBrush whiteBg = new SolidColorBrush(Colors.White);
        Bitmap bmp = new Bitmap(@"..\..\..\..\..\japanese.bmp");
        int[,] screenPicture;
        int[,] origPicture;
        DateTime start;
        string user;
        public MainWindow(string login)
        {
            user = login;
            InitializeComponent();
            ButtonGridFiller();
            screenPicture = new int[32, 32];
            origPicture = BitmapTools.ToArray(bmp);
            int[,] rowStreakArray = BitmapTools.GetRowStreaks(origPicture);
            int[,] columnStreakArray = BitmapTools.GetColumnStreaks(origPicture);
            RowStreaksToGrid(rowStreakArray, rowGrid);
            ColumnStreaksToGrid(columnStreakArray, columnGrid);
            start = DateTime.Now;
        }

        bool Checker()
        {
            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    if (origPicture[i, j] != screenPicture[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        
        private void RowStreaksToGrid(int[,] array, Grid grid)
        {
            for (int i = 0; i < 32; i++)
            {
                TextBlock textBlock = new TextBlock();
                grid.RowDefinitions.Add(new RowDefinition());
                Grid.SetRow(textBlock, i);
                grid.Children.Add(textBlock);
                textBlock.TextAlignment = TextAlignment.Right;
                for (int j = 0; j < 32; j++)
                {
                    if (array[i, j] != 0)
                    {
                        textBlock.Text += array[i, j] + " ";
                    }
                }
            }
        }
        private void ColumnStreaksToGrid(int[,] array, Grid grid)
        {
            for (int i = 0; i < 32; i++)
            {
                TextBlock textBlock = new TextBlock();
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                Grid.SetColumn(textBlock, i);
                grid.Children.Add(textBlock);
                textBlock.TextAlignment = TextAlignment.Center;
                textBlock.VerticalAlignment = VerticalAlignment.Bottom;
                for (int j = 0; j < 32; j++)
                {
                    if (array[i, j] != 0)
                    {
                        textBlock.Text = textBlock.Text+"\n"+array[i, j];
                    }
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Button btn = (Button)sender;
            //btn.Background = btn.Background == blackBg ? whiteBg : blackBg;
            int[] indexes = { int.Parse(btn.Uid.Split(' ')[0]), int.Parse(btn.Uid.Split(' ')[1]) };
            if (btn.Background == blackBg)
            {
                btn.Background = whiteBg;
                screenPicture[indexes[0], indexes[1]] = 0;
            }
            else
            {
                btn.Background = blackBg;
                screenPicture[indexes[0], indexes[1]] = 1;
            }
            if (Checker())
            {
                DateTime end = DateTime.Now;
                WinnerWindow winner = new WinnerWindow(user, end - start);
                winner.ShowDialog();
                MessageBox.Show("ahahah");
                
            }
        }
        private void ButtonGridFiller()
        {
            for (int i = 0; i < 32; i++)
            {
                buttonGrid.RowDefinitions.Add(new RowDefinition());
                buttonGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    Button btn = new Button
                    {
                        Background = /*i * j % 2 == 0 ?*/ whiteBg/* : new SolidColorBrush(Color.FromRgb(255, 255, 255))*/,
                        Uid = $"{i} {j}",
                        BorderBrush = blackBg,
                        BorderThickness = new Thickness(1, 1, 1, 1),

                    };
                    btn.Click += Button_Click;
                    Grid.SetRow(btn, i);
                    Grid.SetColumn(btn, j);
                    buttonGrid.Children.Add(btn);
                }
            }
        }
        private void Checker(object sender, RoutedEventArgs e)
        {
            string res = "";
            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    if (origPicture[i, j] != screenPicture[i, j])
                    {
                        res += $"{i},{j} ";
                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Database db = new Database("C:\\Users\\levik\\OneDrive\\Документы\\ТУСУР\\Курсач\\japanese\\japaneseLoginBase.db");
            string t = db.AddUser("test", "test");
            Button b = (Button)sender;
            b.Content = t;

        }
    }
}
