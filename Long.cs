using System;
using System.Diagnostics.Contracts;


namespace SpRzOM
{

    internal sealed class Long
    {
        private List<long> Number = new List<long>();
        private bool Negative = false;
        public Long(string str = "", int s = 16)
        {
            if (str != "" && str[0] == '-')
            {
                str = str.Substring(1);
                Negative = true;
            }
            switch (s)
            {
                case 16:
                    {
                        int d = str.Length % 8;
                        if (d != 0)
                        {
                            str = new string('0', 8 - d) + str;
                        }
                        for (int i = str.Length; i > 0; i -= 8)
                        {
                            Number.Add(Convert.ToInt64(str.Substring(i - 8, 8), 16));
                        }
                        break;
                    }
                case 2:
                    {
                        int d = str.Length % 32;
                        if (d != 0)
                        {
                            str = new string('0', 32 - d) + str;
                        }
                        for (int i = str.Length; i > 0; i -= 32)
                        {
                            Number.Add(Convert.ToInt64(str.Substring(i - 32, 32), 2));  
                        }
                        break;
                    }
                default:
                    break;
            }
        }
        public string BinToString()
        {
            string str = "";
            if (Negative)
            {
                str = "-";
            }
            str += Convert.ToString(Number[Number.Count - 1], 2);
            for (int i = Number.Count - 2; i >= 0; i--)
            {
                str += Convert.ToString(Number[i], 2).PadLeft(32, '0');
            }
            return str;
        }
        public string HexToString()
        {
            string str = "";
            if (Negative)
            {
                str = "-";
            }
            str += Convert.ToString(Number[Number.Count - 1], 16);
            for (int i = Number.Count - 2; i >= 0; i--)
            {
                str += Convert.ToString(Number[i], 16).PadLeft(8, '0');
            }
            return str;
        }
        public static Long RemoveEmpty(Long A)
        {
            int i = A.Number.Count - 1;
            while (i > 0 && A.Number[i] == 0)
            { 
                A.Number.RemoveAt(i);
                i--;
            }
            return A;
        }
        public static Long operator +(Long A, Long B)
        {
            Long C;
            long carry = 0;
            if (A.Negative != B.Negative)
            {
                if (A.Negative)
                {
                    A.Negative = false;
                    C = B - A;
                }
                else
                {
                    B.Negative = false;
                    C = A - B;
                }
                return C;
            }
            if (A.Number.Count < B.Number.Count)
            {
                C = A;
                A = B;
                B = C;
            }
            for (int i = B.Number.Count; i < A.Number.Count; i++)
            {
                B.Number.Add(0);
            }
            C = new Long();
            long temp;
            for (int i = 0; i < A.Number.Count; i++)
            {
                temp = A.Number[i] + B.Number[i] + carry;
                C.Number.Add(temp & 0xFFFFFFFFL);
                carry = temp >> 32;
            }
            if (carry != 0)
            {
                C.Number.Add(carry);
            }
            B = RemoveEmpty(B);
            C.Negative = A.Negative;
            return C;
        }
        public static Long operator -(Long A, Long B)
        {
            Long C = new Long();
            long borrow = 0;
            long temp;
            
            if (A.Negative && !B.Negative)
            {
                A.Negative = false;
                C = A + B;
                C.Negative = true;
                return C;
            }
            if (!A.Negative && B.Negative)
            {
                B.Negative = false;
                C = A + B;
                return C;
            }
            if (A.Negative && B.Negative)
            {
                B.Negative = false;
                A.Negative = false;
                C = A;
                A = B;
                B = C;
                C = new Long();
            }
            if (LongCmp(A, B, false) == -1)
            {
                C = A;
                A = B;
                B = C;
                C = new Long();
                C.Negative = true;
            }
            if (A.Number.Count > B.Number.Count)
            {
                for (int i = B.Number.Count; i < A.Number.Count; i++)
                {
                    B.Number.Add(0);
                }
            }
            else
            {
                for (int i = A.Number.Count; i < B.Number.Count; i++)
                {
                    A.Number.Add(0);
                }
            }
            for (int i = 0; i < A.Number.Count; i++)
            {
                temp = A.Number[i] - B.Number[i] - borrow;
                if (temp >= 0)
                {
                    C.Number.Add(temp);
                    borrow = 0;
                }
                else
                {
                    C.Number.Add(temp + 0x100000000);
                    borrow = 1;
                }
            }
            A = RemoveEmpty(A);
            B = RemoveEmpty(B);
            C = RemoveEmpty(C);
            if (C.Number[C.Number.Count - 1] == 0)
            {
                C.Negative = false;
            }
            return C;
        }

