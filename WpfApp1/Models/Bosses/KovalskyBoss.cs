using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models.Bosses
{
    public class KovalskyBoss : Skeleton
    {
        public KovalskyBoss() : base()
        {
            Name = "Ковальский";
            ImagePath = "Assets/boss_kovalsky.png";
            HP = (int)(HP * 2.5);
            MaxHP = HP;
            AttackDMG = (int)(AttackDMG * 1.3);
            Defense = (int)(Defense * 1.4);
        }
    }
}
