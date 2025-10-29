using SpRzOM;
using System.Text;
using static SpRzOM.Long;
Console.OutputEncoding = Encoding.UTF8;
Console.WriteLine("1 - Lab1; 2 - Lab2");
Long A;
Long B;
Long C;
string str;
try
{
    switch (Console.ReadLine())
    {
        case "1":
            while (true)
            {
                Console.WriteLine("Оберіть бажане представлення числа 1 - шістандцяткове, 2 - двійкове");
                Console.Write(">");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Уведіть шістнадцяткове А:");
                        Console.Write(">");
                        str = Console.ReadLine();
                        A = new Long(str, 16);
                        Console.WriteLine(A.HexToString());
                        Console.WriteLine("Уведіть шістнадцяткове B:");
                        Console.Write(">");
                        str = Console.ReadLine();
                        B = new Long(str, 16);
                        C = A + B;
                        Console.WriteLine("A + B = " + C.HexToString());
                        C = A - B;
                        Console.WriteLine("A - B = " + C.HexToString());
                        C = A * B;
                        Console.WriteLine("A * B = " + C.HexToString());
                        C = A / B;
                        Console.WriteLine("A / B = " + C.HexToString());
                        C = A % B;
                        Console.WriteLine("A % B = " + C.HexToString());
                        C = A ^ B;
                        Console.WriteLine("A ^ B = " + C.HexToString());
                        break;
                    case "2":
                        Console.WriteLine("Уведіть двійкове А:");
                        Console.Write(">");
                        str = Console.ReadLine();
                        A = new Long(str, 2);
                        Console.WriteLine(A.BinToString());
                        Console.WriteLine("Уведіть двійкове B:");
                        Console.Write(">");
                        str = Console.ReadLine();
                        B = new Long(str, 2);
                        C = A + B;
                        Console.WriteLine("A + B = " + C.BinToString());
                        C = A - B;
                        Console.WriteLine("A - B = " + C.BinToString());
                        C = A * B;
                        Console.WriteLine("A * B = " + C.BinToString());
                        C = A / B;
                        Console.WriteLine("A / B = " + C.BinToString());
                        C = A % B;
                        Console.WriteLine("A % B = " + C.BinToString());
                        C = A ^ B;
                        Console.WriteLine("A ^ B = " + C.BinToString());
                        break;
                    default:
                        break;
                }

            }
        case "2":
            while (true)
            {
                Long gcd;
                Long lcm;
                Long u;
                Long v;
                Console.WriteLine("Уведіть шістнадцяткове А:");
                Console.Write(">");
                str = Console.ReadLine();
                A = new Long(str, 16);
                Console.WriteLine(A.HexToString());
                Console.WriteLine("Уведіть шістнадцяткове B:");
                Console.Write(">");
                str = Console.ReadLine();
                B = new Long(str, 16);
                (gcd, lcm, u, v) = Euclid(A, B, true);
                Console.WriteLine("gcd: ");
                Console.WriteLine(gcd.HexToString());
                Console.WriteLine("lcm: ");
                Console.WriteLine(lcm.HexToString());
                Console.WriteLine("u: ");
                Console.WriteLine(u.HexToString());
                Console.WriteLine("v: ");
                Console.WriteLine(v.HexToString());
            }
            //break;
        default:
          break;
    }
}
catch (Exception)
{
    Console.Write("Під час виконання сталася помилка.");
    throw;
}


