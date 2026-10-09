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
        public virtual void Print()
        {
            Console.WriteLine($"UserID: {id}. {name} {familya} {otchestvo}. Pozvonite: {telephone};");
        }
    }
    class Course
    {
        public string title;
        private static int nextCode = 1;
        public int code;
        public Teacher teacher;
        List<Student> students = new List<Student>();
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
            if (teacher != null) { Console.WriteLine("Teacher: "); teacher.Print(); } else { Console.WriteLine("No teacher!"); }
        }
        public void PrintStudents(Predicate<Student> p) 
        {
            Console.WriteLine("Studenti:");
            foreach (Student s in students) if (p(s)) s.Print();
        }
    }
    class Teacher : Person 
    {
        string qualification; string psk;
        static uint nextTd = 1;
        public uint td;
        public Teacher(string t, string n, string f, string o, string q, string p) : base(t, n, f, o)
        {
            qualification = q; psk = p; td = nextTd; nextTd++;
        }
        public override void Print()
        {
            base.Print();
            Console.WriteLine($"TeacherID: {td}. Predmetno-tsiklovaya komissiya: {psk}. Qualify: {qualification}");
        }
    }
    class Student : Person
    {
        public List<int> courses = new List<int>();
        static int nextStud = 1;
        public int stud;
        public Student(string t, string n, string f, string o) : base(t, n, f, o)
        {
            stud = nextStud; nextStud++;
        }
        public override void Print()
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
     class UniversityManager
    {
        List<Student> students = new List<Student>() { new Student("+739898989", "Egor", "Gorbachev", "Dmitrievich") };
        List<Teacher> teachers = new List<Teacher>() { new Teacher("+85696214896", "Fordov", "Maxim", "Olegovich", "C#", "Progr") };
        List<Course> courses = new List<Course>() { new Course("C#", null) };
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
            Teacher nt = new Teacher(t, n, f, o, q, p);
            nt.Print();
            teachers.Add(nt);
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
            Student ns = new Student(t, n, f, o);
            ns.Print();
            students.Add(ns);
        }
        uint check(string s) 
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
        void AddCourse()
        {
            Console.WriteLine("Creating new student...");
            Console.Write("Title:  ");
            string t = Console.ReadLine();
            uint checkable = check("Teacher ID:  ");
            Teacher te = teachers.Find(teacher => teacher.id == checkable);
            Course nc = new Course(t, te);
            nc.Print();
            courses.Add(nc);
            
        }
        void SeeStudentCourses()
        {
            uint checkable = check("StudentID  ");
            Student st = students.Find(student => student.stud == checkable);
            Console.WriteLine($"{st.name}'s courses:");
            foreach (uint i in st.courses) courses.FirstOrDefault(course => course.code == i)?.Print();
        }
        void SeeStudentInfo()
        {
            uint checkable = check("StudentID  ");
            Student st = students.Find(student => student.stud == checkable);
            if (st != null) { st.Print(); } else { Console.WriteLine("Invalid student!"); }
        }
        void SeeTeacherInfo()
        {
            uint checkable = check("TeacherID  ");
            Teacher te = teachers.Find(t => t.td == checkable);
            if (te != null) { te.Print(); } else { Console.WriteLine("Invalid teach!"); }
        }
        void SeeCourseInfo()
        {
            uint checkable = check("CourseCode  ");
            Course st = courses.Find(c => c.code == checkable);
            if (st != null) { st.Print(); } else { Console.WriteLine("Invalid course!"); }
        }
        void SeeCourseStudents()
        {
            Course cc = courses.Find(c => c.code == check("CourseCode  "));
            if (cc != null) { cc.PrintStudents(s => true); } else { Console.WriteLine("Invalid!"); }
        }
        void AddCourseToStudent()
        {
            Course c = courses.Find(course => course.code == check("CourseID  "));
            uint checkable = check("StudentID  ");
            Student s = students.Find(student => student.stud == checkable);
            if (c != null) { c.AddStudent(s); Console.WriteLine($"U {s.name} {s.familya} novii curs: {c.title}."); } else { Console.WriteLine("Invalid course"); };
        }
        void SetCourseTeacher()
        {
            uint checkableS = check("CourseID  ");
            Course c = courses.Find(course => course.code == checkableS);
            uint checkable = check("TeacherID  ");
            Teacher te = teachers.Find(t => t.td == checkable);
            if (c != null && te != null) { c.teacher = te; Console.WriteLine($"Teper y cursa {c.code} teacher {te.name} {te.familya}"); } else { Console.WriteLine("Invalid course or teacher!"); }
        }
        void SeeAllStudents()
        {
            foreach (Student student in students) { student.Print(); }
        }
        void SeeAllTeachers()
        {
            foreach (Teacher teacher in teachers) { teacher.Print(); }
        }
        void SeeAllCourses()
        {
            foreach (Course course in courses) { course.Print(); }
        }
        uint menu()
        {
            Console.WriteLine();
            Console.WriteLine("1. AddStudent");
            Console.WriteLine("2. SeeStudentInfo");
            Console.WriteLine("3. AddCourseToStudent");
            Console.WriteLine("4. SeeStudentCourses");
            Console.WriteLine("5. AddTeacher");
            Console.WriteLine("6. SeeTeacherInfo");//+
            Console.WriteLine("7. SetCourseTeacher");//+
            Console.WriteLine("8. AddCourse");
            Console.WriteLine("9. SeeCourseInfo");//+
            Console.WriteLine("10. SeeCourseStudents");//+
            Console.WriteLine("11. SeeAllStudents");
            Console.WriteLine("12. SeeAllTeachers");
            Console.WriteLine("13. SeeAllCourses");
            Console.WriteLine("0. Nya poka");
            uint action = check("Chd?  ");
            return action;
        }
        public void start()
        {
            Console.WriteLine("СУУ");
            while (true)
            {
                uint action = menu();
                switch (action)
                {
                    case 1: AddStudent(); break;
                    case 2: SeeStudentInfo(); break;
                    case 3: AddCourseToStudent(); break;
                    case 4: SeeStudentCourses(); break;
                    case 5: AddTeacher(); break;
                    case 6: SeeTeacherInfo(); break; 
                    case 7: SetCourseTeacher(); break;
                    case 8: AddCourse(); break;
                    case 9: SeeCourseInfo(); break;
                    case 10: SeeCourseStudents(); break;
                    case 11: SeeAllStudents(); break;
                    case 12: SeeAllTeachers(); break;
                    case 13: SeeAllCourses(); break;
                    case 0: return;
                    default: return;
                }
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            UniversityManager um = new UniversityManager();
            um.start();
        }
    }
}
