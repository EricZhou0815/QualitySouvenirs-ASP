using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QualitySouvenirs.Data;
using QualitySouvenirs.Models;
using System.Net.Http.Headers;//Week 6
using Microsoft.AspNetCore.Hosting; //Week 6
using Microsoft.AspNetCore.Http; //Week 6
using System.IO; //Week 6

namespace QualitySouvenirs.Controllers
{
    public class SouvenirsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;//Week 6

        public SouvenirsController(ApplicationDbContext context, IHostingEnvironment hEnv)
        {
            _context = context;
            _hostingEnv = hEnv;
        }

        // GET: Souvenirs
        public async Task<IActionResult> Index(
            string sortOrder,
            string searchString,
            string currentFilter,
            int? page
            )
        {
            ViewData["SouvenirCurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DescriptionSortParm"] = sortOrder=="description" ? "description_desc" : "description";
            ViewData["PriceSortParm"] = sortOrder == "price" ? "price_desc" : "price";
            ViewData["CategorySortParm"] = sortOrder == "category" ? "category_desc" : "category";
            ViewData["SupplierSortParm"] = sortOrder == "supplier" ? "supplier_desc" : "supplier";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["SouvenirCurrentFilter"] = searchString;

            var souvenirs = from s in _context.Souvenirs
                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                souvenirs = souvenirs.Where(s => s.Name.Contains(searchString)
                                       || s.Description.Contains(searchString) || (s.CategoryName.Contains(searchString)) || (s.SupplierName.Contains(searchString)));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    souvenirs = souvenirs.OrderByDescending(s => s.Name);
                    break;
                case "description":
                    souvenirs = souvenirs.OrderBy(s => s.Description);
                    break;
                case "description_desc":
                    souvenirs = souvenirs.OrderByDescending(s => s.Description);
                    break;
                case "price":
                    souvenirs = souvenirs.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    souvenirs = souvenirs.OrderByDescending(s => s.Price);
                    break;
                case "category":
                    souvenirs = souvenirs.OrderBy(s => s.CategoryName);
                    break;
                case "category_desc":
                    souvenirs = souvenirs.OrderByDescending(s => s.CategoryName);
                    break;
                case "supplier":
                    souvenirs = souvenirs.OrderBy(s => s.SupplierName);
                    break;
                case "supplier_desc":
                    souvenirs = souvenirs.OrderByDescending(s => s.SupplierName);
                    break;


                default:
                    souvenirs = souvenirs.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<Souvenir>.CreateAsync(souvenirs.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Products
        public async Task<IActionResult> Shop(string searchString)
        {
            ViewData["searchProducts"] = searchString;
            var souvenirs = from s in _context.Souvenirs select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                souvenirs = souvenirs.Where(s => s.Name.Contains(searchString) || s.Description.Contains(searchString) || s.CategoryName.Contains(searchString) || s.SupplierName.Contains(searchString));
            }


            var ApplicationDbContext = souvenirs.Include(p => p.Category).Include(p => p.Supplier);
            return View(await ApplicationDbContext.AsNoTracking().ToListAsync());
        }



        // GET: Souvenirs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var souvenir = await _context.Souvenirs
                .SingleOrDefaultAsync(m => m.ID == id);
            if (souvenir == null)
            {
                return NotFound();
            }

            return View(souvenir);
        }

        public async Task<IActionResult> ShopItemDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var souvenir = await _context.Souvenirs
                .SingleOrDefaultAsync(m => m.ID == id);
            if (souvenir == null)
            {
                return NotFound();
            }
  
            return View(souvenir);
        }

        // GET: Souvenirs/Create
        public IActionResult Create()
        {
            ViewData["CategoryName"] = new SelectList(_context.Set<Category>(), "Name", "Name");
            ViewData["SupplierName"] = new SelectList(_context.Set<Supplier>(), "Name", "Name");
            return View();
        }

        // POST: Souvenirs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,Price,PathOfImage,SupplierName,CategoryName")] Souvenir souvenir, IList<IFormFile> _files)
        {
            ViewData["CategoryName"] = new SelectList(_context.Set<Category>(), "Name", "Name");
            ViewData["SupplierName"] = new SelectList(_context.Set<Supplier>(), "Name", "Name");
            var relativeName = "";
            var fileName = "";

            if (_files.Count < 1)
            {
                relativeName = "/images/Souvenirs/Default.png";
            }
            else
            {
                foreach (var file in _files)
                {
                    fileName = ContentDispositionHeaderValue
                                      .Parse(file.ContentDisposition)
                                      .FileName
                                      .Trim('"');
                    //Path for localhost
                    relativeName = "/images/Souvenirs/"+fileName;

                    using (FileStream fs = System.IO.File.Create(_hostingEnv.WebRootPath + relativeName))
                    {
                        await file.CopyToAsync(fs);
                        fs.Flush();
                    }
                }
            }
            souvenir.PathOfImage = fileName;
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(souvenir);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists " + "see your system administrator.");
            }
            return View(souvenir);
        }

        // GET: Souvenirs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var souvenir = await _context.Souvenirs.SingleOrDefaultAsync(m => m.ID == id);
            if (souvenir == null)
            {
                return NotFound();
            }
            ViewData["CategoryName"] = new SelectList(_context.Set<Category>(), "Name", "Name");
            ViewData["SupplierName"] = new SelectList(_context.Set<Supplier>(), "Name", "Name");
            return View(souvenir);
        }

        // POST: Souvenirs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,Price,PathOfImage,SupplierName,CategoryName")] Souvenir souvenir, IList<IFormFile> _files)
        {
            ViewData["CategoryName"] = new SelectList(_context.Set<Category>(), "Name", "Name");
            ViewData["SupplierName"] = new SelectList(_context.Set<Supplier>(), "Name", "Name");

            if (id != souvenir.ID)
            {
                return NotFound();
            }

            var relativeName = "";
            var fileName=souvenir.PathOfImage;

            if (_files.Count < 1)
            {
                relativeName = "/images/Souvenirs/Default.png";
            }
            else
            {
                foreach (var file in _files)
                {
                    fileName = ContentDispositionHeaderValue
                                      .Parse(file.ContentDisposition)
                                      .FileName
                                      .Trim('"');
                    //Path for localhost
                    relativeName = "/images/Souvenirs/"+ fileName;

                    using (FileStream fs = System.IO.File.Create(_hostingEnv.WebRootPath + relativeName))
                    {
                        await file.CopyToAsync(fs);
                        fs.Flush();
                    }
                }
            }
            souvenir.PathOfImage = fileName;

                if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(souvenir);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SouvenirExists(souvenir.ID))
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
            return View(souvenir);
        }

        // GET: Souvenirs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var souvenir = await _context.Souvenirs
                .SingleOrDefaultAsync(m => m.ID == id);
            if (souvenir == null)
            {
                return NotFound();
            }

            return View(souvenir);
        }

        // POST: Souvenirs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var souvenir = await _context.Souvenirs.SingleOrDefaultAsync(m => m.ID == id);
            _context.Souvenirs.Remove(souvenir);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SouvenirExists(int id)
        {
            return _context.Souvenirs.Any(e => e.ID == id);
        }
    }
}
