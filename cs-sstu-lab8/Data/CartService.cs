using cs_sstu_lab8.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace cs_sstu_lab8.Data
{
    public class CartService : ICartService
    {
        public const string key = "cartItems";

        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public List<CartItem> GetCartItems() 
        {
            var _cartIems = _httpContextAccessor.HttpContext.Session.GetString(key);

            if (_cartIems == null)
            {
                return new List<CartItem>();
            }
            else
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(_cartIems);
            }
        }

        public void saveCartItems(List<CartItem> cartItems)
        {
            _httpContextAccessor.HttpContext.Session.SetString(key, JsonConvert.SerializeObject(cartItems));
        }

        public void AddItem(Product product, int amount = 1)
        {
            if (amount < 0)
            {
                return;
            }
            
            var cartItems = GetCartItems();

            var _product = cartItems.FirstOrDefault(i => i.Product.Id == product.Id);

            if (_product == null)
            {
                if (amount > product.Quantity) return;
                cartItems.Add(new CartItem { Product = product, Amount = amount });
            }
            else
            {
                if (_product.Amount + amount > product.Quantity) return;
                _product.Amount += amount;
            }

            saveCartItems(cartItems);
        }

        public void RemoveItem(Product product, int amount = 1)
        {
            if (amount < 0) return;

            var cartItems = GetCartItems();

            var _product = cartItems.FirstOrDefault(i => i.Product.Id == product.Id);

            if (_product == null)
            {
                return;
            }
            else
            {
                if (_product.Amount - amount <= 0)
                {
                    cartItems.Remove(_product);
                }
                else
                {
                    _product.Amount -= amount;
                }
            }

            saveCartItems(cartItems);
        }

        public void EmptyCart()
        {
            saveCartItems(new List<CartItem>());
        }

    }
}
