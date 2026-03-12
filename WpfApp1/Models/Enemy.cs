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
        public string Name { get;  set; }
        public int HP { get;  set; }
        public int MaxHP { get; set; }
        public int AttackDMG { get;  set; }
        public int Defense { get; set; }
        public string ImagePath { get; set; }

        public bool IsAlive => HP > 0;

        protected Enemy(string name, int hp, int attackDMG, int defense, string imagePath)
        {
            Name = name;
            HP = hp;
            MaxHP = hp; 
            AttackDMG = attackDMG;
            Defense = defense;
            ImagePath = imagePath;
        }

        public virtual int CalculateDamage(Player player)
        {
            return Math.Max(1, AttackDMG - player.DefensePower);
        }
    }
}
