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
using WpfApp1.Models;

namespace WpfApp1.pages
{
    /// <summary>
    /// Логика взаимодействия для AddsPage.xaml
    /// </summary>
    public partial class AddsPage : Page
    {
        private Order currentOrder;
        public AddsPage(Models.Order newOrder)
        {
            InitializeComponent();
            currentOrder = newOrder;
            TxtPizzaName.Text = currentOrder.SelectedPizza.Name;

            CmbToppings.ItemsSource = new List<Topping>
            {
                new Topping { Name = "Сыр", Price = 50 },
                new Topping { Name = "Бекон", Price = 80 },
                new Topping { Name = "Грибы", Price = 40 }
            };

            UpdateTotal();
        }

        private void Size_Checked(object sender, RoutedEventArgs e)
        {
            if (currentOrder == null) return;

            if (RbSmall.IsChecked == true) { currentOrder.SizeMultiplier = 1.0; currentOrder.SizeName = "Маленькая"; }
            if (RbMedium.IsChecked == true) { currentOrder.SizeMultiplier = 1.2; currentOrder.SizeName = "Средняя"; }
            if (RbLarge.IsChecked == true) { currentOrder.SizeMultiplier = 1.4; currentOrder.SizeName = "Большая"; }

            UpdateTotal();
        }

        private void BtnAddTopping_Click(object sender, RoutedEventArgs e)
        {
            if (CmbToppings.SelectedItem is Topping topping)
            {
                currentOrder.Toppings.Add(topping);
                ListAddedToppings.ItemsSource = null;
                ListAddedToppings.ItemsSource = currentOrder.Toppings;

                UpdateTotal();
            }
        }

        private void UpdateTotal()
        {
            TxtTotal.Text = $"Итого: {currentOrder.TotalPrice}";
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtnCheckout_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ConfirmPage(currentOrder));
        }
    }

}
