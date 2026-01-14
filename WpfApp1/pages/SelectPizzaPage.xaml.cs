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
    /// Логика взаимодействия для SelectPizzaPage.xaml
    /// </summary>
    public partial class SelectPizzaPage : Page
    {
        public SelectPizzaPage()
        {
            InitializeComponent();
            LoadPizzas();
        }

        private void LoadPizzas()
        {
            var pizzas = new List<Pizza>
            {
                new Pizza { Name = "Маргарита", Description = "Классика жанра. Томат, сыр, базилик.", BasePrice = 400 },
                new Pizza { Name = "Пепперони", Description = "Острая колбаска для горячих студентов.", BasePrice = 550 },
                new Pizza { Name = "Гавайская", Description = "С ананасами. Гордов одобряет.", BasePrice = 500 },
                new Pizza { Name = "Четыре сыра", Description = "Много сыра не бывает.", BasePrice = 600 },
                new Pizza { Name = "Мясная", Description = "Говядина, курица, бекон, ветчина.", BasePrice = 700 }
            };
            PizzaList.ItemsSource = pizzas;
        }

        private void PizzaList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnNext.IsEnabled = PizzaList.SelectedItem != null;
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            if (PizzaList.SelectedItem is Pizza selected)
            {
                Order newOrder = new Order { SelectedPizza = selected };
                NavigationService.Navigate(new OptionsPage(newOrder));
            }
        }
    }
}
