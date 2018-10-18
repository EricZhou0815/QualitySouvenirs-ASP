using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QualitySouvenirs.Data;
using QualitySouvenirs.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;


namespace QualitySouvenirs.Controllers
{
    [Authorize(Roles = "Admin,Member")]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public OrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Orders
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders.Include(i => i.User).AsNoTracking().ToListAsync());
        }

        // GET: Orders
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> MemberIndex(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.Where(i=>i.User.Id==id).Include(i=>i.OrderDetails).AsNoTracking().ToListAsync();
            if (orders == null)
            {
                return NotFound();
            }
            return View(orders);
        }


        // GET: Orders/Create
        [Authorize(Roles = "Member")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Create([Bind("City,Country,FirstName,LastName,Phone,PostalCode,State,Address")] Order order)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {

                ShoppingCart cart = ShoppingCart.GetCart(this.HttpContext);
                List<CartItem> items = cart.GetCartItems(_context);
                List<OrderDetail> details = new List<OrderDetail>();
                foreach (CartItem item in items)
                {

                    OrderDetail detail = CreateOrderDetailForThisItem(item);
                    detail.Order = order;
                    details.Add(detail);
                    _context.Add(detail);

                }

                order.User = user;
                order.Status = "Waiting";
                order.OrderDate = DateTime.Today;
                order.Total = ShoppingCart.GetCart(this.HttpContext).GetTotal(_context);
                order.GST = order.Total * (decimal)0.15;
                order.GrandTotal = order.Total + order.GST;
                order.OrderDetails = details;
                _context.SaveChanges();


                return RedirectToAction("Purchased", new RouteValueDictionary(
                new { action = "Purchased", id = order.OrderId }));
            }

            return View(order);
        }

        private OrderDetail CreateOrderDetailForThisItem(CartItem item)
        {

            OrderDetail detail = new OrderDetail();


            detail.Quantity = item.Count;
            detail.Souvenir = item.Souvenir;
            detail.UnitPrice = (decimal)item.Souvenir.Price;

            return detail;

        }
        public async Task<IActionResult> Purchased(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.Include(i => i.User).AsNoTracking().SingleOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            var details = _context.OrderDetails.Where(detail => detail.Order.OrderId == order.OrderId).Include(detail => detail.Souvenir).ToList();

            order.OrderDetails = details;
            ShoppingCart.GetCart(this.HttpContext).EmptyCart(_context);
            return View(order);
        }

        // GET: Orders/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.Include(i => i.User).AsNoTracking().SingleOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            var details = _context.OrderDetails.Where(detail => detail.Order.OrderId == order.OrderId).Include(detail => detail.Souvenir).ToList();

            order.OrderDetails = details;


            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(m => m.OrderId == id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> ChangeOrderStatus(int id)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(m => m.OrderId == id);
            if (order.Status=="Waiting")
            {
                order.Status = "Shipped";
            }
            else
            {
                order.Status = "Waiting";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Orders/Details/5
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.Include(i => i.User).AsNoTracking().SingleOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            var details = _context.OrderDetails.Where(detail => detail.Order.OrderId == order.OrderId).Include(detail => detail.Souvenir).ToList();

            order.OrderDetails = details;


            return View(order);
        }

        public IActionResult Back()
        {
            return Redirect(Request.Headers["Referer"].ToString());
        }



    }
}
