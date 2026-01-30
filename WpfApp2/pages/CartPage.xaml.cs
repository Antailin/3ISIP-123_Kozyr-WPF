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

namespace WpfApp2.pages
{
    /// <summary>
    /// Логика взаимодействия для CartPage.xaml
    /// </summary>
    public partial class CartPage : Page
    {
        public CartPage()
        {
            InitializeComponent();
            CartList.ItemsSource = Cart.Items;
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag is int productId)
            {
                var item = Cart.Items.FirstOrDefault(i => i.Product.ID == productId);
                if (item != null)
                {
                    Cart.Decrease(item.Product);
                    CartList.ItemsSource = null;
                    CartList.ItemsSource = Cart.Items;
                }
            }
        }

        private void GoToCheckout_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CheckoutPage());
        }

        private void BackToProducts_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductsPage());
        }
    }
}
