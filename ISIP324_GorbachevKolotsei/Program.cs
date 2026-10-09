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
    //Вам необходимо создать систему управления университетом.
    //Система должна позволять управлять информацией о студентах, преподавателях и курсах через консоль.
    class Person 
    {
        private static int nextID = 1;
        public int id;
        public string telephone;
        public string name;
        public string familya;
        public string otchestvo;
        public Person(string t, string n, string f, string o)
        {
            telephone = t;name = n; familya = f; otchestvo = o;
            id = nextID; nextID++;
        }
        public void Print()
        {
            Console.WriteLine($"{id}. {name} {familya} {otchestvo}. Pozvonite: {telephone};");
        }
        public abstract AddCourse();
    }
    class Course
    {
        string title;
        private static int nextCode = 0;
        int code;
        Teacher teacher;
        List<Student> students;
        public Course(string t, Teacher te)
        {
            title = t; teacher = te;
            code = nextCode; nextCode++;
        }
        public void AddStudent(Student student)
        {
            students.Add(student);
            student.courses.Add(code);
        }
        public void Print()
        {
            Console.Write($"{code}. Name of course: {title}. ");
            Console.WriteLine("Teacher: "); teacher.Print();
        }
        public void PrintStudents(Predicate<Student> p) 
        {
            Console.WriteLine("Studenti:");
            foreach (Student s in Students) if (p(s)) s.Print();
        }
    }
    class Teacher : Person 
    {
        string qualification; string psk;
        public Teacher(string t, string n, string f, string o string q, string p) : base(string t, string n, string f, string o)
        {
            qualification = q; psk = p
        }
        public override Print()
        {
            base.Print();
            Console.WriteLine($"Predmetno-tsiklovaya komissiya: {psk}. Qualify: {qualification}");
        }
    }
    class Student : Person
    {
        public List<int> courses = new List<int>;
        public int stud;
        public Teacher(string t, string n, string f, string o, int s) : base(string t, string n, string f, string o)
        {
            stud = s;
        }
        public override Print()
        {
            base.Print();
            Console.WriteLine($"Nomer studa: {stud}.");
        }

    }
    //В университете есть студенты, которые могут записываться на различные курсы.
    //У каждого курса есть преподаватель, который его ведет.Система должна хранить информацию обо всех участниках учебного процесса
    //и позволять выполнять различные операции с ними.

    //Пользователь должен иметь возможность добавлять в систему новых студентов и просматривать информацию
    //о них. Также необходимо реализовать функциональность записи студентов на курсы и просмотра списка всех
    //курсов, на которые записан конкретный студент.

    //Система должна позволять добавлять преподавателей и просматривать информацию о каждом из них.
    //Преподаватели могут быть назначены на различные курсы, которые они будут вести.

    //Для управления курсами нужно реализовать возможность создания новых курсов, просмотра детальной
    //информации о каждом курсе и вывода списка всех студентов, записанных на конкретный курс.

    //Дополнительно программа должна предоставлять возможность вывода полных списков: всех студентов
    //в системе, всех преподавателей и всех доступных курсов.

    //Ваша задача - спроектировать архитектуру приложения, используя принципы ООП, и реализовать
    //консольное меню для удобного взаимодействия со всеми описанными функциями системы.
    internal class Program
    {
        static void Main(string[] args)
        {
            
            void menu()
            {
                Console.WriteLine("СУУ");
                while (true)
                {
                    Console.WriteLine("0 addBook; 1 delete; 2 search; 3 sort; 4 prices; 5 grpby; 6 block; 7 buy");
                    uint act = check("chd???   ");
                    switch (act) {
                        case 0: addBook(); break;
                        case 1: del(); break;
                        case 2: search(); break;
                        case 3: sort(); break;
                        case 4: prices(); break;
                        case 5: grpby(); break;
                        case 6: addBlock(); break;
                        case 7: buy(); break;
                        default: return;
                    }
                }
            }
            menu();
        }
    }
}
