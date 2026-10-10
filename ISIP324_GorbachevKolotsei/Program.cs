using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using static ISIP324_GorbachevKolotsei.Program;
using static System.Net.Mime.MediaTypeNames;

namespace ISIP324_GorbachevKolotsei
{
    public static class Checker {
        public static uint check(string s)
        {
            uint result;
            Console.Write(s);
            while (!uint.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine("Это не число! Попробуйте еще раз.");
                Console.Write(s);
            }

            return result;
        }

    }
    class Game
    {
        List<Weapon> weapons = new List<Weapon>()
            {
            new Weapon("Пукалка", 2, 0.1m),
            new Weapon("Волына", 15, 0.66m),
            new Weapon("Палочка выручалочка", 5, 0.77m),
            new Weapon("Мороженое 48 копеек", 48, 0.048m),
            new Weapon("RTX 5090", 50, 0.90m),
            new Weapon("Святая мать", 1, 1m),
            new Weapon("уярик", 40, 0.4m),
            new Weapon("Хомяк", 66, 0.96m),
            new Weapon("Ручка", 2, 0.3m),
            new Weapon("Топор Рыкарей", 33, 0.33m),
            new Weapon("Пёся", 1000, 1m),
            };

        List<Equipment> equipment = new List<Equipment>()
            {
            new Equipment("Обычная броня", 0.05m, 1),
            new Equipment("Коcоворотка", 0.15m, 2),
            new Equipment("Железная дева", 0.1m, 5),
            new Equipment("Стальные яйца", 0.88m, 0.88m),
            new Equipment("Мифриловая броня", 0.2m, 10),
            new Equipment("Драконья броня", 0.3m, 20),
            new Equipment("Броня из попы тролля", 0.4m, 5),
            new Equipment("Броня из пёси", 0.77m, 10),
            new Equipment("БДСМ-Костюм", -0.25m, 60),
            new Equipment("Фурсьют", -1m, -50),
            };
        public Random random = new Random();
        private Player player = Player.GetInstance();
        private int totalSteps = 0;

        public void Battle(int steps) 
        {
            Enemy e = steps % 10 != 0 ? EnemyFabric.createEnemy(random, steps) : EnemyFabric.createBoss(random, steps);
            bool playerTurn = true;
            Console.WriteLine($"Встретили врага {e.name}.");
            while (player.isAlive() && e.isAlive())
            {
                Console.WriteLine("     HP:");
                Console.WriteLine($"Enemy HP: {e.health}/{e.maxHealth}");
                Console.WriteLine($"Player HP: {player.health}/{player.maxHealth}");
                if (player.isFrosen)
                {
                    playerTurn = false;
                    Console.WriteLine("Подморозили вас конешно");
                    player.isFrosen = false;
                    player.isDefencing = false;
                    e.Attack(player, random);
                }
                else
                {
                    if (playerTurn)
                    {
                        uint action;
                        do {
                            action = Checker.check("Чд кд? 0 - attack, 1 - defense ");
                        } while (action > 2);
                        if (action == 0) { Console.WriteLine($"Player атакует {e.name}!"); player.Attack(e, random); }
                        if (action == 1) { player.Defense(); Console.WriteLine("Обороняемся"); }
                    }
                    if (!playerTurn)
                    {
                        Console.WriteLine($"{e.name} атакует Player!");
                        e.Attack(player, random);
                    }
                    playerTurn = !playerTurn;
                }
            }
            if (player.isAlive()) Console.WriteLine($"Хороший Player! Ты победил {e.name}!"); else Console.WriteLine($"Твоя смерть - {e.name}. Rest in peace, Player");
        }
        public void Chest() 
        {
            Console.WriteLine("Вы нашли сундук! Содержимое:");
            int sod = random.Next(0, 3);
            switch (sod)
            {
                case 0: player.Heal(); return;
                case 1: player.EquipWeapon(weapons[random.Next(0, weapons.Count)]); return;
                case 2: player.EquipEquipment(equipment[random.Next(0, equipment.Count)]); return;
            }
        }

