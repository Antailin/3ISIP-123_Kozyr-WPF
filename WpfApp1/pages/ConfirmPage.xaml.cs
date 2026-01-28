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
    /// Логика взаимодействия для ConfirmPage.xaml
    /// </summary>
    public partial class ConfirmPage : Page
    {
        private Order finalOrder;
        public ConfirmPage(Models.Order newOrder)
        {
            InitializeComponent();
            finalOrder = newOrder;
            GenerateSummary();
        }

        private void GenerateSummary()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Пицца: {finalOrder.SelectedPizza.Name}");
            sb.AppendLine($"Размер: {finalOrder.SizeName}");

            if (finalOrder.Toppings.Count > 0)
            {
                sb.Append("Добавки: ");
                foreach (var t in finalOrder.Toppings) sb.Append($"{t.Name}, ");
                sb.AppendLine();
            }

            sb.AppendLine($"\nК ОПЛАТЕ: {finalOrder.TotalPrice}");
            TxtSummary.Text = sb.ToString();
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TbName.Text))
            {
                MessageBox.Show("Имя введи, студент! А то как я тебя по списку найду?", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string payment = (CmbPayment.SelectedItem as ComboBoxItem).Content.ToString();

            MessageBox.Show(
                $"Спасибо, {TbName.Text}!\nЗаказ принят.\nОплата: {payment}\n",
                "Успех",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            NavigationService.Navigate(new SelectPizzaPage());
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
