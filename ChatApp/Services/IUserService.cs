using ChatApp.Data.Entities;

namespace ChatApp.Services
{
    public interface IUserService
    {
        Task AddUser(User user);
        Task<List<User>> GetUsers();

        Task<User> AddOrReturnUserByName(string name);
    }
}