        public void Start() 
        {
            while (player.isAlive())
            {
                totalSteps++;
                Console.WriteLine($"Ход №{totalSteps}");
                int e = random.Next(0,2);
                if (e == 0 && totalSteps % 10 != 0) Chest();
                if (e == 1 || totalSteps % 10 == 0) Battle(totalSteps);
            }
            Console.WriteLine($"You died. Total steps: {totalSteps}");
        }
    }
    public abstract class Entity
    {
        public decimal maxHealth;
        public decimal health;
        public decimal damage;
        public decimal armorPercent;
        public decimal armorAbsolute = 0;
        public decimal critChance;
        public bool isDefencing;
        public bool isFrosen = false;
        public abstract void Attack(Entity kogo, Random rand);
        public Entity(decimal h, decimal d, decimal ap, decimal cc) 
        {
            maxHealth = h; health = h; damage = d; armorPercent = ap; critChance = cc;
        }
        public bool isAlive()
        {
            return health > 0;
        }

    }
    public class Player : Entity
    {
        private Player(decimal h, decimal d, decimal ap, decimal cc) : base(h, d, ap, cc) {
            this.EquipWeapon(new Weapon("Плевок", 20, 0.01m));
            this.EquipEquipment(new Equipment("Голый", 0.5m, 1m));
        }
        private static Player _instance = new Player(100, 0, 0, 0);
        public static Player GetInstance() { return _instance; }
        public Weapon weapon;
        public Equipment equip;
        public override void Attack(Entity kogo, Random rand)
        {
            if (rand.Next(1, 101) > 5)
            {
                decimal dmg = damage * (1 - kogo.armorPercent);
                kogo.health -= dmg;
                Console.WriteLine($"И наносит {dmg} дамага!");
            } else { Console.WriteLine("Ты промахнулся дебил ха-ха"); }
        }
        public void Defense() {
            this.isDefencing = true;
        }
        public void Heal()
        {
            health = maxHealth;
            Console.WriteLine("Напились зелья. Снова максхп.");
        }
        public bool EquipWeapon(Weapon we)
        {
            if (this.weapon != null)
            {
                Console.WriteLine($"Новое оружие: {we.name}, его урон: {we.damage}, крит.шанс: {we.critChance}.");
                Console.WriteLine($"Текущее оружие: {weapon.name}, его урон: {weapon.damage}, крит.шанс: {weapon.critChance}.");
                uint action = Checker.check("Equip? 0/1  ");
                if (action == 1)
                {
                    this.weapon = we;
                    this.damage = we.damage;
                    this.critChance = we.critChance;
                    Console.WriteLine($"Equipped {weapon.name}!");
                    return true;
                } else
                {
                    Console.WriteLine("Не надел. Ну и ладно.");
                    return false;
                } 
            } else
            {
                this.weapon = we;
                this.damage = we.damage;
                this.critChance = we.critChance;
                return true;
            }
        }
        public bool EquipEquipment(Equipment eq)
        {
            if (this.equip != null)
            {
                Console.WriteLine($"Новое equipment: {eq.name}, процент брони: {eq.armorPercent}, абсолют брони: {eq.armorAbsolute}.");
                Console.WriteLine($"Текующее equipment: {equip.name}, процент брони: {equip.armorPercent}, абсолют брони: {equip.armorAbsolute}.");
                uint action = Checker.check("Equip? 0/1  ");
                if (action == 1)
                {
                    this.equip = eq;
                    this.armorPercent = eq.armorPercent;
                    this.armorAbsolute = eq.armorAbsolute;
                    Console.WriteLine($"Equipped {eq.name}!");
                    return true;
                }
                else
                {
                    Console.WriteLine("Не надел. Ну и ладно.");
                    return false;
                }
            }
            else
            {
                this.equip = eq;
                this.armorPercent = eq.armorPercent;
                this.armorAbsolute = eq.armorAbsolute;
                return true;
            }
        }

    }
    public class Weapon {
        public string name;
        public decimal damage;
        public decimal critChance;
        public Weapon(string n, decimal d, decimal c) {
            name = n; damage = d; critChance = c;
        }

    }
    public class Equipment
    {
        public string name;
        public decimal armorPercent;
        public decimal armorAbsolute;
        public Equipment(string n, decimal a, decimal aa)
        {
            name = n; armorPercent = a; armorAbsolute = aa;
        }
    }

