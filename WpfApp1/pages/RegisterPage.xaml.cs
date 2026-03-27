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
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        // Рефакторинг по заданию: метод Register
        public bool Register(string username, string password, string email, string fullName)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(fullName))
            {
                MessageBox.Show("Все поля обязательны для заполнения");
                return false;
            }

            if (Core.Context.Users.Any(u => u.Username == username || u.Email == email))
            {
                MessageBox.Show("Пользователь с таким логином или email уже существует");
                return false;
            }

            var newUser = new Users
            {
                Username = username,
                Password = password,
                Email = email,
                FullName = fullName
            };

            Core.Context.Users.Add(newUser);
            Core.Context.SaveChanges();
            MessageBox.Show("Регистрация успешна");
            NavigationService.Navigate(new MainPage());
            return true;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Register(UsernameTextBox.Text, PasswordBox.Password, EmailTextBox.Text, FullNameTextBox.Text);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
    }
}
