using MyClassroom.Core.Models;

namespace MyClassroom.Application.Services
{
    public interface IUserService
    {
        Task<User?> AddRole(int userId, int roleId);
        Task<User?> AddRole(int userId, string roleName);
        Task<User> AddUser(User user);
        Task<User?> GetUserByIdAsync(int id);
    }
}