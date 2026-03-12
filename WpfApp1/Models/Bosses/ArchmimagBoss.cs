using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models.Bosses
{
    public class ArchmimagBoss : Mage
    {
        public ArchmimagBoss() : base()
        {
            Name = "Архимаг C++";
            ImagePath = "";
            HP = (int)(HP * 1.8);
            MaxHP = HP;
            AttackDMG = (int)(AttackDMG * 1.6);
            Defense = (int)(Defense * 1.1);
            FreezeChance += 10;
        }
    }
}
