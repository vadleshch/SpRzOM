using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static SpRzOM.Long;

namespace SpRzOM
{
    //p(x)=x^283+x^26+x^9+x+1

    public sealed class GField
    {
        Long Px = (new Long("1") << 283) + (new Long("1") << 26) + (new Long("1") << 9) + new Long("3");
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
            return RemoveEmpty(Res);
        }
        public Long ElMul(Long A, Long B)
        {
            Long Res = new Long("0");
            Long carry = A;
            for (int i = 0; i < 283; i++)
            {
                if (B.GetBit(i))
                {
                    Res = ElSum(Res, carry);
                }
                carry = carry << 1;
            }
            return ElMod(Res);
        }
        public Long ElMod(Long A)
        {
            //x^283=x^26+x^9+x+1
            Long Res = A;
            while (Res.BitLength() > 283)
            {
                int k = Res.BitLength() - 284;
                Res = ElSum(Res, Px << k);
            }
            return RemoveEmpty(Res);
        }
        public Long ElPow(Long A, Long C)
        {
            Long Res = new Long("1");
            while (C.BitLength() > 0)
            {
                if ((C.Number[0] & 1) == 1)
                {
                    Res = ElMul(Res, A);
                }
                A = ElSqr(A);
                C = C >> 1;
            }
            return Res;
        }
        public Long ElSqr(Long A)
        {
            return ElMul(A, A);
        }

        public int ElTrace(Long A)
        {
            Long Res = new Long("0");
            for (int i = 0; i < 283; i++)
            {
                Res = ElSum(Res, A);
                A = ElSqr(A);
            }
            return (int)(Res.Number[0] & 1);
        }
        
        public Long ElInv(Long A)
        {
            Long u = A;
            Long v = Px;
            Long g1 = new Long("1");
            Long g2 = new Long("0");
            int j = 0;
            while (u.Number[0] != 1 || u.Number.Count != 1)
            { 
                j = u.BitLength() - v.BitLength();
                if (j < 0)
                {
                    var t = u;
                    u = v;
                    v = t;
                    t = g1;
                    g1 = g2;
                    g2 = t;
                    j = -j;
                }
                u = ElSum(u, v << j);
                g1 = ElSum(g1, g2 << j);
            }
            return ElMod(g1);
        }
    }
}
