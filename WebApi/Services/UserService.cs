using WebApi.Context;
using WebApi.Contracts;
using WebApi.Model;

namespace WebApi.Services
{
	public class UserService : IUserService
	{
		private readonly IDataContext _dataContext;

		public UserService(IDataContext dataContext)
		{
			_dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
		}

		public async Task<bool> CheckPasswordAsync(User username, string password)
		{
			return await _dataContext.CheckPassword(username, password);
		}

		public async Task<User> Add(User newUser)
		{
			await _dataContext.AddUser(newUser);
			await _dataContext.SaveChanges();

			return newUser;
		}

		public async Task<User> Update(User user)
		{
			await _dataContext.UpdateUser(user);
			await _dataContext.SaveChanges();

			return user;
		}

		public async Task Remove(int userId)
		{
			await _dataContext.RemoveUser(userId);
			await _dataContext.SaveChanges();
		}

		public async Task<User?> GetByUsername(string username)
		{
			return await _dataContext.GetUser(username);
		}
	}
}