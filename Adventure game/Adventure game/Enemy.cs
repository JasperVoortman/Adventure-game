using System;

namespace Adventure_game
{
    public class Enemy : Entity
    {


        public Enemy(string name, int health, int attack, int Maxhealth, int exp, int level, int speed) : base(name, health, attack, Maxhealth, exp, level, speed)
        {
        }

}
}