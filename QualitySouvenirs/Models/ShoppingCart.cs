using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QualitySouvenirs.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace QualitySouvenirs.Models
{
    public class ShoppingCart
    {
        public string ShoppingCartId { get; set; }
        public const string CartSessionKey = "cartId";
        public static ShoppingCart GetCart(HttpContext context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        //bind to additem: add item to ShoppingCart
        public void AddToCart(Souvenir souvenir, ApplicationDbContext db)
        {
            var cartItem = db.CartItems.SingleOrDefault(c => c.CartID == ShoppingCartId && c.Souvenir.ID == souvenir.ID);
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    Souvenir = souvenir,
                    CartID = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                db.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Count++;
            }
            db.SaveChanges();
        }

        //bind to minusitem
        public int RemoveFromCart(int id, ApplicationDbContext db)
        {
            var cartItem = db.CartItems.SingleOrDefault(cart => cart.CartID == ShoppingCartId && cart.Souvenir.ID == id);
            int itemCount = 0;
            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    db.CartItems.Remove(cartItem);
                }
                db.SaveChanges();
            }
            return itemCount;
        }

        public void EmptyCart(ApplicationDbContext db)
        {
            var cartItems = db.CartItems.Where(cart => cart.CartID == ShoppingCartId);
            foreach (var cartItem in cartItems)
            {
                db.CartItems.Remove(cartItem);
            }
            db.SaveChanges();
        }

        public List<CartItem> GetCartItems(ApplicationDbContext db)
        {
            List<CartItem> cartItems = db.CartItems.Include(i => i.Souvenir).Where(cartItem => cartItem.CartID == ShoppingCartId).ToList();

            return cartItems;

        }

        public int GetCount(ApplicationDbContext db)
        {
            int? count =
                (from cartItems in db.CartItems where cartItems.CartID == ShoppingCartId select (int?)cartItems.Count).Sum();
            return count ?? 0;
        }

        public decimal GetTotal(ApplicationDbContext db)
        {
            decimal? total = (decimal?)(from cartItems in db.CartItems
                              where cartItems.CartID == ShoppingCartId
                              select (int?)cartItems.Count * cartItems.Souvenir.Price).Sum();
            return total ?? decimal.Zero;
        }

        public string GetCartId(HttpContext context)
        {
            if (context.Session.GetString(CartSessionKey) == null)
            {
                Guid tempCartId = Guid.NewGuid();
                context.Session.SetString(CartSessionKey, tempCartId.ToString());
            }
            return context.Session.GetString(CartSessionKey).ToString();
        }
    }


}
