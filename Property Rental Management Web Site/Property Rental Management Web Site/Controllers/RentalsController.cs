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
    public class RentalsController : Controller
    {
        private readonly PropertyRentalDBContext _context;

        public RentalsController(PropertyRentalDBContext context)
        {
            _context = context;
        }

        // GET: Rentals
        public async Task<IActionResult> Index()
        {
            var propertyRentalDBContext = _context.Rentals.Include(r => r.Apartment).Include(r => r.Employee).Include(r => r.Tenant);
            return View(await propertyRentalDBContext.ToListAsync());
        }

        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentals = await _context.Rentals
                .Include(r => r.Apartment)
                .Include(r => r.Employee)
                .Include(r => r.Tenant)
                .FirstOrDefaultAsync(m => m.RentalId == id);
            if (rentals == null)
            {
                return NotFound();
            }

            return View(rentals);
        }

        // GET: Rentals/Create
        public IActionResult Create()
        {
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "ApartmentNumber", "ApartmentNumber");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            ViewData["TenantId"] = new SelectList(_context.Tenants1, "TenantId", "TenantId");
            return View();
        }

        // POST: Rentals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentalId,RentalPrice,StartDate,Enddate,ApartmentId,TenantId,EmployeeId")] Rentals rentals)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rentals);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "ApartmentNumber", "ApartmentNumber", rentals.ApartmentId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", rentals.EmployeeId);
            ViewData["TenantId"] = new SelectList(_context.Tenants1, "TenantId", "TenantId", rentals.TenantId);
            return View(rentals);
        }

        // GET: Rentals/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentals = await _context.Rentals.FindAsync(id);
            if (rentals == null)
            {
                return NotFound();
            }
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "ApartmentNumber", "ApartmentNumber", rentals.ApartmentId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", rentals.EmployeeId);
            ViewData["TenantId"] = new SelectList(_context.Tenants1, "TenantId", "TenantId", rentals.TenantId);
            return View(rentals);
        }

        // POST: Rentals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("RentalId,RentalPrice,StartDate,Enddate,ApartmentId,TenantId,EmployeeId")] Rentals rentals)
        {
            if (id != rentals.RentalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rentals);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentalsExists(rentals.RentalId))
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
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "ApartmentNumber", "ApartmentNumber", rentals.ApartmentId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", rentals.EmployeeId);
            ViewData["TenantId"] = new SelectList(_context.Tenants1, "TenantId", "TenantId", rentals.TenantId);
            return View(rentals);
        }

        // GET: Rentals/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentals = await _context.Rentals
                .Include(r => r.Apartment)
                .Include(r => r.Employee)
                .Include(r => r.Tenant)
                .FirstOrDefaultAsync(m => m.RentalId == id);
            if (rentals == null)
            {
                return NotFound();
            }

            return View(rentals);
        }

        // POST: Rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var rentals = await _context.Rentals.FindAsync(id);
            _context.Rentals.Remove(rentals);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentalsExists(string id)
        {
            return _context.Rentals.Any(e => e.RentalId == id);
        }
    }
}
