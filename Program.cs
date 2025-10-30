using SpRzOM;
using System.Diagnostics;
using System.Text;
using static SpRzOM.Long;

//Long RandomLong(int len)
//{
//    Random r = new Random();
//    Long A = new Long("0");
//    int bits = len % 32;
//    int words = len / 32;
//    for (int i = 1; i < words; i++)
//    {
//        A.Number.Add(0);
//    }
//    for (int i = 0; i < A.Number.Count - 1; i++)
//    {
//        A.Number[i] = r.NextInt64() & 0xffffffff;
//    }
//    A.Number[A.Number.Count - 1] = (r.NextInt64() & 0xffffffff) >> 32 - bits;
//    return A;
//}


//void MainTimers()
//{
//    Long res = new Long();
//    Stopwatch sw1 = new Stopwatch();
//    Stopwatch sw2 = new Stopwatch();
//    Stopwatch sw3 = new Stopwatch();
//    Stopwatch sw4 = new Stopwatch();
//    sw1.Start();
//    Long[,] Dataset = new Long[10, 20001];
//    for (int i = 0; i < 10; i++)
//    {
//        for (int j = 0; j < 20001; j++)
//        {
//            Dataset[i, j] = RandomLong((i + 1) * 100);
//        }
//    }
//    sw1.Stop();
//    Console.WriteLine(sw1.ElapsedMilliseconds);
//    sw1.Reset();
//    for (int i = 0; i < 10; i++)
//    {
//        sw1.Start();
//        for (int j = 0; j < 20000; j++)
//        {
//            res = Dataset[i, j] + Dataset[i, j + 1];
//        }
//        sw1.Stop();
//        sw2.Start();
//        for (int j = 0; j < 20000; j++)
//        {
//            res = Dataset[i, j] - Dataset[i, j + 1];
//        }
//        sw2.Stop();
//        sw3.Start();
//        for (int j = 0; j < 20000; j++)
//        {
//            res = Dataset[i, j] * Dataset[i, j + 1];
//        }
//        sw3.Stop();
//        sw4.Start();
//        for (int j = 0; j < 20000; j++)
//        {
//            res = Dataset[i, j] / Dataset[i, j + 1];
//        }
//        sw4.Stop();
//        Console.WriteLine($"{(i + 1) * 100}: + {sw1.ElapsedMilliseconds} - {sw2.ElapsedMilliseconds} * {sw3.ElapsedMilliseconds} / {sw4.ElapsedMilliseconds}");
//        sw1.Reset(); sw2.Reset(); sw3.Reset(); sw4.Reset();
//        sw4.Start();
//        for (int j = 0; j < 20000; j++)
//        {
//            res = Dataset[i, j] / new Long("f");
//        }
//        sw4.Stop();
//        Console.WriteLine($"/f  {sw4.ElapsedMilliseconds}");
//        sw4.Reset();
//    }
//}

//MainTimers();

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


