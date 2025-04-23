using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Adventure_game
{
    public class BlitzEnemy : Enemy
    {
        public BlitzEnemy(string name, int health, int attack, int Maxhealth, int exp, int level, int speed) : base(name, health, attack, Maxhealth, exp, level, speed)
        {
        }
        public void BlitzAttack(Entity target)
        {

            Random random = new Random();
            double chance = 0.8;
            while (random.NextDouble() < chance)
            {
                target.TakeDamage(Attack);
                Console.WriteLine($"{Name} attacks {target.Name} for {Attack} damage!");

                chance *= 0.8; // Decrease chance by 20% each time



            }
            if (target.Health <= 0)
            {
                Console.WriteLine($"{target.Name} has been defeated!");
            }
            else
            {
                Console.WriteLine($"{target.Name} has {target.Health} health remaining.");
            }
            chance = 0.8; // Reset chance for the next attack

        }
    }
}