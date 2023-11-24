using WebApi.Model;

namespace WebApi.Contracts
{
	public interface IShoppingCartService
    {
        IEnumerable<ShoppingItem> GetAllItems();
        ShoppingItem Add(ShoppingItem newItem);
        ShoppingItem Update(ShoppingItem item);
        ShoppingItem GetById(Guid id);
        void Remove(Guid id);
    }
}