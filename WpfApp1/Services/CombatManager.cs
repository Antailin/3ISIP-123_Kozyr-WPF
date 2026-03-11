using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.Services
{
    public class CombatManager
    {
        private static readonly Random _rnd = new Random();

        public (int damage, string log) EnemyAttackPlayer (Enemy enemy, Player player) 
        { 
            if (player.IsDefending && _rnd.Next(100) < 40)
            {
                return (0, $"{enemy.Name} атакует — {player} уклоняется!");
            }
            int rawDamage = enemy.CalculateDamage(player);
            int finalDamage = rawDamage;

            if (player.IsDefending)
            {
                double blockPercent = 0.70 + _rnd.NextDouble() * 0.30;
                int blocked = (int)(player.DefensePower * blockPercent);
                finalDamage = Math.Max(0, rawDamage - blocked);
            }
            player.TakeDamage(finalDamage);
            string log = $"{enemy.Name} наносит {finalDamage} урона. HP: {player.HP}";
            return (finalDamage, log);
        }

        public (int damage, string log) PlayerAttackEnemy(Player player, Enemy enemy)
        {
            int dmg = Math.Max(1, player.AttackPower - enemy.Defense);
            enemy.HP -= dmg;
            string log = $"Вы атакуете {enemy.Name} на {dmg} урона. HP врага: {Math.Max(0, enemy.HP)}";
            return (dmg, log);
        }

    }
}
    