        public static Long operator >>(Long A, int n)
        {
            Long res = new Long();
            int words = n / 32;
            int bits = n % 32;
            ulong carry = 0;
            ulong temp;
            for (int i = words; i < A.Number.Count; i++)
            {
                temp = (ulong)A.Number[i];
                temp = (temp >> bits) | carry;
                carry = ((ulong)A.Number[i] << (32 - bits)) & 0xFFFFFFFFUL;
                res.Number.Add((long)temp & 0xFFFFFFFFL);
            }
            return res;
        }

        public static Long operator <<(Long A, int n)
        {
            Long res = new Long();
            int words = n / 32;
            int bits = n % 32;
            for (int i = 0; i < words; i++)
            {
                res.Number.Add(0);
            }
            ulong carry = 0;
            ulong temp;
            for (int i = 0; i < A.Number.Count; i++)
            {
                temp = (ulong)A.Number[i];
                temp = (temp << bits) + carry;
                carry = temp >> 32;
                res.Number.Add((long)temp & 0xFFFFFFFFL);
            }
            if (carry != 0)
            {
                res.Number.Add((long)carry);
            }
            return res;
        }

        public static int LongCmp(Long A, Long B, bool sign = true)
        {
            // A > B return 1
            // A < B return -1
            bool reverse = false;
            if (sign)
            {
                if (!A.Negative && B.Negative)
                {
                    return 1;
                }
                if (A.Negative && !B.Negative)
                {
                    return -1;
                }
                if (A.Negative && B.Negative)
                {
                    reverse = true;
                }
            }
            if (A.Number.Count > B.Number.Count)
            {
                return reverse ? -1 : 1;
            }
            if (A.Number.Count < B.Number.Count)
            {
                return reverse ? 1 : -1;
            }
            for (int i = A.Number.Count - 1; i >= 0; i--)
            {
                if (A.Number[i] > B.Number[i])
                {
                    return reverse ? -1 : 1;
                }
                if (A.Number[i] < B.Number[i])
                {
                    return reverse ? 1 : -1;
                }
            }
            return 0;
        }

        public static Long operator *(Long A, Long B)
        {
            Long C = new Long();
            Long temp = new Long();
            for (int i = 0; i < B.Number.Count; i++)
            {
                temp = LongMulOneDigit(A, B.Number[i]);
                //temp = temp << (i * 32);
                temp = LongShiftDigitsToHigh(temp, i);
                C = C + temp;
            }
            C = RemoveEmpty(C);
            if (A.Negative != B.Negative)
            {
                C.Negative = true;
            }
            return C;
        } 
        
        private static Long LongShiftDigitsToHigh(Long temp, int i)
        {
            Long res = new Long();
            for (int j = 0; j < i; j++)
            {
                res.Number.Add(0);
            }
            for (int j = 0; j < temp.Number.Count; j++)
            {
                res.Number.Add(temp.Number[j]);
            }
            return res;
        }
        private static Long LongMulOneDigit(Long A, long b)
        {
            Long C = new Long();
            ulong carry = 0;
            ulong temp;
            for (int i = 0; i < A.Number.Count; i++)
            {
                temp = (ulong)A.Number[i] * (ulong)b + (ulong)carry;
                C.Number.Add((long)(temp & 0xFFFFFFFFUL));
                carry = temp >> 32;
            }
            if (carry != 0)
            {
                C.Number.Add((long)carry);
            }
            return C;
        }
        
        public static Long operator /(Long A, Long B)
        {
            A.Negative = false;
            B.Negative = false;
            Long Q;
            Long R;
            long k = B.BitLength();
            R = A;
            Q = new Long("0");
            for (int i = 1; i < A.Number.Count; i++)
            {
                Q.Number.Add(0);
            }
            long t;
            Long C;
            while (LongCmp(R, B, false) >= 0)
            {
                t = R.BitLength();
                C = B << (int)(t - k);
                if (LongCmp(R, C, false) == -1)
                {
                    t--;
                    C = B << (int)(t - k);
                }
                R = R - C;
                R.Negative = false;
                Q.Number[(int)((t - k) / 32)] = Q.Number[(int)((t - k) / 32)] | (long)(1UL << (int)((t - k) % 32));
            }
            Q = RemoveEmpty(Q);
            if (A.Negative != B.Negative)
            {
                Q.Negative = true;
            }
            return Q;
        }

