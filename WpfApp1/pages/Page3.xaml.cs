using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Models;

namespace WpfApp1.pages
{
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            SliderPayment.Value = (double)AppData.CurrentConfig.InitialPaymentPercent;
            SliderTerm.Value = (double)AppData.CurrentConfig.LoanTermMonths;

            Recalculate();
        }

        private void OnSliderChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Recalculate();
        }

        private void Recalculate()
        {

            if (TxtInitialPayment == null || TxtLoanTerm == null) return;

            decimal price = AppData.CurrentConfig.GetTotalPrice();

            decimal percent = (decimal)SliderPayment.Value;
            int months = (int)SliderTerm.Value;

            AppData.CurrentConfig.InitialPaymentPercent = percent;
            AppData.CurrentConfig.LoanTermMonths = months;

            decimal paymentAmount = price * (percent / 100.0m); 
            decimal monthlyPayment = AppData.CurrentConfig.CalculateMonthlyLoanPayment();

            TxtInitialPayment.Text = $"{percent:0}% ({paymentAmount:N0} руб.)";
            TxtLoanTerm.Text = $"{months} месяцев";
            TxtTotalPrice.Text = $"Цена авто: {price:N0} руб.";
            TxtMonthlyPayment.Text = $"Ежемесячный платеж: {monthlyPayment:N0} руб.";
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => NavigationService.GoBack();
        private void BtnNext_Click(object sender, RoutedEventArgs e) => NavigationService.Navigate(new Page4());
    }
}