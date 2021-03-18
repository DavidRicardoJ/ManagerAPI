using Manager.Domain.Entities;
using Manager.Infra.Context;
using Manager.Infra.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Manager.Infra.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ManagerContext _context;
        public UserRepository(ManagerContext context) : base(context)
        {
            _context = context;
        }

        public virtual async Task<User> GetByEmail(string email)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email.Equals(email));
            return user;
        }

        public virtual async Task<List<User>> SearchByEmail(string email)
        {
            var users = await _context.Users.AsNoTracking().Where(x => x.Email.Contains(email.ToLower())).ToListAsync();
            return users;
        }

        public virtual async Task<List<User>> SearchByName(string name)
        {
            var users = await _context.Users.AsNoTracking().Where(x => x.Name.Contains(name)).OrderBy(x => x.Name).ToListAsync();
            return users;
        }
    }
}
