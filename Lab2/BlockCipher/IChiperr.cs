using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.BlockCipher
{
    public interface IChiperr
    {
        List<string> Encrypt(string plaintext, int encrypt = 0);
        List<string> Decrypt(List<string> ciphertext);
    }
}
