using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Pizza
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }

        public override string ToString() => $"{Name} - {BasePrice}";
    }

    public class Topping
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public override string ToString() => $"{Name} (+{Price})";
    }

    public class Order
    {
        public Pizza SelectedPizza { get; set; }
        public double SizeMultiplier { get; set; } = 1.0; 
        public string SizeName { get; set; } = "Маленькая";
        public List<Topping> Toppings { get; set; } = new List<Topping>();

        public decimal TotalPrice
        {
            get
            {
                decimal pizzaPrice = SelectedPizza.BasePrice * (decimal)SizeMultiplier;
                decimal toppingsPrice = 0;
                foreach (var t in Toppings) toppingsPrice += t.Price;
                return pizzaPrice + toppingsPrice;
            }
        }
    }
}

