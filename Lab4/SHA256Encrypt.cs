using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSLab4.Interfaces;


namespace CSLab4
{
    public sealed class SHA256Encrypt : ISHA256Encrypt

    {
        public string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var secretBytes = Encoding.UTF8.GetBytes(password);
            var secretHash = sha256.ComputeHash(secretBytes);
            return Convert.ToHexString(secretHash);
        }
    }
}
