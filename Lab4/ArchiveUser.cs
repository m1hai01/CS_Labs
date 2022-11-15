using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSLab4.Interfaces;

namespace CSLab4
{
    public class ArchiveUser : IArchiveUser
    {
        private readonly SHA256Context _context;

        public ArchiveUser(SHA256Context context)
        {
            _context = context;
        }

        public void UserInitialization(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User UserAccess(Guid id)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserId == id);
            return user;
        }
    }
}
