using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure_game
{
   public class Entity
    {
        public string Name { get; private set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int maxHealth { get; set; }
        public int Exp { get; set; }
        public int Level { get; set; }
        public int Speed { get; set; }
        public float CriticalHitChance { get; set; } // in percentage  
        public float CriticalHitMultiplier { get; set; } // e.g. 1.5 for 50% extra damage 
        public int ArmourValue { get; set; } // for tank enemies

        public Entity(string name, int health, int attack, int Maxhealth, int exp, int level, int speed)
        {
            Name = name;
            Health = health;
            Attack = attack;
            maxHealth = Maxhealth;
            Exp = exp;
            Level = level;
            Speed = speed;
        }

        public void SetName(string newName)
        {
            Name = newName;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
        }

        public void SetAttack(int attackValue)
        {
            Attack = attackValue;
        }
    }
}
 