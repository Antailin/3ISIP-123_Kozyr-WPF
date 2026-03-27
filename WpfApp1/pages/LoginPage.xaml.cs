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

namespace WpfApp1.pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        // Рефакторинг по заданию: метод Auth
        public bool Auth(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Поля обязательны для заполнения");
                return false;
            }

            var user = Core.Context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                MessageBox.Show("Вход выполнен успешно");
                NavigationService.Navigate(new MainPage());
                return true;
            }
            else
            {
                MessageBox.Show("Неверные учетные данные");
                return false;
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Auth(UsernameTextBox.Text, PasswordBox.Password);  // только вызов!
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
    }
}
