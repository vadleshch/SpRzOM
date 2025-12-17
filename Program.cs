using SpRzOM;
using System.Diagnostics;
using System.Text;
using static SpRzOM.Long;
using static SpRzOM.GField;

Long RandomLong(int len)
{
    Random r = new Random();
    Long A = new Long("0");
    int bits = len % 32;
    int words = len / 32;
    for (int i = 0; i < words; i++)
    {
        A.Number.Add(0);
    }
    for (int i = 0; i < A.Number.Count - 1; i++)
    {
        A.Number[i] = r.NextInt64() & 0xffffffff;
    }
    A.Number[A.Number.Count - 1] = (r.NextInt64() & 0xffffffff) >> 32 - bits;
    return A;
}


void MainTimers()
{
    Long res = new Long();
    Stopwatch sw1 = new Stopwatch();
    Stopwatch sw2 = new Stopwatch();
    Stopwatch sw3 = new Stopwatch();
    Stopwatch sw4 = new Stopwatch();
    sw1.Start();
    Long[,] Dataset = new Long[1, 1001];
    for (int i = 0; i < 1; i++)
    {
        for (int j = 0; j < 1001; j++)
        {
            Dataset[i, j] = RandomLong(131);
        }
    }
    sw1.Stop();
    Console.WriteLine(sw1.ElapsedMilliseconds);
    sw1.Reset();
    NBGField gf1 = new NBGField();
    for (int i = 0; i < 1; i++)
    {
        Long gcd;
        Long lcm;
        Long u;
        Long v;
        int i1;
        sw1.Start();
        for (int j = 0; j < 100; j++)
        {
            gcd = gf1.ElPow(Dataset[i, j], Dataset[i, j + 1]);
        }
        sw1.Stop();
        sw2.Start();
        for (int j = 0; j < 100; j++)
        {
            gcd = gf1.ElInv(Dataset[i, j]);
        }
        sw2.Stop();
        sw3.Start();
        for (int j = 0; j < 100; j++)
        {
            gcd = gf1.ElMul(Dataset[i, j], Dataset[i, j + 1]);
        }
        sw3.Stop();
        //sw4.Start();
        //for (int j = 0; j < 20000; j++)
        //{
        //    res = Dataset[i, j] / Dataset[i, j + 1];
        //}
        //sw4.Stop();
        Console.WriteLine($"{(i + 1) * 100}: Pow {sw1.ElapsedMilliseconds} Inv {sw2.ElapsedMilliseconds} Mul {sw3.ElapsedMilliseconds}");
        sw1.Reset(); sw2.Reset(); sw3.Reset(); sw4.Reset();
    }
}

MainTimers();

