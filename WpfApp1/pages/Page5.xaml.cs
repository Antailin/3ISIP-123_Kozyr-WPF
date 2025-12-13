using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp1.Models;

namespace WpfApp1.pages
{
    public partial class Page5_Summary : Page
    {
        private Core config = AppData.CurrentConfig;

        public Page5_Summary()
        {
            InitializeComponent();
            GenerateReport();
        }

        private void GenerateReport()
        {
            SummaryPanel.Children.Clear();

            if (config.SelectedModel == null)
            {
                SummaryPanel.Children.Add(CreateHeader("Ошибка: Автомобиль не выбран."));
                return;
            }


            SummaryPanel.Children.Add(CreateHeader("1. Конфигурация Автомобиля"));
            SummaryPanel.Children.Add(CreateDetail("Модель", config.SelectedModel?.Name, isBold: true));
            SummaryPanel.Children.Add(CreateDetail("Двигатель", config.SelectedEngine?.Type, $"+{config.SelectedEngine?.PriceModifier:N0} руб."));
            SummaryPanel.Children.Add(CreateDetail("Цвет", config.SelectedColor?.Name, $"+{config.SelectedColor?.PriceModifier:N0} руб."));

            SummaryPanel.Children.Add(CreateHeader("2. Дополнительные Опции"));
            var selectedOptions = config.AvailableOptions.Where(o => o.IsSelected).ToList();
            if (selectedOptions.Any())
            {
                foreach (var opt in selectedOptions)
                {
                    SummaryPanel.Children.Add(CreateDetail($"   {opt.Name}", string.Empty, $"+{opt.Price:N0} руб."));
                }
            }
            else
            {
                SummaryPanel.Children.Add(new TextBlock { Text = "Опции не выбраны.", Margin = new Thickness(0, 5, 0, 5), Foreground = Brushes.Gray });
            }

            SummaryPanel.Children.Add(CreateHeader("3. Контактные Данные"));
            SummaryPanel.Children.Add(CreateDetail("ФИО", config.ClientName));
            SummaryPanel.Children.Add(CreateDetail("Телефон", config.ClientPhone));
            SummaryPanel.Children.Add(CreateDetail("Email", config.ClientEmail ?? "—"));

            SummaryPanel.Children.Add(CreateHeader("4. Финансовый Расчет"));

            decimal totalPrice = config.GetTotalPrice();
            decimal monthlyPayment = config.CalculateMonthlyLoanPayment();
            decimal initialPaymentAmount = totalPrice * (config.InitialPaymentPercent / 100m);
            decimal loanAmount = totalPrice - initialPaymentAmount;

            SummaryPanel.Children.Add(CreateDetail("Общая Цена Авто", string.Empty, $"{totalPrice:N0} руб.", Brushes.Black, FontWeights.SemiBold));

            SummaryPanel.Children.Add(CreateDetail("Начальный Взнос", $"{config.InitialPaymentPercent}%", $"{initialPaymentAmount:N0} руб."));
            SummaryPanel.Children.Add(CreateDetail("Сумма Кредита", $"{config.LoanTermMonths} мес.", $"{loanAmount:N0} руб."));

            SummaryPanel.Children.Add(CreateDetail("ЕЖЕМЕСЯЧНЫЙ ПЛАТЕЖ ", string.Empty, $"{monthlyPayment:N0} руб.", Brushes.DarkGreen, FontWeights.ExtraBold, 16));
        }

        private TextBlock CreateHeader(string text)
        {
            return new TextBlock
            {
                Text = text,
                FontWeight = FontWeights.Bold,
                FontSize = 16,
                Margin = new Thickness(0, 25, 0, 10),
                Foreground = Brushes.Navy
            };
        }

        private StackPanel CreateDetail(string label, string info1, string info2 = null, Brush color = null, FontWeight weight = default, double fontSize = 14)
        {
            if (weight == default) weight = FontWeights.Normal;
            if (color == null) color = Brushes.Black;

            return new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 4, 0, 4),
                Children =
                {
                    new TextBlock { Text = label + ":", Width = 200, FontWeight = FontWeights.SemiBold, FontSize = fontSize, Foreground = color },
                    new TextBlock { Text = info1, Width = 150, FontWeight = weight, FontSize = fontSize, Foreground = color },
                    new TextBlock { Text = info2, FontWeight = weight, FontSize = fontSize, Foreground = color }
                }
            };
        }

        private StackPanel CreateDetail(string label, string value, bool isBold)
        {
            return new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 4, 0, 4),
                Children =
                {
                    new TextBlock { Text = label + ":", Width = 200, FontWeight = FontWeights.SemiBold },
                    new TextBlock { Text = value, FontWeight = isBold ? FontWeights.ExtraBold : FontWeights.Normal, Foreground = Brushes.Black }
                }
            };
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtnFinish_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Заказ на автомобиль '{config.SelectedModel.Name}' успешно оформлен!\nС вами свяжется наш менеджер по телефону: {config.ClientPhone}", "Заказ принят", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}