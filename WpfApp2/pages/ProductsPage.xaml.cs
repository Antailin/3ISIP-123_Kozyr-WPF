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
    /// Логика взаимодействия для ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        public ProductsPage()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void LoadProducts()
        {
            var products = Core.Context.Products.ToList();

            foreach (var product in products)
            {
                var panel = new StackPanel { Margin = new Thickness(10), Width = 200 };

                var img = new Image { Height = 150 };
                if (!string.IsNullOrEmpty(product.Product_IMG))
                {
                    try
                    {
                        img.Source = new BitmapImage(new Uri(product.Product_IMG, UriKind.RelativeOrAbsolute));
                    }
                    catch { }
                }

                var nameText = new TextBlock { Text = product.Product_Name ?? "No Name" };
                var priceText = new TextBlock { Text = $"Price: {(product.Price ?? 0):C}" };
                var addButton = new Button { Content = "Add to Cart", Tag = product };
                addButton.Click += AddToCart_Click;

                panel.Children.Add(img);
                panel.Children.Add(nameText);
                panel.Children.Add(priceText);
                panel.Children.Add(addButton);

                ProductsList.Items.Add(panel);
            }
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var product = button.Tag as Products;
            if (product != null)
            {
                Cart.Add(product);
                MessageBox.Show($"{product.Product_Name} added!");
            }
        }

        private void GoToCart_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CartPage());
        }
    }
}

