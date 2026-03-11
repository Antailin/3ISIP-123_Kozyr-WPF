using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Weapon : Item
    {
        public int AttackBonus { get; }

        public Weapon(string name, int attackBonus, string imagePath) : base(name, $"бонус к атаке - {attackBonus}", imagePath)
        {
            AttackBonus = attackBonus;
        }
    }
}
