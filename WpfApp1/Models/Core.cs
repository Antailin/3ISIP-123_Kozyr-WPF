using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Core
    {
        public CarModel Model { get; set; }
        public Engine Engine { get; set; }
        public CarColor Color { get; set; }
        public decimal ColorPrice { get; set; }
        public List<CarOption> Options { get; set; } = new List<CarOption>();
        public List<CarOption> AvailableOptions { get; set; } = new List<CarOption>();

        public double InitialPaymentPercent { get; set; } = 20; 
        public int LoanTermMonths { get; set; } = 24; 

    
        public string ClientName { get; set; }
        public string ClientPhone { get; set; }
        public string ClientEmail { get; set; }


        public decimal GetTotalPrice()
        {
            decimal total = 0;
            if (Model != null) total += Model.BasePrice;
            if (Engine != null) total += Engine.PriceModifier;
            total += ColorPrice;

            foreach (var opt in Options)
            {
                if (opt.IsSelected) total += opt.Price;
            }
            return total;
        }
    }
    public class CarModel
    {
        public string Name { get; set; }
        public decimal BasePrice { get; set; }
        public override string ToString() => $"{Name} - ({BasePrice} руб.)";
    }

    public class Engine
    {
        public string Type { get; set; } 
        public decimal PriceModifier { get; set; }
        public override string ToString() => $"{Type} (+{PriceModifier} руб.)";
    }

    public class CarOption
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsSelected { get; set; }
    }
    public class CarColor
    {
        public string Name { get; set; }
        public string HexCode { get; set; } 
        public decimal PriceModifier { get; set; }
        public override string ToString() => Name;
    }
    public static class AppData
    {
        public static Core CurrentConfig { get; set; } = new Core();
    }
}
