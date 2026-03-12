using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models.Bosses
{
    public class VVGBoss : Goblin
    {
        public VVGBoss() : base()
        {
            Name = "ВВГ";
            ImagePath = "Assets/boss_vvg.png";
            HP = (int)(HP * 2);
            MaxHP = HP;
            AttackDMG = (int)(AttackDMG * 1.5);
            Defense = (int)(Defense * 1.2);
            CritChance += 10;
        }
    }
}
