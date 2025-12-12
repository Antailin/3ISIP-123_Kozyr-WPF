using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Models;
using System.Linq;

namespace WpfApp1.pages
{
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();

            CmbColor.ItemsSource = new List<CarColor>
            {
                new CarColor { Name = "Белый", PriceModifier = 0, HexCode = "#FFFFFF" },
                new CarColor { Name = "Черный Металлик", PriceModifier = 35000, HexCode = "#000000" },
                new CarColor { Name = "Красный", PriceModifier = 50000, HexCode = "#FF0000" }
            };


            if (AppData.CurrentConfig.AvailableOptions.Count == 0)
            {
                AppData.CurrentConfig.AvailableOptions = new List<CarOption>
                {
                    new CarOption { Name = "Пакет 'Зима'", Price = 45000 },
                    new CarOption { Name = "Премиум аудиосистема", Price = 90000 },
                    new CarOption { Name = "Панорамная крыша", Price = 120000 }
                };
            }

            ListOptions.ItemsSource = AppData.CurrentConfig.AvailableOptions;

            CmbColor.SelectedItem = AppData.CurrentConfig.SelectedColor;

            UpdatePrice();
            CheckCompletion();
        }

        private void CmbColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AppData.CurrentConfig.SelectedColor = CmbColor.SelectedItem as CarColor;
            UpdatePrice();
            CheckCompletion();
        }

        private void Option_CheckChanged(object sender, RoutedEventArgs e)
        {
            UpdatePrice();
        }

        private void UpdatePrice()
        {
            TxtCurrentPrice.Text = $"Текущая цена: {AppData.CurrentConfig.GetTotalPrice()} руб.";
        }

        private void CheckCompletion()
        {
            BtnNext.IsEnabled = AppData.CurrentConfig.SelectedColor != null;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack(); 
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page3()); 
        }
    }
}