Console.WriteLine("1 - Lab1; 2 - Lab2; 3 - Lab3; 4 - Lab4");
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
                Long M;
                Long D;
                Console.WriteLine("Уведіть шістнадцяткове А:");
                Console.Write(">");
                str = Console.ReadLine();
                str = "8692D3FE302469AE8984488668D86530CEA78D23172AD0F8AE0B7864545F3189FA50F90EBD40071BD5B0D1B9ADF82EEEA121138B95DF363CF89469980F23D61F50E16DB91DF15E86B1870E719FD710B3783A217F78E1560A1130E49FCD9C9AD0E0190EF0D27E841B70BD05BF666697E80A5882AB6DAD4B625BB01F755E2E981A";
                A = new Long(str, 16);
                Console.WriteLine(A.HexToString());
                Console.WriteLine("Уведіть шістнадцяткове B:");
                Console.Write(">");
                str = Console.ReadLine();
                str = "1A32BA32AD1F22E2E43535524CC668C570A471534664AC4CFBAC765EE7E902152571DA2B7347BEC45EBBAE7054E2F8C7227FB7F60AD9ACE7D3ED9DA24E92BB49A844D92F3DED8ECCFF053C83A57C25F5D10C9A85C94C78E4D29071FFEE27035698B768286B6B15DF003A5F53571055D83FE18DE0D00AC295F4BF4D08B668C83F";
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
                Console.WriteLine(Bingcd(A, B).HexToString());
                //Console.WriteLine("Уведіть шістнадцяткове M:");
                //Console.Write(">");
                //str = Console.ReadLine();
                str = "7E0DD5863122AC09C282D526BF37EE9641127ECCDD09B29552057FADFEB8FDCD2FC937C7D563E0335890D1C4DF34DC202B7461352C98D12135C92533492EBBA07F5038B1950235C2418F9DA2B1B1CA5E49F2D68BF4B5779E70E5981F5DBFC2CE41C9363A98CEE65B17E2F2D2287DF76298B4869ADEC8F2327CBD5AA138D8EBAB";
                M = new Long(str, 16);
                Console.Write("(A+B)modM=");
                D = ModAdd(A, B, M);
                Console.WriteLine(D.HexToString());
                Console.Write("(A-B)modM=");
                D = ModSub(A, B, M);
                Console.WriteLine(D.HexToString());
                Console.Write("(A*B)modM=");
                D = ModMul(A, B, M);
                Console.WriteLine(D.HexToString());
                Console.Write("A^2modM=");
                D = ModSqr(A, M);
                Console.WriteLine(D.HexToString());
                Console.Write("A^BmodM=");
                D = ModDegree(A, B, M);
                Console.WriteLine(D.HexToString());
                Console.Write("(B-A)modM=");
                D = ModSub(B, A, M); Console.WriteLine(D.HexToString());
                return;
            }
        case "3":
            while (true)
            {
                GField gf = new GField();
                //Console.WriteLine("Уведіть шістнадцяткове А:");
                //Console.Write(">");
                //str = Console.ReadLine();
                str = "792d6ef9416cb2076ac2e6368471fda47e5a95c35208fc277169197530cfec614dcbf69";
                A = new Long(str, 16);
                //Console.WriteLine("Уведіть шістнадцяткове B:");
                //Console.Write(">");
                //str = Console.ReadLine();
                str = "56df1366d2e020579e22f67e66b8a4ee397db984f686bfa1d0ace4f5727d9cf9b6269d2";
                B = new Long(str, 16);
                //Console.WriteLine("Уведіть шістнадцяткове C:");
                //Console.Write(">");
                //str = Console.ReadLine();
                str = "4bca4fb7e3c9fb60d73c99a671842d3ce3e063c65c44a3761bc5f28e40594f157bea46";
                C = new Long(str, 16);
                Console.WriteLine("A + B");
                Long Res = gf.ElSum(A, B);
                Console.WriteLine(Res.HexToString());
                Console.WriteLine("A * B");
                Res = gf.ElMul(A, B);

                Console.WriteLine(Res.HexToString());
                Console.WriteLine("A ^ 2");
                Res = gf.ElSqr(A);
                Console.WriteLine(Res.HexToString());
                Console.WriteLine("A ^ С");
                Res = gf.ElPow(A, C);
                Console.WriteLine(Res.HexToString());
                Console.WriteLine("Tr(A)");
                Console.WriteLine(gf.ElTrace(A));
                Console.WriteLine("!A");
                Res = gf.ElInv(A);
                Console.WriteLine(Res.HexToString());
                return;
            }
        case "4":
            while (true)
            {
                NBGField nbgf = new NBGField();
                //Console.WriteLine("Уведіть шістнадцяткове А:");
                //Console.Write(">");
                //str = Console.ReadLine();
                //str = "7f34f843a970dcb34187ea965a91ce612";
                //A = new Long(str, 16);
                ////Console.WriteLine("Уведіть шістнадцяткове B:");
                ////Console.Write(">");
                ////str = Console.ReadLine();
                //str = "7f34f843a970dcb34187ea965a91ce612";
                //B = new Long(str, 16);
                ////Console.WriteLine("Уведіть шістнадцяткове C:");
                ////Console.Write(">");
                ////str = Console.ReadLine();
                //str = "7fc0f4780ae4b4d08a15c368375414d59";
                //C = new Long(str, 16);
                A = RandomLong(131);
                B = RandomLong(131);
                C = RandomLong(131);
                Console.WriteLine("A");
                Console.WriteLine(A.HexToString());
                Console.WriteLine("B");
                Console.WriteLine(B.HexToString());
                Console.WriteLine("C");
                Console.WriteLine(C.HexToString());
                Console.WriteLine("1");
                Console.WriteLine(nbgf.GetOne().HexToString());
                Console.WriteLine("A + B");
                Long Res = nbgf.ElSum(A, B);
                Console.WriteLine(Res.HexToString());
                Console.WriteLine("A * B");
                Res = nbgf.ElMul(A, B);
                Console.WriteLine(Res.HexToString());
                Console.WriteLine("A * 1");
                Res = nbgf.ElMul(A, nbgf.GetOne());
                Console.WriteLine(Res.HexToString());
                Console.WriteLine("(A + B) * C");
                Res = nbgf.ElMul(nbgf.ElSum(A, B), C);
                Console.WriteLine(Res.HexToString());
                Console.WriteLine("B * C + C * A");
                Res = nbgf.ElSum(nbgf.ElMul(B, C), nbgf.ElMul(C, A));
                Console.WriteLine(Res.HexToString());
                Console.WriteLine("sqr(A)");
                Res = nbgf.ElSqr(A);
                Console.WriteLine(Res.HexToString());
                Console.WriteLine("A * A");
                Res = nbgf.ElMul(A, A);
                Console.WriteLine(Res.HexToString());
                Console.WriteLine("A ^ 2");
                Res = nbgf.ElPow(A, new Long("2"));
                Console.WriteLine(Res.HexToString());
                Console.WriteLine("A ^ С");
                Res = nbgf.ElPow(A, C);
                Console.WriteLine(Res.HexToString());
                Console.WriteLine("Tr(A)");
                Console.WriteLine(nbgf.ElTrace(A));
                Console.WriteLine("A^(-1)");
                Res = nbgf.ElInv(A);
                Console.WriteLine(Res.HexToString());
                Console.WriteLine("A * A^(-1)");
                Res = nbgf.ElMul(Res, A);
                Console.WriteLine(Res.HexToString());
                Console.WriteLine("A * 0");
                Res = nbgf.ElMul(nbgf.GetZero(), A);
                Console.WriteLine(Res.HexToString());
                Console.ReadKey();
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


