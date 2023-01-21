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
    [Authorize(Roles = "Employee,Owner")]
    public class BuildingsController : Controller
    {
        private readonly PropertyRentalDBContext _context;

        public BuildingsController(PropertyRentalDBContext context)
        {
            _context = context;
        }

        // GET: Buildings
        public async Task<IActionResult> Index()
        {
            var propertyRentalDBContext = _context.Buildings.Include(b => b.Adress);
            return View(await propertyRentalDBContext.ToListAsync());
        }

        // GET: Buildings/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildings = await _context.Buildings
                .Include(b => b.Adress)
                .FirstOrDefaultAsync(m => m.BuildingId == id);
            if (buildings == null)
            {
                return NotFound();
            }

            return View(buildings);
        }

        // GET: Buildings/Create
        public IActionResult Create()
        {
            ViewData["AdressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId");
            return View();
        }

        // POST: Buildings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BuildingId,BuildingName,AdressId")] Buildings buildings)
        {
            if (ModelState.IsValid)
            {
                _context.Add(buildings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", buildings.AdressId);
            return View(buildings);
        }

        // GET: Buildings/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildings = await _context.Buildings.FindAsync(id);
            if (buildings == null)
            {
                return NotFound();
            }
            ViewData["AdressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", buildings.AdressId);
            return View(buildings);
        }

        // POST: Buildings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("BuildingId,BuildingName,AdressId")] Buildings buildings)
        {
            if (id != buildings.BuildingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(buildings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuildingsExists(buildings.BuildingId))
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
            ViewData["AdressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", buildings.AdressId);
            return View(buildings);
        }

        // GET: Buildings/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildings = await _context.Buildings
                .Include(b => b.Adress)
                .FirstOrDefaultAsync(m => m.BuildingId == id);
            if (buildings == null)
            {
                return NotFound();
            }

            return View(buildings);
        }

        // POST: Buildings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var buildings = await _context.Buildings.FindAsync(id);
            _context.Buildings.Remove(buildings);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BuildingsExists(string id)
        {
            return _context.Buildings.Any(e => e.BuildingId == id);
        }
    }
}
