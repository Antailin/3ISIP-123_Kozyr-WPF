using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Player
    {
        public int MaxHP { get; private set; }
        private int _hp;
        public int HP
        { 
            get => _hp;
            private set
            {
                if (value < 0) _hp = 0;
                else if (value > MaxHP) _hp = MaxHP;
                else _hp = value;
            }

        }
        public bool IsAlive => _hp > 0;
        public Weapon Weapon { get; private set; }
        public Armor Armor { get; private set; }

        public int AttackPower => Weapon != null ? Weapon.AttackBonus : 5;
        public int DefensePower => Armor != null ? Armor.DefenseBonus : 0;

        public bool IsFrozen = false;
        public bool IsDefending = false;

        public Player(int maxHp = 100)
        {
            MaxHP = maxHp;
            HP = maxHp;
        }

        public void TakeDamage(int amount)
        {
            if (amount < 0) return;
            HP -= amount;
        }
        public void Heal(int amount)
        {
            HP += amount;
        }

        public void FullHeal() => HP = MaxHP;

        public Weapon EquipWeapon(Weapon newWeapon)
        {
            var old = Weapon;
            Weapon = newWeapon;
            return old;
        }

        public Armor EquipArmor(Armor newArmor)
        { 
            var old = Armor;
            Armor = newArmor;
            return old;
        }
        public void ResetTurnFlags()
        {
            IsDefending = false;
            IsFrozen = false;
        }
    }
}
