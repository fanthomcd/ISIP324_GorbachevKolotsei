using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using static ISIP324_GorbachevKolotsei.Program;

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
    }
    public abstract class Entity
    {
        public decimal maxHealth;
        public decimal health;
        public decimal damage;
        public decimal armorPercent;
        public int critChance;
        public bool isFrosen;
        public abstract void Attack(Entity kogo, Random rand);
        

    }
    public class Player : Entity
    {
        private Player() {
            health = 100; maxHealth = 100;
            this.EquipWeapon(new Weapon("Плевок", 2, 0.01m));
            this.EquipEquipment(new Equipment("Голый", -0.05m));
        }
        private static Player _instance = new Player();
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
            if (this.weapon != null) {
                Console.WriteLine($"Новое оружие: {we.Name}, его урон: {we.damage}, крит.шанс: {we.critChance}.");
                uint action = Checker.check("Equip? 0/1");
            this.weapon = we;
            this.damage = we.damage;
            this.critChance = we.critChance;
            return true;
        }
        public bool EquipEquipment(Equipment eq)
        {
            this.equip = eq;
            this.armorPercent = eq.armorPercent;
            return true;
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
    public abstract class Enemy : Entity{ }
    public class Goblin : Enemy { }
    public class Skeleton : Enemy { }
    public class Mage : Enemy { }
    public class Gorlanov : Goblin { }
    public class Kovalskii : Skeleton { }
    public class Archmage : Mage { }
    public class Pestov : Skeleton { }
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
        }
    }
}
