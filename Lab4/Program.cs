
using System;
using CSLab3;
using CSLab4.Interfaces;
using CSLab4.Services;
using CSLab3.Interfaces;

namespace CSLab4 // Note: actual namespace depends on the project name.
{
    public class Program
    {
        static void Main(string[] args)
        {
            using var context = new SHA256Context();

            IArchiveUser archive = new ArchiveUser(context);
            IRSA cipher = new RSA();
            ISHA256Encrypt encrypt = new SHA256Encrypt();

            var userService = new UserService(archive, cipher, encrypt);
            userService.UserAddDB();
            Console.WriteLine(userService.PasswordVerify());
        }
    }
}