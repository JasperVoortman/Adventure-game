using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Adventure_game
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteThis("Welcome to the Adventure Game! ");
            WriteThis("You are in a dark room in a mysterious dungeon tied up. ");
            WriteThis("You can't remember how you got here. ");
            WriteThis("You are a monster hunter, but you lost your memories.");
            WriteThis("The goal of this game is that you will eventually regain your memories by completing quests and escape the dungeon.");
            WriteThis("You look around and see a door in front of you. ");
            WriteThis("You hear a voice in your head that says: ");
            WriteThis("Hello, brave warrior. You are in the lair of the monster. ", ConsoleColor.Magenta);
            WriteThis("You must defeat the monster to escape the dungeon. ", ConsoleColor.Magenta);
            WriteThis("What is your name? ");
            WriteThis("Please enter your name: ", ConsoleColor.Cyan, false);
            string playerName = Console.ReadLine() ?? string.Empty;
            Player player = new Player(playerName, 125, 15, 125, 0, 1, 1, 30, 1.5f); 
            WriteThis($"\nWelcome, {player.Name}! ");
            WriteThis("Are you ready to begin your adventure? ");

            Random random = new();

            Enemy enemy = new Enemy("Goblin", 40, 8, 40, 0, 1, 1);
            WriteThis($"\nA wild {enemy.Name} appears with {enemy.Health} health, {enemy.Attack} attack and {enemy.Speed} speed!", ConsoleColor.Red);

            // maakt een lijst van alle enemies en je kan ze allemaal een andere weigt meegeven hoe hoger de weight hoe meer kans dat hij appeart
            var weightedEnemyDefinitions = new List<(string Name, Type EnemyClass, int Weight)>
                    {
                        ("Goblin", typeof(Enemy), 70),
                        ("Orc", typeof(TankEnemy), 40),
                        ("Troll", typeof(Enemy), 20),
                        ("Dragon", typeof(BlitzEnemy), 10),
                        ("Kayan", typeof(BlitzEnemy), 5),
                        ("Shedion", typeof(TankEnemy), 3)
                    };

            // gevecht
            while (player.Health > 0)
            {
                if (enemy.Health <= 0)
                {
                 
                    int totalWeight = weightedEnemyDefinitions.Sum(e => e.Weight); // telt alle weights bij elkaar op voor de totalweight

                    // random nummer tussen de total weight en 0
                    int randomWeight = random.Next(0, totalWeight);
                    int levelMulti = player.Level * 2;
                    randomWeight += levelMulti; // verhoogt de randomweight met de level multiplier
                    if (randomWeight > totalWeight)
                    {
                        randomWeight = totalWeight; // zorgt ervoor dat de randomweight niet hoger is dan de total weight
                    }



                    // Selecteerd een enenmy gebaseerd op de weights

                    //•	totalWeight = 148
                    //•	randomWeight = 135
                    //2.Iteration:
                    //•	Start with cumulativeWeight = 0.
                    //•	Add "Goblin" weight: cumulativeWeight = 70. 135 < 70 ? No.
                    //•	Add "Orc" weight: cumulativeWeight = 110. 135 < 110 ? No.
                    //•	Add "Troll" weight: cumulativeWeight = 130. 135 < 130 ? No.
                    //•	Add "Dragon" weight: cumulativeWeight = 140. 135 < 140 ? Yes.
                    //3.Result:
                    //•	"Dragon" is selected.
                    int cumulativeWeight = 0;
                    (string Name, Type EnemyClass, int Weight) selectedEnemy = default;

                    foreach (var enemyDef in weightedEnemyDefinitions)
                    {
                        cumulativeWeight += enemyDef.Weight;
                        if (randomWeight < cumulativeWeight)
                        {
                            selectedEnemy = enemyDef;
                            break;
                        }
                    }

                    

                    
                    int levelMultiplier = player.Level;

                    
                    var enemyAttributes = new Dictionary<string, (int MinDamage, int MaxDamage, int MinHealth, int MaxHealth, int MinSpeed, int MaxSpeed)>
                    {
                        { "Shedion", (50, 70, 200, 300, 10, 15) },
                        { "Kayan", (30, 50, 150, 200, 8, 12) },   
                        { "Dragon", (20, 40, 100, 150, 5, 10) },  
                        { "Troll", (15, 30, 80, 120, 4, 8) },    
                        { "Orc", (10, 25, 60, 100, 3, 7) },       
                        { "Goblin", (5, 15, 40, 80, 2, 6) }      
                    };

                   
                    if (enemyAttributes.TryGetValue(selectedEnemy.Name, out var attributes))//TryGetValue is een methode die probeert om de waarde van de key te krijgen van de dictionary
                    {
                        int randomEnemyDamage = random.Next(attributes.MinDamage, attributes.MaxDamage) + (levelMultiplier * 2);
                        int randomEnemyHealth = random.Next(attributes.MinHealth, attributes.MaxHealth) + (levelMultiplier * 10);
                        int randomMaxHealth = randomEnemyHealth;
                        int randomSpeed = random.Next(attributes.MinSpeed, attributes.MaxSpeed) + levelMultiplier;

                        
                        int enemyTypeChance = random.Next(0, 3); // 0 = Normal, 1 = Blitz, 2 = Tank

                        if (enemyTypeChance == 1) // BlitzEnemy
                        {
                            enemy = new BlitzEnemy(selectedEnemy.Name, randomEnemyHealth, randomEnemyDamage, randomMaxHealth, 0, 1, randomSpeed);
                            WriteThis($"A wild {enemy.Name} appears! It's a fast attacker! It has {enemy.Health} health, {enemy.Attack} attack, and {enemy.Speed} speed.", ConsoleColor.Red);
                        }
                        else if (enemyTypeChance == 2) // TankEnemy
                        {
                            int randomArmor = random.Next(1, 10) + (levelMultiplier * 2); // Increase armor with level
                            enemy = new TankEnemy(selectedEnemy.Name, randomEnemyHealth, randomEnemyDamage, randomMaxHealth, 0, 1, randomSpeed, randomArmor);
                            WriteThis($"A wild {enemy.Name} appears! It's a heavily armored attacker! It has {enemy.Health} health, {enemy.Attack} attack, and {enemy.Speed} speed.", ConsoleColor.Red);
                        }
                        else // Normal Enemy
                        {
                            enemy = new Enemy(selectedEnemy.Name, randomEnemyHealth, randomEnemyDamage, randomMaxHealth, 0, 1, randomSpeed);
                            WriteThis($"A wild {enemy.Name} appears! It has {enemy.Health} health, {enemy.Attack} attack, and {enemy.Speed} speed.", ConsoleColor.Red);
                        }
                    }
                }


                WriteThis("\nChoose an action: (A)ttack, (R)un or do (N)othing", ConsoleColor.Cyan, false);
                string action = Console.ReadLine().ToUpper();

                if (action == "A")
                {
                    
                    bool isCriticalHit = random.NextDouble() < (player.CriticalHitChance / 100);
                    int playerDamage = isCriticalHit ? (int)(player.Attack * player.CriticalHitMultiplier) : player.Attack;

                    if (isCriticalHit)
                    {
                        WriteThis($"Critical hit! You attack the {enemy.Name} for {playerDamage} damage. It has {enemy.Health} health left", ConsoleColor.Yellow);
                    }
                    else
                    {
                        WriteThis($"You attack the {enemy.Name} for {playerDamage} damage. It has {enemy.Health} health left", ConsoleColor.Green);
                    }

                    
                    if (enemy is TankEnemy tankEnemy)
                    {
                        int reducedDamage = playerDamage - tankEnemy.ArmourValue;
                        tankEnemy.TakeDamage(reducedDamage);
                        WriteThis($"The {enemy.Name} reduces your attack by {tankEnemy.ArmourValue}. It takes {reducedDamage} damage.", ConsoleColor.DarkCyan);
                    }
                    else
                    {
                        enemy.TakeDamage(playerDamage);
                    }

                    if (enemy.Health < 0)
                    {
                        enemy.Health = 0;
                    }

                    
                    if (enemy.Health > 0)
                    {
                        if (enemy is BlitzEnemy blitzEnemy)
                        {
                            // BlitzEnemy attackt met een kans van 80% en halveert de kans elke keer dat hij aanvalt
                            double blitzChance = 0.8; // Start with 80% chance for the first attack
                            do
                            {
                                int blitzDamage = random.Next(enemy.Attack - 3, enemy.Attack + 3);
                                player.TakeDamage(blitzDamage);
                                WriteThis($"The {enemy.Name} blitz attacks you for {blitzDamage} damage! You have {player.Health} health left.", ConsoleColor.Red);

                                blitzChance *= 0.5; // halveerd de kans elke keer dat hij aanvalt
                            } while (random.NextDouble() < blitzChance && player.Health > 0);
                        }
                        else
                        {
                            
                            int enemyAttack = random.Next(enemy.Attack - 3, enemy.Attack + 3);
                            player.TakeDamage(enemyAttack);
                            WriteThis($"The {enemy.Name} attacks you for {enemyAttack} damage. You have {player.Health} health left.", ConsoleColor.Red);
                        }
                    }
                }
                else if (action == "R")
                {
                    if (enemy.Speed > player.Speed)
                    {
                        WriteThis("You can't run away from the battle! The enemy is faster than you.", ConsoleColor.DarkCyan);
                    }
                    else
                    {
                        WriteThis("You ran away from the battle!", ConsoleColor.Yellow);
                        break;
                    }
                }
                else if (action == "N")
                {
                    player.TakeDamage(enemy.Attack);
                    WriteThis($"You did nothing. The {enemy.Name} attacks you for {enemy.Attack} damage. You have {player.Health} health left.", ConsoleColor.Red);
                }
                else
                {
                    WriteThis("Invalid action. Please choose (A)ttack, (R)un, or (N)othing.", ConsoleColor.Red);
                }

                if (player.Health <= 0)
                {
                    WriteThis("You have been defeated. Game Over.", ConsoleColor.Red);
                }
                else if (enemy.Health <= 0)
                {
                    WriteThis($"You have defeated the {enemy.Name}!", ConsoleColor.Green);

                   
                    var experienceRanges = new Dictionary<string, (int Min, int Max)>
                    {
                        { "Shedion", (50, 100) },
                        { "Kayan", (30, 70) },
                        { "Dragon", (20, 50) },
                        { "Troll", (10, 30) },
                        { "Orc", (5, 20) },
                        { "Goblin", (1, 10) }
                    };

                    
                    if (experienceRanges.TryGetValue(enemy.Name, out var range))// hiermee haalt hij de experience range op van de enemy die is verslagen
                    {
                        int expGained = random.Next(range.Min, range.Max);
                        player.Exp += expGained;
                        WriteThis($"You have gained {expGained} experience!", ConsoleColor.Cyan);
                    }

                    int healthGain = random.Next(10, 30);
                    player.Health += healthGain;
                    WriteThis($"You have healed {healthGain} health by defeating the {enemy.Name}\n", ConsoleColor.Green);

                    int experienceToLevelUp = 10 * player.Level; 

                    if (player.Exp >= experienceToLevelUp)
                    {
                        player.Level++;
                        player.Exp -= experienceToLevelUp; 
                        player.maxHealth += 10;
                        player.Attack += random.Next(1, 9);
                        player.Health = player.maxHealth;

                        WriteThis($"You have leveled up! You are now level {player.Level}.", ConsoleColor.Cyan);
                        WriteThis($"Your max health is now {player.maxHealth}, and your attack has increased to {player.Attack}.", ConsoleColor.Cyan);
                    }

                    if (player.Health > player.maxHealth)
                    {
                        player.Health = player.maxHealth;
                    }
                    WriteThis($"You now have {player.Health} health and {player.Attack} attack.", ConsoleColor.Green);
                    WriteThis($"You are level {player.Level} with {player.Exp}/{experienceToLevelUp} experience.", ConsoleColor.Cyan);
                }
            }
        }

        public static void WriteThis(string text, ConsoleColor kleur = ConsoleColor.White, bool useWriteLine = true, int delay = 1)
        {
            Console.ForegroundColor = kleur;
            if (useWriteLine)
            {
                foreach (char c in text)
                {
                    Console.Write(c);
                    System.Threading.Thread.Sleep(delay);
                }
                Console.WriteLine();
            }
            else
            {
                foreach (char c in text)
                {
                    Console.Write(c);
                    System.Threading.Thread.Sleep(delay);
                }
            }
            Console.ResetColor();
        }
    }
}
