using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QualitySouvenirs.Data;
using QualitySouvenirs.Models;
using Microsoft.AspNetCore.Authorization;


namespace QualitySouvenirs.Controllers
{
    [AllowAnonymous]
    [Authorize(Roles = "Member")]

    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            // Return the view
            return View(cart);
        }

        //
        // GET: /Store/AddToCart/5
        public IActionResult AddToCart(int id)
        {
            // Retrieve the album from the database
            var addedSouvenir =_context.Souvenirs
                .SingleOrDefault(m => m.ID == id);
            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.AddToCart(addedSouvenir, _context);

          return RedirectToAction("Shop", "Souvenirs");
          
        }

        // GET: /Store/AddToCartStay/5
        public JsonResult AddToCartStay(int id)
        {
            // Retrieve the album from the database
            var addedSouvenir = _context.Souvenirs
                .SingleOrDefault(m => m.ID == id);
            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.AddToCart(addedSouvenir, _context);
            // Go back to the main store page for more shopping
            //return Redirect(Request.Headers["Referer"].ToString());
            return Json(addedSouvenir);
        }

        public ActionResult RemoveFromCart(int id)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            int itemCount = cart.RemoveFromCart(id, _context);
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public int RemoveFromCartStay(int id)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            int itemCount = cart.RemoveFromCart(id, _context);
            return itemCount;
        }


        public ActionResult EmptyCart()
        {

            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.EmptyCart(_context);
            // Go back to the main store page for more shopping
            //return Redirect(Request.Headers["Referer"].ToString());
            return Redirect(Request.Headers["Referer"].ToString());
        }

    }
}