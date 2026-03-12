using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.Services
{
    public class LootGenerator
    {
        private static readonly Random _rnd = new Random();

        private static readonly List<Weapon> _weapons = new List<Weapon>
        {
            new Weapon("Ржавый кинжал",  3,  "Assets/dagger.png"),
            new Weapon("Длинный меч",    8,  "Assets/sword.png"),
            new Weapon("Боевой топор",   12, "Assets/axe.png"),
            new Weapon("Магический жезл",10, "Assets/staff.png"),
        };

        private static readonly List<Armor> _armors = new List<Armor>
        {
            new Armor("Кожаная куртка", 3,  "Assets/leather.png"),
            new Armor("Кольчуга",       7,  "Assets/chainmail.png"),
            new Armor("Латный доспех",  12, "Assets/plate.png"),
        };

        public Item GeneratorLoot() 
        {
            int roll = _rnd.Next(3);
            switch (roll)
            {
                case 0: return GetRandomWeapon();
                case 1: return GetRandomArmor();
                case 2: return new Potion();
                default:return null;
            }
        }
        private Weapon GetRandomWeapon()
        {
            int index = _rnd.Next(_weapons.Count);
            return _weapons[index];
        }

        private Armor GetRandomArmor()
        {
            int index = _rnd.Next(_armors.Count);
            return _armors[index];
        }
    }
}
