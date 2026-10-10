using System;
using System.CodeDom;
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
            uint result = 0;
            Console.Write(s);
            do
            {
                string ss = Console.ReadLine();
                uint.TryParse(ss, out result);
            } while (result < 0);
            return result;
        }
    }
    class Game
    {
        public Random random = new Random();
        private Player player = Player.GetInstance();

        public void Battle() { }

        public void Start() 
        {
            
        }
    }
    public abstract class Entity
    {
        public decimal maxHealth;
        public decimal health;
        public decimal damage;
        public decimal armorPercent;
        public decimal critChance;
        public bool isFrosen = false;
        public abstract void Attack(Entity kogo, Random rand);
        public Entity(decimal h, decimal d, decimal ap, decimal cc) 
        {
            maxHealth = h; health = h; damage = d; armorPercent = ap; critChance = cc;
        }

    }
    public class Player : Entity
    {
        private Player(decimal h, decimal d, decimal ap, decimal cc) : base(h, d, ap, cc) {
            this.EquipWeapon(new Weapon("Плевок", 2, 0.01m));
            this.EquipEquipment(new Equipment("Голый", -0.05m));
        }
        private static Player _instance = new Player(100, 0, 0, 0);
        public static Player GetInstance() { return _instance; }
        public Weapon weapon;
        public Equipment equip;
        public bool isDefencing;
        public override void Attack(Entity kogo, Random rand)
        {
            if (rand.Next(1, 101) > 5)
            {
                kogo.health -= damage * (1 - kogo.armorPercent);
            }
        }
        public void Defense(Random rand) {
            this.isDefencing = true;
        }
        public bool EquipWeapon(Weapon we)
        {
            if (this.weapon != null)
            {
                Console.WriteLine($"Новое оружие: {we.name}, его урон: {we.damage}, крит.шанс: {we.critChance}.");
                uint action = Checker.check("Equip? 0/1");
                if (action == 1)
                {
                    this.weapon = we;
                    this.damage = we.damage;
                    this.critChance = we.critChance;
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
            this.equip = eq;
            this.armorPercent = eq.armorPercent;
            return true;
        }
        public bool isAlive()
        {
            return health > 0;
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
        public Equipment(string n, decimal a)
        {
            name = n; armorPercent = a;
        }
    }

    public class EnemyFabric { }
    public abstract class Enemy : Entity
    {
        public decimal freezeChance;
        public string name;
        public bool ignoreArmour;
        public Enemy(decimal h, decimal d, decimal ap, decimal cc, string n, bool ia, decimal fc = 0) : base(h, d, ap, cc) {
            name =n; ignoreArmour = ia; freezeChance = fc;
        }
        public override void Attack(Entity kogo, Random rand) 
        {
            if (rand.Next(1, 101) > 5)
            {
                kogo.health -= damage * (1 - kogo.armorPercent);
            }
        }
    }

public class Goblin : Enemy
    {
        public Goblin() : base(50, 10, 0.05m, 0.1m, "Гоблин", false) { }
    }
    public class Skeleton : Enemy
    {
        public Skeleton() : base(40, 8, 0.03m, 0, "Скелет", true) { }
    }
    public class Mage : Enemy
    {
        public Mage() : base(30, 12, 0.02m, 0, "Маг", false, 0.15m) { }
    }
    public class Gorlanov : Goblin
    {
        public Gorlanov() : base()
        {
            health *= 2;
            damage *= 1.5m;
            armorPercent *= 1.2m;
            critChance += 0.1m;
            name = "ВВГ";
        }
    }
    public class Kovalskii : Skeleton
    {
        public Kovalskii() : base()
        {
            health *= 2.5m;
            damage *= 1.3m;
            armorPercent *= 1.4m;
            name = "Ковальский";
        }
    }
    public class Archmage : Mage
    {
        public Archmage() : base()
        {
            health *= 1.8m;
            damage *= 1.6m;
            armorPercent *= 1.1m;
            name = "Архимаг C++";
            freezeChance += 0.1m;
        }
    }
    public class Pestov : Skeleton
    {
        public Pestov() : base()
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
