using WebApi.Model;

namespace WebApi.Contracts
{
	public interface IUserService
    {
        Task<bool> CheckPasswordAsync(User username, string password);
        Task<User> Add(User newUser);
        Task<User> Update(User user);
        Task Remove(int userId);
        Task<User?> GetByUsername(string username);
    }
}