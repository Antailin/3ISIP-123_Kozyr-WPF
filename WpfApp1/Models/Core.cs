using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace WpfApp1.Models
{
    public class Core
    {
        public CarModel SelectedModel { get; set; }
        public Engine SelectedEngine { get; set; }
        public CarColor SelectedColor { get; set; }

        public List<CarOption> AvailableOptions { get; set; } = new List<CarOption>();

        public decimal InitialPaymentPercent { get; set; } = 20m;
        public int LoanTermMonths { get; set; } = 24;

        public string ClientName { get; set; }
        public string ClientPhone { get; set; }
        public string ClientEmail { get; set; }

        public decimal GetTotalPrice()
        {
            decimal total = 0;
            if (SelectedModel != null) total += SelectedModel.BasePrice;
            if (SelectedEngine != null) total += SelectedEngine.PriceModifier;
            if (SelectedColor != null) total += SelectedColor.PriceModifier;

            total += AvailableOptions.Where(o => o.IsSelected).Sum(o => o.Price);

            return total;
        }

        public decimal CalculateMonthlyLoanPayment()
        {
            const decimal InterestRateYear = 0.15m;
            decimal price = GetTotalPrice();

            if (price <= 0 || LoanTermMonths == 0) return 0m;

            decimal initialPaymentAmount = price * (InitialPaymentPercent / 100m);
            decimal loanAmount = price - initialPaymentAmount;

            decimal termYears = (decimal)LoanTermMonths / 12m;

            decimal totalInterest = loanAmount * InterestRateYear * termYears;

            decimal monthlyPayment = (loanAmount + totalInterest) / LoanTermMonths;

            return monthlyPayment;
        }
    }

    public class CarModel
    {
        public string Name { get; set; }
        public decimal BasePrice { get; set; }
        public override string ToString() => Name;
    }

    public class Engine
    {
        public string Type { get; set; }
        public decimal PriceModifier { get; set; }
        public override string ToString() => Type;
    }

    public class CarColor
    {
        public string Name { get; set; }
        public string HexCode { get; set; }
        public decimal PriceModifier { get; set; }
        public override string ToString() => Name;
    }

    public class CarOption : INotifyPropertyChanged
    {
        private bool _isSelected;
        public string Name { get; set; }
        public decimal Price { get; set; }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public static class AppData
    {
        public static Core CurrentConfig { get; set; } = new Core();
    }
}