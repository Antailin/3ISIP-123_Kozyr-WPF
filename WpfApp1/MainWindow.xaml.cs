using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.pages;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int currentUserId = -1;

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new FilmListPage(this));
        }

        public void SetCurrentUser(int userId)
        {
            currentUserId = userId;
        }

        public int GetCurrentUser() => currentUserId;

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (MainFrame.Content is FilmListPage filmList)
            {
                filmList.ApplyFilter(SearchBox.Text, SortCombo.SelectedItem as ComboBoxItem);
            }
        }

        private void SortCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainFrame.Content is FilmListPage filmList)
            {
                filmList.ApplyFilter(SearchBox.Text, SortCombo.SelectedItem as ComboBoxItem);
            }
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentUserId == -1)
            {
                MainFrame.Navigate(new LoginPage(this));
            }
            else
            {
                MainFrame.Navigate(new ProfilePage(this));
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new LoginPage(this));
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new RegisterPage(this));
        }
    }
}