using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Property_Rental_Management_Web_Site.Models;

namespace Property_Rental_Management_Web_Site.Controllers
{
    [Authorize]
    public class Tenants1Controller : Controller
    {
        private readonly PropertyRentalDBContext _context;

        public Tenants1Controller(PropertyRentalDBContext context)
        {
            _context = context;
        }

        // GET: Tenants1
        public async Task<IActionResult> Index()
        {
            var propertyRentalDBContext = _context.Tenants1.Include(t => t.Tenant);
            return View(await propertyRentalDBContext.ToListAsync());
        }

        // GET: Tenants1/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenants1 = await _context.Tenants1
                .Include(t => t.Tenant)
                .FirstOrDefaultAsync(m => m.TenantId == id);
            if (tenants1 == null)
            {
                return NotFound();
            }

            return View(tenants1);
        }

        // GET: Tenants1/Create
        public IActionResult Create()
        {
            ViewData["TenantId"] = new SelectList(_context.Users1, "UserId", "UserId");
            return View();
        }

        // POST: Tenants1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenantId,FirstName,LastName,Email,PhoneNumber")] Tenants1 tenants1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tenants1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TenantId"] = new SelectList(_context.Users1, "UserId", "UserId", tenants1.TenantId);
            return View(tenants1);
        }

        // GET: Tenants1/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenants1 = await _context.Tenants1.FindAsync(id);
            if (tenants1 == null)
            {
                return NotFound();
            }
            ViewData["TenantId"] = new SelectList(_context.Users1, "UserId", "UserId", tenants1.TenantId);
            return View(tenants1);
        }

        // POST: Tenants1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TenantId,FirstName,LastName,Email,PhoneNumber")] Tenants1 tenants1)
        {
            if (id != tenants1.TenantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tenants1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Tenants1Exists(tenants1.TenantId))
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
            ViewData["TenantId"] = new SelectList(_context.Users1, "UserId", "UserId", tenants1.TenantId);
            return View(tenants1);
        }

        // GET: Tenants1/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenants1 = await _context.Tenants1
                .Include(t => t.Tenant)
                .FirstOrDefaultAsync(m => m.TenantId == id);
            if (tenants1 == null)
            {
                return NotFound();
            }

            return View(tenants1);
        }

        // POST: Tenants1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tenants1 = await _context.Tenants1.FindAsync(id);
            _context.Tenants1.Remove(tenants1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Tenants1Exists(string id)
        {
            return _context.Tenants1.Any(e => e.TenantId == id);
        }
    }
}
