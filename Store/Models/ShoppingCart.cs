using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Store.Models
{
    public class ShoppingCart
    {
        private readonly StoreContext _storeContext;
        private ShoppingCart(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            var context = services.GetService<StoreContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Movie movie, int amount)
        {
            var shoppingCartItem =
                    _storeContext.ShoppingCartItems
                    .SingleOrDefault(s => s.Movie.ID == movie.ID && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Movie = movie,
                    Amount = 1,
                    OrderTitleMovieShoppingCart = movie.Title
                };

                _storeContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _storeContext.SaveChanges();
        }

        public int RemoveFromCart(Movie movie)
        {
            var shoppingCartItem =
                    _storeContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Movie.ID == movie.ID && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _storeContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _storeContext.SaveChanges();

            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??
                   (ShoppingCartItems =
                       _storeContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                           .Include(s => s.Movie)
                           .ToList());
        }

        public void ClearCart()
        {
            var cartItems = _storeContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _storeContext.ShoppingCartItems.RemoveRange(cartItems);

            _storeContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _storeContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Movie.Price * c.Amount).Sum();
            return total;
        }
    }
}
