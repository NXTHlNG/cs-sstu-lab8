using cs_sstu_lab8.Models;

namespace cs_sstu_lab8.Data
{
    public interface ICartService
    {
        List<CartItem> GetCartItems();

        void saveCartItems(List<CartItem> cartItems);

        void AddItem(Product product, int amount = 1);

        public void RemoveItem(Product product, int amount = 1);

        public void EmptyCart();
    }
}