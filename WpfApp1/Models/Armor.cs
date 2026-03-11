using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Armor : Item
    {
        public int DefenseBonus { get; }

        public Armor(string name, int defenseBonus, string imagePath) : base(name, $"бонус к защите - {defenseBonus}", imagePath)
        {
            DefenseBonus = defenseBonus;
        }
    }
}
