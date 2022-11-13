using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSLab3.Interfaces
{
    public interface IRSA
    {
        string Encrypt(string plaintext);
        string Decrypt(string ciphertext);
    }
}
