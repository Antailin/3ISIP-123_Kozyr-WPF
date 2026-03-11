using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Skeleton : Enemy
    {
        public Skeleton() : base("Скелет", 40, 10, 5, "Assets/skeleton.png")
        {
        }

        public override int CalculateDamage(Player player)
        {
            return AttackDMG;
        }
    }
}
