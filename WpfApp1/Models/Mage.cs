using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Mage : Enemy
    {
        private static readonly Random _rng = new Random();

        public int FreezeChance { get; protected set; } = 15;

        public Mage() : base("Маг", 25, 15, 2, "Assets/mage.png")
        {
        }

        public override int CalculateDamage(Player player)
        {
            int dmg = base.CalculateDamage(player);
            if (_rng.Next(100) < FreezeChance)
            {   
                player.IsFrozen = true;
            }

            return dmg;
        }
    }

}
