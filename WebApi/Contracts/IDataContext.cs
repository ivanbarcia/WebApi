using WebApi.Model;

namespace WebApi.Contracts;

public interface IDataContext
{
    Task SaveChanges();
    Task AddUser(User newUser);
    Task UpdateUser(User newUser);
    Task RemoveUser(int id);
    Task<User?> GetUser(string username);
    Task<bool> CheckPassword(User user, string password);
}