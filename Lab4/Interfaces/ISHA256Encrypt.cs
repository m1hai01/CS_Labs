using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSLab4.Interfaces
{
    public interface ISHA256Encrypt
    {
        string HashPassword(string password);
    }
}
