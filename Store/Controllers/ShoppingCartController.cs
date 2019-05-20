using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Models;
using NLog;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Store.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ShoppingCart _shoppingCart;
        private readonly StoreContext _appDbContext;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ShoppingCartController(StoreContext movieRepository, ShoppingCart shoppingCart)
        {
            _appDbContext = movieRepository;
            _shoppingCart = shoppingCart;
        }

        
        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(shoppingCartViewModel);
        }


        public RedirectToActionResult AddToShoppingCart(int movieId)
        {
            var selectedMovie = _appDbContext.Movie.FirstOrDefault(p => p.ID == movieId);
            if (selectedMovie != null)
            {
                _shoppingCart.AddToCart(selectedMovie, 1);
            }
            logger.Debug("ADD to cart movie. ID:" + movieId);
            return RedirectToAction("Index", "Movies"); 
        }

        public RedirectToActionResult RemoveFromShoppingCart(int movieId)
        {
            var selectedMovie = _appDbContext.Movie.FirstOrDefault(p => p.ID == movieId);
            if (selectedMovie != null)
            {
                _shoppingCart.RemoveFromCart(selectedMovie);
            }
            logger.Debug("REMOVE from cart movie. ID:" + movieId);
            return RedirectToAction("Index");
        }
    }
}