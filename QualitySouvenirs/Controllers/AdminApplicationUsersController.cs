using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QualitySouvenirs.Data;
using QualitySouvenirs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;


namespace QualitySouvenirs.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminApplicationUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminApplicationUsersController(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        }

        // GET: AdminApplicationUsers
        public IActionResult Index()
        {
            IEnumerable<ApplicationUser> members = ReturnAllMembers().Result;
            return View(members);
        }

        public async Task<IEnumerable<ApplicationUser>> ReturnAllMembers()
        {
            var users = await _userManager.GetUsersInRoleAsync("Member");
            return users;
        }

        public async Task<IActionResult> EnableDisable(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            IEnumerable<ApplicationUser> members = ReturnAllMembers().Result;
            ApplicationUser member = (ApplicationUser)members.Single(u => u.Id == id);
            if (member == null)
            {
                return NotFound();
            }
            member.Enabled = !member.Enabled;
            _context.Update(member);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            IEnumerable<ApplicationUser> members = ReturnAllMembers().Result;
            ApplicationUser member = (ApplicationUser)members.Single(u => u.Id == id);
            var orders = await _context.Orders.Where(i => i.User.Id == id).ToListAsync();
            if (orders!=null)
            {
                foreach (Order order in orders)
                {
                    _context.Orders.Remove(order);
                    
                }
                await _context.SaveChangesAsync();
            }
            await _userManager.DeleteAsync(member);
            return RedirectToAction("Index");
        }


        // GET: ApplicationUsersNew/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }
            return View(applicationUser);
        }

        // POST: ApplicationUsersNew/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Enabled,Address,UName,MobileNumber,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] ApplicationUser applicationUser)
        {
            if (id != applicationUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                    _context.Update(applicationUser);
                    await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(applicationUser);
        }

        // GET: ApplicationUsersNew/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // POST: ApplicationUsersNew/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            _context.ApplicationUser.Remove(applicationUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
