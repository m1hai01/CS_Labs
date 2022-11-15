using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSLab3.Interfaces;
using CSLab4.Interfaces;

namespace CSLab4.Services
{
    public class UserService
    {
        private readonly IArchiveUser _userArchive;
        private readonly IRSA _rsa;
        private readonly ISHA256Encrypt _sha256Encryptor;

        private Guid _userId;

        public UserService(IArchiveUser userRepository, IRSA rsa, ISHA256Encrypt sha256Encryptor)
        {
            _userArchive = userRepository;
            _rsa = rsa;
            _sha256Encryptor = sha256Encryptor;
        }

        public void UserAddDB()
        {
            Console.WriteLine("Login: ");
            var login = Console.ReadLine();
            Console.WriteLine("Password");
            var password = Console.ReadLine();

            password = _sha256Encryptor.HashPassword(password);

            var hexEncryptedPassword = _rsa.Encrypt(password);
            _userId = Guid.NewGuid();

            var user = new User
            {
                Login = login,
                Password = hexEncryptedPassword,
                UserId = _userId
            };

            _userArchive.UserInitialization(user);
        }

        public bool PasswordVerify()
        {
            Console.WriteLine("Verify your Password: ");
            var password = Console.ReadLine();

            password = _sha256Encryptor.HashPassword(password);

            var user = _userArchive.UserAccess(_userId);
            var decryptedPassword = _rsa.Decrypt(user.Password);

            return string.Equals(password, decryptedPassword, StringComparison.OrdinalIgnoreCase);
        }
    }
}
