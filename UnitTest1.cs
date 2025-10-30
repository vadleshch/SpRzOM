using static SpRzOM.Long;

namespace SpRzOM
{
    public class Tests
    {
        static Long RandomLong(int len)
        {
            Random r = new Random();
            Long A = new Long("0");
            int bits = len % 32;
            int words = len / 32;
            for (int i = 1; i < words; i++)
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
        Long A = RandomLong(1500);
        Long B = RandomLong(500);
        Long C = RandomLong(1500);
        Long D = new Long("0");
        Long E = new Long("0");

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Test1()
        {
            D = (A + B) * C;
            E = C * (A + B);
            if (LongCmp(D, E, false) == 0)
            {
                E = (A * C) + (B * C);
                if (LongCmp(D, E, false) == 0)
                {
                    Assert.Pass();
                }
            }
            Assert.Fail();
        }

        [Test]
        public void Test2()
        {
            E = new Long("0");
            D = B * new Long("64");
            for (int i = 0; i < 100; i++)
            {
                E = E + B;
            }
            if (LongCmp(D, E, false) == 0)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
        [Test]
        public void Test3()
        {
            D = A / B;
            E = A % B;
            C = D * B + E;
            if (LongCmp(C, A, false) == 0)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
    }
}