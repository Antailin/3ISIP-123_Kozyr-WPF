using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Potion : Item
    {
        public Potion() : base("Зелье лечения", "Полностью восстанавливает HP", "Assets/potion.png")
        {
        }
    }
}
