using WebApi.Context;
using WebApi.Contracts;
using WebApi.Model;

namespace WebApi.Services
{
	public class ShoppingCartService : IShoppingCartService
	{
		private readonly DataContext _dataContext;

		public ShoppingCartService(DataContext dataContext)
		{
			_dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
		}

		public ShoppingItem Add(ShoppingItem newItem) => throw new NotImplementedException();
		public ShoppingItem Update(ShoppingItem item) => throw new NotImplementedException();

		public IEnumerable<ShoppingItem> GetAllItems() => throw new NotImplementedException();

		public ShoppingItem GetById(Guid id) => throw new NotImplementedException();

		public void Remove(Guid id) => throw new NotImplementedException();
	}
}