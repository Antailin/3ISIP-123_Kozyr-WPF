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
using WpfApp2;

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
            ProductsPanel.Children.Clear();
            var products = Core.Context.Products.ToList();

            foreach (var product in products)
            {
                var card = new Border
                {
                    Margin = new Thickness(8),
                    Padding = new Thickness(10),
                    Background = Brushes.White,
                    BorderBrush = Brushes.LightGray,
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(6)
                };

                var panel = new StackPanel();

                // Фото — полностью видно, без обрезки
                var img = new Image
                {
                    Height = 140,
                    Width = 140,
                    Stretch = Stretch.Uniform,          // полностью видно, пропорции сохранены
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 0, 0, 8)
                };

                if (!string.IsNullOrEmpty(product.Product_IMG))
                {
                    try
                    {
                        img.Source = new BitmapImage(new Uri(product.Product_IMG, UriKind.RelativeOrAbsolute));
                    }
                    catch { }
                }

                var name = new TextBlock
                {
                    Text = product.Product_Name ?? "Без названия",
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Center,
                    FontWeight = FontWeights.SemiBold,
                    Margin = new Thickness(0, 0, 0, 4)
                };

                var price = new TextBlock
                {
                    Text = $"{(product.Price ?? 0):C}",
                    FontSize = 15,
                    Foreground = Brushes.DarkGreen,
                    TextAlignment = TextAlignment.Center,
                    Margin = new Thickness(0, 0, 0, 8)
                };

                var addBtn = new Button
                {
                    Content = "В корзину",
                    Padding = new Thickness(12),
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                addBtn.Tag = product;
                addBtn.Click += AddToCart_Click;

                panel.Children.Add(img);
                panel.Children.Add(name);
                panel.Children.Add(price);
                panel.Children.Add(addBtn);

                card.Child = panel;
                ProductsPanel.Children.Add(card);
            }
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is Products product)
            {
                Cart.Add(product);
                MessageBox.Show($"{product.Product_Name} добавлен в корзину");
            }
        }

        private void GoToCart_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CartPage());
        }
    }
}