        public static Long operator %(Long A, Long B)
        {
            A.Negative = false;
            B.Negative = false;
            Long Q;
            Long R;
            long k = B.BitLength();
            R = A;
            Q = new Long("0");
            for (int i = 1; i < A.Number.Count; i++)
            {
                Q.Number.Add(0);
            }
            long t;
            Long C;
            while (LongCmp(R, B, false) >= 0)
            {
                t = R.BitLength();
                C = B << (int)(t - k);
                if (LongCmp(R, C, false) == -1)
                {
                    t--;
                    C = B << (int)(t - k);
                }
                R = R - C;
                R.Negative = false;
                Q.Number[(int)((t - k) / 32)] = Q.Number[(int)((t - k) / 32)] | (long)(1UL << (int)((t - k) % 32));
            }
            Q = RemoveEmpty(Q);
            if (A.Negative != B.Negative)
            {
                Q.Negative = true;
            }
            return R;
        }

        public static (Long, Long) Div(Long A, Long B)
        {
            A.Negative = false;
            B.Negative = false;
            Long Q;
            Long R;
            long k = B.BitLength();
            R = A;
            Q = new Long("0");
            for (int i = 1; i < A.Number.Count; i++)
            {
                Q.Number.Add(0);
            }
            long t;
            Long C;
            while (LongCmp(R, B, false) >= 0)
            {
                t = R.BitLength();
                C = B << (int)(t - k);
                if (LongCmp(R, C, false) == -1)
                {
                    t--;
                    C = B << (int)(t - k);
                }
                R = R - C;
                R.Negative = false;
                Q.Number[(int)((t - k) / 32)] = Q.Number[(int)((t - k) / 32)] | (long)(1UL << (int)((t - k) % 32));
            }
            Q = RemoveEmpty(Q);
            if (A.Negative != B.Negative)
            {
                Q.Negative = true;
            }
            return (Q, R);
        }

        public long BitLength()
        {
            if (Number.Count == 0)
            {
                return 0;
            }
            long k = (Number.Count - 1) * 32;
            long n = Number[Number.Count - 1];
            while (n != 0)
            {
                n >>= 1;
                k++;
            }
            return k;
        }
        public static Long operator ^(Long A, Long B)
        {
            Long C = new Long("1");
            for (int i = (int)B.BitLength() - 1; i >= 0; i--)
            {
                if (((B.Number[i / 32] >> (i % 32)) & 1) == 1)
                {
                    C = C * A;
                }
                if(i != 0)
                {
                    C = C * C;
                }
            }
            return C;
        }
        public static bool IsZero(Long A)
        {
            A = RemoveEmpty(A);
            if (A.Number.Count == 1 && A.Number[0] == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static Long Clone(Long A)
        {
            Long B = new Long();
            B.Number = A.Number;
            B.Negative = A.Negative;
            return B;
        }
        public static (Long, Long, Long, Long) Euclid(Long A, Long B, bool uv = false)
        {
            Long gcd = new Long();
            Long u = new Long();
            Long v = new Long();
            Long lcm = new Long();

            if (uv)
            {
                Long[] t = new Long[10];
                t[0] = Clone(A);
                t[1] = Clone(B);
                (t[3], t[2]) = Div(t[0], t[1]);
                t[4] = new Long("1");
                t[5] = new Long("0");
                t[6] = new Long("1");
                t[7] = new Long("0");
                t[8] = new Long("1");
                t[9] = Clone(t[3]);
                t[9].Negative = true;
                while (!IsZero(t[2]))
                {
                    t[0] = t[1];
                    t[1] = t[2];
                    (t[3], t[2]) = Div(t[0], t[1]);
                    t[4] = t[5];
                    t[5] = t[6];
                    t[6] = t[4] - (t[3] * t[5]);
                    t[7] = t[8];
                    t[8] = t[9];
                    t[9] = t[7] - (t[3] * t[8]);
                }
                gcd = t[1];
                u = t[5];
                v = t[8];
            }
            else
            {
                Long[] t = new Long[4];
                t[0] = A;
                t[1] = B;
                (t[3], t[2]) = Div(t[0], t[1]);
                while (!IsZero(t[2]))
                {
                    t[0] = t[1];
                    t[1] = t[2];
                    (t[3], t[2]) = Div(t[0], t[1]);
                }
                gcd = t[1];
            }
            lcm = A * B / gcd;
            return (gcd, lcm, u, v);
        }
    }
}