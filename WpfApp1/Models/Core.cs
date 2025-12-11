using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Core
    {
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
    public static class AppData
    {
        public static Core CurrentConfig { get; set; } = new Core();
    }
}
