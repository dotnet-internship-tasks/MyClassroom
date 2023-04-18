using Microsoft.EntityFrameworkCore;
using MyClassroom.Core.Models;
using MyClassroom.Infrastructure.Data;
using System.Security.Cryptography.X509Certificates;

namespace MyClassroom.Application.Services
{
    public class UserService : IUserService
    {
        private readonly MyClassroomContext _context;

        public UserService(MyClassroomContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.Include(u => u.Roles).SingleOrDefaultAsync(user => user.Id == id);
            return user;
        }

        public async Task<User> AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> AddRole(int userId, int roleId)
        {
            var userDb = await _context.Users.Include(u => u.Roles).SingleOrDefaultAsync(u => u.Id == userId);
            var roleDb = await _context.Roles.SingleOrDefaultAsync(r => r.Id == roleId);
            if (userDb is null || roleDb is null)
            {
                return null;
            }
            userDb.Roles.Add(roleDb);
            await _context.SaveChangesAsync();
            return userDb;
        }

        public async Task<User?> AddRole(int userId, string roleName)
        {
            var userDb = await _context.Users.Include(u => u.Roles).SingleOrDefaultAsync(u => u.Id == userId);
            var roleDb = await _context.Roles.SingleOrDefaultAsync(r => r.Name == roleName);

            if (userDb is null)
            {
                return null;
            }

            if (roleDb is null)
            {
                roleDb = new Role()
                {
                    Name = roleName
                };
            }

            userDb.Roles.Add(roleDb);
            await _context.SaveChangesAsync();
            return userDb;
        }
    }
}
