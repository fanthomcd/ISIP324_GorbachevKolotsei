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
    class Game
    {
        Random random = new Random();
        private Player player = Player.GetInstance();
    }
    public abstract class Entity
    {
        public decimal health;
        public decimal damage;
        public decimal armorPercent;
        public abstract void Attack(Entity kogo);
        

    }
    public class Player : Entity
    {
        private Player() { }
        private static Player _instance = new Player();
        public static Player GetInstance() { return _instance; }
        public Weapon weapon;
        public Equipment equip;
        public override void Attack(Entity kogo)
        {
            
        }
        public void Defense() { }

    }
    public class Weapon { }
    public class Equipment { }

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
