using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;
using WpfApp1.Models.Bosses;

namespace WpfApp1.Services
{
    public class EventGenerator
    {
        private static readonly Random _rnd = new Random();

        public List<Enemy> GenerateEnemies()
        {
            int count = _rnd.Next(1, 4);
            var enemies = new List<Enemy>();

            for (int i = 0; i < count; i++)
            {
                enemies.Add(CreateRandomEnemy());
            }
            return enemies;
        }
        private Enemy CreateRandomEnemy()
        {
            int roll = _rnd.Next(3);
            switch (roll)
            {
                case 0: return new Goblin();
                case 1: return new Skeleton();
                case 2: return new Mage();
                default : return null;
            }
        }
        public Enemy GenerateBoss()
        {
            int roll = _rnd.Next(4);
            switch (roll)
            {
                case 0: return new VVGBoss();
                case 1: return new KovalskyBoss();
                case 2: return new ArchmimagBoss();
                case 3: return new PestovBoss();
                default: return null;
            }
        }

        public bool IsEnemyEvent() => _rnd.Next(2) == 0; 
    }

}
