using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSlab1
{
    public static class ChangeString
    {
        public static string DeleteWildSymbols(string input)
          {
            //delete wild symbols
            var r = new Regex("(?:[^a-z0-9]|(?<=['\"])s)",
                RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return r.Replace(input, String.Empty);
        }
    }
}
