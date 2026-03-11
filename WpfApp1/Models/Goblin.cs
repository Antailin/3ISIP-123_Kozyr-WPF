using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Goblin : Enemy
    {
        private static readonly Random _rnd = new Random();

        public int CritChance { get; protected set; } = 20;

        public Goblin() : base("Гоблин", 30, 12, 3, "Assets/goblin.png") 
        {

        }
        public override int CalculateDamage(Player player)
        {
            int baseDmg = base.CalculateDamage(player);
            bool isCrit = _rnd.Next(100) < CritChance;
            return isCrit ? baseDmg * 2 : baseDmg;
        }

    }
}
