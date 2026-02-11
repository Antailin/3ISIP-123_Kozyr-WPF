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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public List<Films> Movies { get; set; }

        public MainPage()
        {
            InitializeComponent();
            LoadMovies();
            DataContext = this;
        }

        private void LoadMovies(string search = "", string sort = "")
        {
            var query = Core.Context.Films.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(f => f.Title.Contains(search));
            }

            if (sort == "Название")
            {
                query = query.OrderBy(f => f.Title);
            }
            else if (sort == "Рейтинг")
            {
                query = query.OrderByDescending(f => f.Rating);
            }

            Movies = query.ToList();
            MoviesListView.ItemsSource = Movies;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            LoadMovies(SearchTextBox.Text, (SortComboBox.SelectedItem as ComboBoxItem)?.Content.ToString());
        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            LoadMovies(SearchTextBox.Text, (SortComboBox.SelectedItem as ComboBoxItem)?.Content.ToString());
        }

        private void PersonalPageButton_Click(object sender, RoutedEventArgs e)
        {
            
            NavigationService.Navigate(new PersonalPage());
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterPage());
        }

        private void MoviesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (MoviesListView.SelectedItem is Films selectedFilm)
            {
                NavigationService.Navigate(new MoviePage(selectedFilm));
            }
        }
    }
}
