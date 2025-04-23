using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure_game
{
    public class TankEnemy : Enemy
    {
        public TankEnemy(string name, int health, int attack, int Maxhealth, int exp, int level, int speed, int armourvalue) : base(name, health, attack, Maxhealth, exp, level, speed)
        {
            ArmourValue = armourvalue;

        }
    }
}
