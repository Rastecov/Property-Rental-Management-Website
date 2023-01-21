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
    public class ApartmentsController : Controller
    {
        private readonly PropertyRentalDBContext _context;

        public ApartmentsController(PropertyRentalDBContext context)
        {
            _context = context;
        }

        // GET: Apartments
        public async Task<IActionResult> Index()
        {
            var propertyRentalDBContext = _context.Apartments.Include(a => a.Building);
            return View(await propertyRentalDBContext.ToListAsync());
        }

        // GET: Apartments/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartments = await _context.Apartments
                .Include(a => a.Building)
                .FirstOrDefaultAsync(m => m.ApartmentNumber == id);
            if (apartments == null)
            {
                return NotFound();
            }

            return View(apartments);
        }

        // GET: Apartments/Create
        public IActionResult Create()
        {
            ViewData["BuildingId"] = new SelectList(_context.Buildings, "BuildingId", "BuildingId");
            return View();
        }

        // POST: Apartments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApartmentNumber,ApartmentType,Description,Floor,Status,BuildingId")] Apartments apartments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(apartments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BuildingId"] = new SelectList(_context.Buildings, "BuildingId", "BuildingId", apartments.BuildingId);
            return View(apartments);
        }

        // GET: Apartments/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartments = await _context.Apartments.FindAsync(id);
            if (apartments == null)
            {
                return NotFound();
            }
            ViewData["BuildingId"] = new SelectList(_context.Buildings, "BuildingId", "BuildingId", apartments.BuildingId);
            return View(apartments);
        }

        // POST: Apartments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ApartmentNumber,ApartmentType,Description,Floor,Status,BuildingId")] Apartments apartments)
        {
            if (id != apartments.ApartmentNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(apartments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApartmentsExists(apartments.ApartmentNumber))
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
            ViewData["BuildingId"] = new SelectList(_context.Buildings, "BuildingId", "BuildingId", apartments.BuildingId);
            return View(apartments);
        }

        // GET: Apartments/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartments = await _context.Apartments
                .Include(a => a.Building)
                .FirstOrDefaultAsync(m => m.ApartmentNumber == id);
            if (apartments == null)
            {
                return NotFound();
            }

            return View(apartments);
        }

        // POST: Apartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var apartments = await _context.Apartments.FindAsync(id);
            _context.Apartments.Remove(apartments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApartmentsExists(string id)
        {
            return _context.Apartments.Any(e => e.ApartmentNumber == id);
        }
    }
}
