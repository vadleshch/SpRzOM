using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SpRzOM.Long;
using Microsoft.Win32.SafeHandles;
using System.Net.Sockets;

namespace SpRzOM
{
    public sealed class NBGField
    {
        static int m = 131;
        static int[,] Lambda = new int[m, m];
        static Long Zero = new Long("0");
        static Long One = new Long(new string('1', 131), 2);
        public NBGField()
        {
            int[] twos = new int[m];
            int p = 2 * m + 1;
            for (int i = 32; i < m; i += 32)
            {
                Zero.Number.Add(0);
            }
            for (int i = 0; i < m; i++)
            {
                twos[i] = Convert.ToInt32(ModDegree(new Long("2"), new Long(Convert.ToString(i, 16)), new Long(Convert.ToString(p, 16))).BinToString(), 2);
            }
            int ModP(int a)
            { 
                a %= p;
                if (a < 0)
                {
                    a += p;
                }
                return a;
            }
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (ModP(twos[i] + twos[j]) == 1 || ModP(twos[i] - twos[j]) == 1 || ModP(-twos[i] + twos[j]) == 1 || ModP(-twos[i] - twos[j]) == 1)
                    {
                        Lambda[i, j] = 1;
                    }
                    else
                    {
                        Lambda[i, j] = 0;
                    }
                }
            }
        }
        public Long ElSum(Long A, Long B)
        {
            Long Res = new Long();
            if (A.Number.Count < B.Number.Count)
            {
                var t = A;
                A = B;
                B = t;
            }
            for (int i = 0; i < B.Number.Count; i++)
            {
                Res.Number.Add(A.Number[i] ^ B.Number[i]);
            }
            for (int i = B.Number.Count; i < A.Number.Count; i++)
            {
                Res.Number.Add(A.Number[i]);
            }
            return Res;
        }

        public Long ElMul(Long A, Long B)
        {
            Long res = Zero;
            int s;
            for (int i = 0; i < m; i++)
            {
                s = 0;
                for (int n = 0; n < m; n++)
                {
                    if (!A.GetBit(n)) continue;
                    for (int k = 0; k < m; k++)
                    {
                        if (Lambda[n, k] == 0)
                        {
                            continue;
                        }
                        if (!B.GetBit(k)) 
                        {
                            continue;
                        }
                        s ^= 1;
                    }
                }
                if ((s & 1) != 0)
                {
                    res += new Long("1") << i;
                }
                A = CML(A, 1, m);
                B = CML(B, 1, m);
            }
            while (res.Number.Count > 5)
            {
                res.Number.RemoveAt(res.Number.Count - 1);
            }
            if (res.Number.Count == 5)
            {
                return res;
            }
            else
            {
                while (res.Number.Count < 5)
                {
                    res.Number.Add(0);
                }
                return res;
            }
        }
        public Long GetOne()
        {
            return One;
        }

        public Long GetZero()
        {
            return Zero;
        }

        public Long ElPow(Long A, Long C)
        {
            Long Res = One;
            while (!(C.Number.Count == 1 && C.Number[0] == 0))
            {
                if ((C.Number[0] & 1) == 1)
                {
                    Res = ElMul(Res, A);
                }
                A = ElSqr(A);
                C >>= 1;
            }
            return Res;
        }
        public Long ElSqr(Long A)
        {
            return CMR(A, 1, m);
        }

        public int ElTrace(Long A)
        {
            int res = 0;
            int k = A.BitLength();
            for (int i = 0; i < k; i++)
            {
                if (A.GetBit(i))
                {
                    res++;
                }
            }
            return res % 2;
        }

        public Long ElInv(Long A)
        {
            int x = m - 1;
            List<int> bit = new List<int>();
            while (x > 0)
            {
                bit.Add(x & 1);
                x >>= 1;
            }
            int t = bit.Count - 1;
            Long b = A;
            Long temp;
            int k = 1;
            for (int i = t - 1; i >= 0; i--)
            {
                temp = b;
                for (int j = 0; j < k; j++)
                {
                    temp = ElSqr(temp);
                }
                b = ElMul(temp, b);
                k *= 2;
                if (bit[i] == 1)
                {
                    b = ElMul(ElSqr(b), A);
                    k = k + 1;
                }
            }
            return ElSqr(b);
        }
    }
}
