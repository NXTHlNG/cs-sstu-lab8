using cs_sstu_lab8.Data;
using cs_sstu_lab8.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cs_sstu_lab8.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(ICartService cartService, AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _cartService = cartService;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_cartService.GetCartItems());
        }
                
        public async Task<IActionResult> AddItem(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            if (product.IsAvailable)
            {
                _cartService.AddItem(product);
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult> RemoveItem(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            _cartService.RemoveItem(product);

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult> MakeOrder()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var cartItems = _cartService.GetCartItems();

            if (cartItems.Any(i => i.Amount > i.Product.Quantity))
            {
                return BadRequest(cartItems);
            }

            cartItems.ForEach(i => {
                i.Product.Quantity -= i.Amount;
                _context.Entry(i.Product).State = EntityState.Modified;
            });
            await _context.SaveChangesAsync();

            var orderItems = cartItems.Select(i => new OrderItem
            {
                Amount = i.Amount,
                Price = i.Product.Price,
                ProductId = i.Product.Id
            });

            var order = new Order
            {
                UserId = user.Id,
                Items = orderItems.ToList(),
                CreatedDate = DateTime.Now,
            };

            _context.Add(order);
            await _context.SaveChangesAsync();

            _cartService.EmptyCart();

            return View("OrderComplete");
        }
    }
}
