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
        private static uint nextID = 1;
        public uint id;
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
            Console.WriteLine($"UserID: {id}. {name} {familya} {otchestvo}. Pozvonite: {telephone};");
        }
        public abstract AddCourse();
    }
    class Course
    {
        string title;
        private static int nextCode = 0;
        public int code;
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
        static int nextStud = 0;
        public int stud;
        public Teacher(string t, string n, string f, string o) : base(string t, string n, string f, string o)
        {
            stud = nextStud; nextStud++;
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
    static class UniversityManager()
    {
        List<Student> students;
        List<Course> courses;
        List<Teacher> teachers;
        void AddTeacher()
        {
            Console.WriteLine("Creating new teacher...");
            Console.Write("Name:  ");
            string n = Console.ReadLine();
            Console.Write("Familiya:  ");
            string f = Console.ReadLine();
            Console.Write("Otchestvo:  ");
            string o = Console.ReadLine();
            Console.Write("Telephone number:  ");
            string t = Console.ReadLine();
            Console.Write("Qualification:  ");
            string q = Console.ReadLine();
            Console.Write("Ptsk:  ");
            string p = Console.ReadLine();
            teachers.Add(new Teacher(t, n, f, o, q, p));
        }
        void AddStudent()
        {
            Console.WriteLine("Creating new student...");
            Console.Write("Name:  ");
            string n = Console.ReadLine();
            Console.Write("Familiya:  ");
            string f = Console.ReadLine();
            Console.Write("Otchestvo:  ");
            string o = Console.ReadLine();
            Console.Write("Telephone number:  ");
            string t = Console.ReadLine();
            students.Add(new Student(t, n, f, o));
        }
        uint check(string s) 
        {
            uint result;
            Console.Write(s);
            do
            {
                string s = Console.ReadLine();
                uint.Parse(s, out result);
            } while (!result)
        }
        void AddCourse()
        {
            Console.WriteLine("Creating new student...");
            Console.Write("Title:  ");
            string t = Console.ReadLine();
            Teacher te = teachers.Find(teacher => teacher.id == check(Console.ReadLine("Teacher ID:  ")));
            courses.Add(new Course(t, te));
        }
        void SeeStudentCourses()
        {
            Student s = students.Find(student => student.id == check("StudentID"));
            foreach (uint i in s.courses) courses.FirstOrDefault(course => course.code == i)?.Print();
        }
        void SeeStudentInfo()
        {
            students.Find(student => student.id == check("StudentID"))?.Print();
        }
        void start()
        {
            Console.WriteLine("СУУ");
            while (true)
            {
                Console.WriteLine("1. AddStudent"); //+
                Console.WriteLine("2. SeeStudentInfo");
                Console.WriteLine("3. AddCourseToStudent");
                Console.WriteLine("4. SeeStudentCourses"); //+
                Console.WriteLine("5. AddTeacher"); //+
                Console.WriteLine("6. SeeTeacherInfo");
                Console.WriteLine("7. SetCourseTeacher");
                Console.WriteLine("8. AddCourse"); //+
                Console.WriteLine("9. SeeCourseInfo");
                Console.WriteLine("10. SeeCourseStudents");
                Console.WriteLine("11. SeeAllStudents");
                Console.WriteLine("12. SeeAllTeachers");
                Console.WriteLine("13. SeeAllCourses");
                Console.WriteLine("0. Nya poka");
            }
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            
            menu();
        }
    }
}
