using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Models; 

namespace WpfApp1.pages
{
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();


            CmbModel.ItemsSource = new List<CarModel>
            {
                new CarModel { Name = "Sedan X", BasePrice = 1500000 },
                new CarModel { Name = "SUV Y", BasePrice = 2500000 }
            };
            CmbEngine.ItemsSource = new List<Engine>
            {
                new Engine { Type = "1.6L (110 л.с.)", PriceModifier = 0 },
                new Engine { Type = "2.0L Turbo (190 л.с.)", PriceModifier = 150000 }
            };


            CheckCompletion();
        }

        private void CmbModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            AppData.CurrentConfig.SelectedModel = CmbModel.SelectedItem as CarModel;
            CheckCompletion();
        }

        private void CmbEngine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            AppData.CurrentConfig.SelectedEngine = CmbEngine.SelectedItem as Engine;
            CheckCompletion();
        }

        private void CheckCompletion()
        {

            BtnNext.IsEnabled = AppData.CurrentConfig.SelectedModel != null &&
                                AppData.CurrentConfig.SelectedEngine != null;
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page2());

        }
    }
}