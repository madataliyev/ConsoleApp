using ServiceLayer.Services.Implementations;
using DomainLayer.Entities;
using RepositoryLayer.Exceptions;
using CourseApp.Helpers;


GroupService groupService = new GroupService();
StudentService studentService = new StudentService();


Console.Clear();
ColorHelper.WriteColored("*******************************************", ConsoleColor.Cyan);
ColorHelper.WriteColored("      Xos geldiniz ConsoleApp-a!          ", ConsoleColor.White);
ColorHelper.WriteColored("*******************************************", ConsoleColor.Cyan);
System.Threading.Thread.Sleep(1000);

while (true)
{
    Console.Clear();
    ColorHelper.WriteColored("--- ESAS MENYU ---", ConsoleColor.DarkCyan);
    Console.WriteLine("1. Teacher Paneli ");
    Console.WriteLine("2. Student Paneli ");
    ColorHelper.WriteColored("0. Cixis", ConsoleColor.Red);

    Console.Write("Seciminiz: ");
    string role = Console.ReadLine();

    if (role == "0") break;

    try
    {
        if (role == "1") 
        {
            Console.Clear();
            ColorHelper.WriteColored("=== TEACHER PANEL ===", ConsoleColor.Yellow);
            Console.WriteLine("1. Qrup Yarat 2. Butun Qruplari Gor 3. Telebe Elave Et 0. Geri");
            Console.Write("Secim: ");
            string op = Console.ReadLine();

            if (op == "1")
            {
                Console.Write("Qrup adi: ");
                groupService.Create(new CourseGroup { Name = Console.ReadLine() });
                ColorHelper.WriteColored("Ugurla yaradildi!", ConsoleColor.Green);
            }
            else if (op == "2")
            {
                var groups = groupService.GetAllGroups().Cast<CourseGroup>();
                foreach (var g in groups)
                {
                    Console.WriteLine($"Id:{g.Id} |  Ad: {g.Name}");
                }
            }
            else if (op == "3")
            {
                try
                {
                    Console.Write("Telebe adi: ");
                    string studentName = Console.ReadLine();

                    Console.Write("Qrup ID: ");
                    int groupId = int.Parse(Console.ReadLine());

                    
                    studentService.Create(new Student { Name = studentName }, groupId);

                    ColorHelper.WriteColored("Telebe qeyde alindi!", ConsoleColor.Green);
                }
                catch (NotFoundException ex)
                {
                    ColorHelper.WriteColored("XETA: " + ex.Message, ConsoleColor.Red);
                }
                catch (Exception)
                {
                    ColorHelper.WriteColored("Gozyasilmaz xeta bas verdi!", ConsoleColor.Red);
                }
            }
        }
        else if (role == "2") 
        {
            Console.Clear();
            ColorHelper.WriteColored("=== STUDENT PANEL ===", ConsoleColor.Green);
            Console.WriteLine("1. Butun Telebeleri Siyahila 2. Qrup Yoldaslarimi Tap 0. Geri");
            Console.Write(" Secim: ");
            string op = Console.ReadLine();

            if (op == "1")
            {
                var students = studentService.GetAll();
                foreach (var s in students) Console.WriteLine($"Ad: {s.Name} | Qrup: {s.GroupId.Name}");
            }
            else if (op == "2")
            {
                Console.Write("Qrup ID daxil et: ");
                int gid = int.Parse(Console.ReadLine());
                var friends = studentService.GetAllByGroupId(gid);
                foreach (var f in friends) Console.WriteLine($"> {f.Name}");
            }
        }
    }
    catch (NotFoundException ex)
    {
        ColorHelper.WriteColored("Xeta: " + ex.Message, ConsoleColor.Red);
    }
    catch (Exception)
    {
        ColorHelper.WriteColored("Yanlis melumat daxil etdiniz!", ConsoleColor.Red);
    }

    Console.WriteLine("Devam etmek ucun Enter basin!");
    Console.ReadLine();
}