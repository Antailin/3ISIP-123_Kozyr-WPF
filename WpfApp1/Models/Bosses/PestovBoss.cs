using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models.Bosses
{
    public class PestovBoss : Skeleton
    {
        private static readonly Random _rnd = new Random();
        private int _freezeChance = 15;

        public PestovBoss() : base()
        {
            Name = "";
            ImagePath = "";
            HP = (int)(HP * 1.3);
            MaxHP = HP;
            AttackDMG = (int)(AttackDMG * 1.8);
            Defense = (int)(Defense * 0.6);
        }
        public override int CalculateDamage(Player player)
        {
            int dmg = base.CalculateDamage(player);

            if (_rnd.Next(100) < _freezeChance)
                player.IsFrozen = true;

            return dmg;
        }

    }   
}
