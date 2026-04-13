using CourseApp.Domain.Models;
using CourseApp.Repository.Repositories.Services;
using CourseApp.Service.Exceptions;
using CourseApp.Service.Helpers;
using CourseApp.Service.Services.Implementations;

using System;
using System.Text;

namespace CourseApp
{
    class Program
    {
        static void Main(string[] args)
        {
           
            var groupRepo = new CourseGroupRepository();
            var studentRepo = new StudentRepository();

            var groupService = new CourseGroupService(groupRepo);
            var studentService = new StudentService(studentRepo, groupService);

            
            Helper.PlayWelcomeSound();

            while (true)
            {
                Console.Clear();
                Helper.ColorWrite(ConsoleColor.Cyan, "=============================================");
                Helper.ColorWrite(ConsoleColor.White, "        Welcome to ConsoleApp      ");
                Helper.ColorWrite(ConsoleColor.Cyan, "=============================================");

                Console.WriteLine("[1] Qrup Emeliyyatlari");
                Console.WriteLine("[2] Telebe Emeliyyatlari");
                Console.WriteLine("[0] Sistemden Cıxıs");

                Helper.ColorWrite(ConsoleColor.Yellow, "Seciminizi daxil edin: ");
                string mainChoice = Console.ReadLine();

                try
                {
                    switch (mainChoice)
                    {
                        case "1":
                            GroupMenu(groupService);
                            break;
                        case "2":
                            StudentMenu(studentService, groupService);
                            break;
                        case "0":
                            Helper.PlayExitSound();
                            Helper.ColorWrite(ConsoleColor.Magenta, "Sistem baglandi. Sag olun!");
                            return;
                        default:
                            Helper.ColorWrite(ConsoleColor.Red, "Yanlis secim! Yeniden yoxlayin.");
                            break;
                    }
                }
                catch (Exception ex)
                { 
                    Helper.ColorWrite(ConsoleColor.Red, $"XETA: {ex.Message}");
                }

                Helper.ColorWrite(ConsoleColor.DarkGray, "Ardi ucun  her  hansi duymeye basin...");
                Console.ReadKey();
            }
        }

        static void GroupMenu(CourseGroupService groupService)
        {
            Console.Clear();
            Helper.ColorWrite(ConsoleColor.Green, "--- QRUP EMELIYYATLARI ---");
            Console.WriteLine("1. Yeni qrup yarat");
            Console.WriteLine("2. Qruplarin  siyahisi");
            Console.WriteLine("3. Qrupu sil");
            Console.WriteLine("0. Geri");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Write("Qrup adi: ");
                    string name = Console.ReadLine();
                    Console.Write("Muellim adi: ");
                    string teacher = Console.ReadLine();
                    Console.Write("Otaq adi/nömresi(id): ");
                    string room = Console.ReadLine();

                    groupService.Create(new CourseGroup { Name = name, TeacherName = teacher, Room = room });
                    Helper.ColorWrite(ConsoleColor.Green, "Qrup ugurla yaradildi!");
                    break;

                case "2":
                    var groups = groupService.GetAll();
                    Helper.ColorWrite(ConsoleColor.Yellow, "Movcud Qruplar:");
                    foreach (var g in groups)
                        Console.WriteLine($"ID: {g.Id} | Ad: {g.Name} | Muellim: {g.TeacherName} | Otaq: {g.Room}");
                    break;

                case "3":
                    Console.Write("Silmek istediyiniz qrupun ID-sini yazın: ");
                    int id = int.Parse(Console.ReadLine());
                    groupService.Delete(id);
                    Helper.ColorWrite(ConsoleColor.DarkYellow, "Qrup ve ona bagli olanlar silindi.");
                    break;
            }
        }

        static void StudentMenu(StudentService studentService, CourseGroupService groupService)
        {
            Console.Clear();
            Helper.ColorWrite(ConsoleColor.Blue, "--- TELEBE EMELIYYATLARI ---");
            Console.WriteLine("1. Telebe elvae et");
            Console.WriteLine("2. Butun telebelri gor");
            Console.WriteLine("3. Telebeni  sil");
            Console.WriteLine("0. Geri");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Write("Telebe afi: ");
                    string sName = Console.ReadLine();
                    Console.Write("Telebe soyadi: ");
                    string sSurname = Console.ReadLine();
                    Console.Write("Yasi: ");
                    int age = int.Parse(Console.ReadLine());
                    Console.Write("Daxil olacagi Qrup ID-si: ");
                    int groupId = int.Parse(Console.ReadLine());

                    studentService.Create(groupId, new Student { Name = sName, Surname = sSurname, Age = age });
                    Helper.ColorWrite(ConsoleColor.Green, "Telebe qrupa elvvae edildi!");
                    break;

                case "2":
                    var students = studentService.GetAll();
                    Helper.ColorWrite(ConsoleColor.Yellow, "Butun Telebeler:");
                    foreach (var s in students)
                        Console.WriteLine($"ID: {s.Id} | {s.Name} {s.Surname} | Yas: {s.Age} | Qrup: {s.CourseGroup?.Name}");
                    break;

                case "3":
                    Console.Write("Silmek istediyiniz telebenin ID-sini yazin: ");
                    int sid = int.Parse(Console.ReadLine());
                    studentService.Delete(sid);
                    Helper.ColorWrite(ConsoleColor.DarkYellow, "Telebe sistemden silindi.");
                    break;
            }
        }
    }
}