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

namespace WpfApp3
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
            LoadFilms();
        }

        private void LoadFilms(string search = "", string sortBy = "Title")
        {
            var films = Core.Context.Films.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                films = films.Where(f => f.Title.Contains(search));
            }
            if (sortBy == "Rating")
            {
                films = films.OrderByDescending(f => f.Rating);
            }
            else
            {
                films = films.OrderBy(f => f.Title);
            }
            FilmsGrid.ItemsSource = films.ToList().Select(f => new
            {
                f.FilmId,
                Cover = new BitmapImage(new Uri(f.CoverImage ?? "", UriKind.RelativeOrAbsolute)),
                f.Title,
                f.Rating,
                ReleaseDate = f.ReleaseDate,
                AgeRating = f.AgeRating.Age
            });
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadFilms(SearchBox.Text, SortCombo.SelectedItem?.ToString() ?? "Title");
        }

        private void SortCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadFilms(SearchBox.Text, SortCombo.SelectedItem?.ToString() ?? "Title");
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentUserId == -1)
            {
                NavigationService.Navigate(new LoginPage(this));
            }
            else
            {
                NavigationService.Navigate(new ProfilePage(currentUserId));
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage(this));
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterPage(this));
        }

        private void FilmsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FilmsGrid.SelectedItem is dynamic selected)
            {
                NavigationService.Navigate(new FilmDetailsPage(selected.FilmId, currentUserId));
            }
        }

        public void SetUserId(int userId)
        {
            currentUserId = userId;
            LoadFilms(); 
        }
    }
}
