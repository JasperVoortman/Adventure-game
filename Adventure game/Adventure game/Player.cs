using System;

namespace Adventure_game
{
    public class Player : Entity
    {


        public Player(string name, int health, int attack, int Maxhealth, int exp, int level, int speed, float criticalhitchance, float criticalhitmultiplier) : base(name, health, attack, Maxhealth, exp, level, speed)
        {
            CriticalHitChance = criticalhitchance;
            CriticalHitMultiplier = criticalhitmultiplier;
        }

    }
}