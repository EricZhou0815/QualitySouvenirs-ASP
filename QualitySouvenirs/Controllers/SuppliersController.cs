using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QualitySouvenirs.Data;
using QualitySouvenirs.Models;

namespace QualitySouvenirs.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SuppliersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Suppliers
        public async Task<IActionResult> Index(
            string sortOrder,
            string searchString,
            string currentFilter,
            int? page
            )
        {
            ViewData["SupplierCurrentSort"] = sortOrder;
            ViewData["SupplierNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["SupplierEmailSortParm"] = sortOrder == "email" ? "email_desc" : "email";
            ViewData["SupplierContactSortParm"] = sortOrder == "contact" ? "contact_desc" : "contact";
            ViewData["SupplierPhoneSortParm"] = sortOrder == "phone" ? "phone_desc" : "phone";
            ViewData["SupplierAddressSortParm"] = sortOrder == "address" ? "address_desc" : "address";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["SupplierCurrentFilter"] = searchString;

            var suppliers = from s in _context.Suppliers
                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                suppliers = suppliers.Where(s => s.Name.Contains(searchString)
                                       || s.Email.Contains(searchString) || s.Address.Contains(searchString) || (s.ContactPerson.Contains(searchString)) || (s.Phone.Contains(searchString)));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    suppliers = suppliers.OrderByDescending(s => s.Name);
                    break;
                case "email":
                    suppliers = suppliers.OrderBy(s => s.Email);
                    break;
                case "email_desc":
                    suppliers = suppliers.OrderByDescending(s => s.Email);
                    break;
                case "contact":
                    suppliers = suppliers.OrderBy(s => s.ContactPerson);
                    break;
                case "contact_desc":
                    suppliers = suppliers.OrderByDescending(s => s.ContactPerson);
                    break;
                case "phone":
                    suppliers = suppliers.OrderBy(s => s.Phone);
                    break;
                case "phone_desc":
                    suppliers = suppliers.OrderByDescending(s => s.Phone);
                    break;
                case "address":
                    suppliers = suppliers.OrderBy(s => s.Address);
                    break;
                case "address_desc":
                    suppliers = suppliers.OrderByDescending(s => s.Address);
                    break;


                default:
                    suppliers = suppliers.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<Supplier>.CreateAsync(suppliers.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Suppliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers
                .SingleOrDefaultAsync(m => m.ID == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // GET: Suppliers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Email,ContactPerson,Phone,Address")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        // GET: Suppliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers.SingleOrDefaultAsync(m => m.ID == id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Email,ContactPerson,Phone,Address")] Supplier supplier)
        {
            if (id != supplier.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supplier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierExists(supplier.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        // GET: Suppliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers
                .SingleOrDefaultAsync(m => m.ID == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supplier = await _context.Suppliers.SingleOrDefaultAsync(m => m.ID == id);
            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierExists(int id)
        {
            return _context.Suppliers.Any(e => e.ID == id);
        }
    }
}
