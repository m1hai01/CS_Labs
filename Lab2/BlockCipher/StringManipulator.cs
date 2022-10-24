using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.BlockCipher
{
    public static class StringManipulator
    {
        public static IEnumerable<string> BreakeApart(this string s, int nr)
        {
            int i = 0;
            while (i < s.Length)
            {
                if (s.Length - i >= nr)
                {
                    yield return s.Substring(i, nr);
                }
                else
                {
                    yield return s.Substring(i, s.Length - i);
                }

                i += nr;
            }
        }
    }
}