    static public class EnemyFabric 
    {
        static public Enemy createEnemy(Random rand, int steps)
        {
            int etype = rand.Next(1, 4);
            decimal diff = 1 + steps/50m;
            switch (etype)
            {
                case 1: return new Goblin(rand.Next(40, 60), rand.Next(0, 15), 0.05m, 0.05m, "Гоблин", false);
                case 2: return new Skeleton(rand.Next(20, 50), rand.Next(0, 21), 0.05m, 0.00001m, "Скелет", true);
                case 3: return new Mage(rand.Next(10, 90), rand.Next(1, 33), -0.25m, 0.00000001m, "Маг", false);
                default: return new Gorlanov(rand.Next(50, 150), rand.Next(10, 20), 0.25m, 0.00000001m, "ВВГ", false);
            }
        }
        static public Enemy createBoss(Random rand, int steps)
        {
            int etype = rand.Next(1, 5);
            decimal diff = 1 + steps/50m;
            switch (etype)
            {
                case 1: return new Gorlanov(rand.Next(50, 150), rand.Next(10, 20), 0.25m, 0.00000001m, "ВВГ", false);
                case 2: return new Pestov(rand.Next(20, 50), rand.Next(0, 21), 0.05m, 0.00001m, "Пестов", true);
                case 3: return new Archmage(rand.Next(10, 90), rand.Next(1, 33), -0.25m, 0.00000001m, "Архимаг", false);
                case 4: return new Kovalskii(rand.Next(20, 50), rand.Next(0, 21), 0.05m, 0.00001m, "Ковальский", true);
                default: return new Gorlanov(999, 99, 0.25m, 0.00000001m, "VVG", false);
            }
        }
    }
    public abstract class Enemy : Entity
    {
        public decimal freezeChance;
        public string name;
        public bool ignoreArmour;
        public Enemy(decimal h, decimal d, decimal ap, decimal cc, string n, bool ia, decimal fc = -1) : base(h, d, ap, cc) {
            name =n; ignoreArmour = ia; freezeChance = fc;
        }
        public override void Attack(Entity kogo, Random rand) 
        {
            if (rand.Next(1, 101) > 5)
            {
                if (kogo.isDefencing && rand.Next(1, 101) < 40) { Console.WriteLine("Успешный блок!"); kogo.isDefencing = false; return; };
                decimal criting = Convert.ToDecimal(rand.Next(1, 101)) / 100m;
                bool crit = (criting < critChance);
                decimal dmg = damage * ((crit ? 1.75m : 1) - (ignoreArmour ? 0 : kogo.armorPercent)) * (kogo.isDefencing ? 1 - rand.Next(40, 70) / 100 : 1) - kogo.armorAbsolute;
                if (dmg < 0) { dmg = 0; }
                Console.WriteLine($"И наносит {dmg} дамага.");
                if (crit) Console.WriteLine($"Кританул! {critChance}"); 
                kogo.health -= dmg;
                if (rand.Next(1, 101)/100m < freezeChance) kogo.isFrosen = true;
            } else 
            {
                Console.WriteLine("Он промахнулся дебил ха-ха");
            }
        }
    }

public class Goblin : Enemy
    {
        public Goblin(decimal h, decimal d, decimal ap, decimal cc, string n, bool ia) : base(h, d, ap, cc, n, ia) { }
    }
    public class Skeleton : Enemy
    {
        public Skeleton(decimal h, decimal d, decimal ap, decimal cc, string n, bool ia) : base(h, d, ap, cc, n, ia) { }
    }
    public class Mage : Enemy
    {
        public Mage(decimal h, decimal d, decimal ap, decimal cc, string n, bool ia) : base(h, d, ap, cc, n, ia, 0.05m) { }
    }
    public class Gorlanov : Goblin
    {
        public Gorlanov(decimal h, decimal d, decimal ap, decimal cc, string n, bool ia) : base(h, d, ap, cc, n, ia)
        {
            health *= 2; maxHealth = health;
            damage *= 1.5m;
            armorPercent *= 1.2m;
            critChance += 0.1m;
            name = "ВВГ";
        }
    }
    public class Kovalskii : Skeleton
    {
        public Kovalskii(decimal h, decimal d, decimal ap, decimal cc, string n, bool ia) : base(h, d, ap, cc, n, ia)
        {
            health *= 2.5m; maxHealth = health;
            damage *= 1.3m;
            armorPercent *= 1.4m;
            name = "Ковальский";
        }
    }
    public class Archmage : Mage
    {
        public Archmage(decimal h, decimal d, decimal ap, decimal cc, string n, bool ia) : base(h, d, ap, cc, n, ia)
        {
            health *= 1.8m; maxHealth = health;
            damage *= 1.6m;
            armorPercent *= 1.1m;
            name = "Архимаг C++";
            freezeChance += 0.1m;
        }
    }
    public class Pestov : Skeleton
    {
        public Pestov(decimal h, decimal d, decimal ap, decimal cc, string n, bool ia) : base(h, d, ap, cc, n, ia)
        {
            health *= 1.3m;
            damage *= 1.8m;
            armorPercent *= 0.6m;
            freezeChance += 0.15m;
            name = "Пестов С--";
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }
}
