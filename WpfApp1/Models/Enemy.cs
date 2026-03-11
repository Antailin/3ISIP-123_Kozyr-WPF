using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public abstract class Enemy
    {
        public string Name { get; protected set; }
        public int HP { get; protected set; }
        public int AttackDMG { get; protected set; }
        public int Defense { get; protected set; }
        public string ImagePath { get; protected set; }

        public bool IsAlive => HP > 0;

        protected Enemy(string name, int hP, int attackDMG, int defense, string imagePath)
        {
            Name = name;
            HP = hP;
            AttackDMG = attackDMG;
            Defense = defense;
            ImagePath = imagePath;
        }

        public virtual int ClaculateDamage(Player player)
        {
            return Math.Max(1, AttackDMG - player.DefensePower);
        }
    }
}
