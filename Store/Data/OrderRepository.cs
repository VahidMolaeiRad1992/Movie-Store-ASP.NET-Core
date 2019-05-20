using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Data.Interfaces;
using Store.Models;


namespace Store.Models
{
   
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreContext _storeDbContext;
        private readonly ShoppingCart _shoppingCart;


        public OrderRepository(StoreContext appDbContext, ShoppingCart shoppingCart)
        {
            _storeDbContext = appDbContext;
            _shoppingCart = shoppingCart;
        }


        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;
            order.OrderTotal = _shoppingCart.GetShoppingCartTotal();

            var title = _shoppingCart.ShoppingCartItems
                 .Select(s => s.OrderTitleMovieShoppingCart)
                 .ToList();

            string output = string.Join(", ", title.ToArray());
            order.OrderTitleMovie = output;


            _storeDbContext.Orders.Add(order);

            _storeDbContext.SaveChanges();
        }
    }

}
