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
    public class PropertiesController : Controller
    {
        private readonly PropertyRentalDBContext _context;

        public PropertiesController(PropertyRentalDBContext context)
        {
            _context = context;
        }

        // GET: Properties
        public async Task<IActionResult> Index()
        {
            var propertyRentalDBContext = _context.Properties.Include(p => p.Adress).Include(p => p.Building);
            return View(await propertyRentalDBContext.ToListAsync());
        }

        // GET: Properties/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var properties = await _context.Properties
                .Include(p => p.Adress)
                .Include(p => p.Building)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (properties == null)
            {
                return NotFound();
            }

            return View(properties);
        }

        // GET: Properties/Create
        public IActionResult Create()
        {
            ViewData["AdressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId");
            ViewData["BuildingId"] = new SelectList(_context.Buildings, "BuildingId", "BuildingId");
            return View();
        }

        // POST: Properties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,BuildingId,AdressId")] Properties properties)
        {
            if (ModelState.IsValid)
            {
                _context.Add(properties);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", properties.AdressId);
            ViewData["BuildingId"] = new SelectList(_context.Buildings, "BuildingId", "BuildingId", properties.BuildingId);
            return View(properties);
        }

        // GET: Properties/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var properties = await _context.Properties.FindAsync(id);
            if (properties == null)
            {
                return NotFound();
            }
            ViewData["AdressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", properties.AdressId);
            ViewData["BuildingId"] = new SelectList(_context.Buildings, "BuildingId", "BuildingId", properties.BuildingId);
            return View(properties);
        }

        // POST: Properties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserId,BuildingId,AdressId")] Properties properties)
        {
            if (id != properties.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(properties);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertiesExists(properties.UserId))
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
            ViewData["AdressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", properties.AdressId);
            ViewData["BuildingId"] = new SelectList(_context.Buildings, "BuildingId", "BuildingId", properties.BuildingId);
            return View(properties);
        }

        // GET: Properties/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var properties = await _context.Properties
                .Include(p => p.Adress)
                .Include(p => p.Building)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (properties == null)
            {
                return NotFound();
            }

            return View(properties);
        }

        // POST: Properties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var properties = await _context.Properties.FindAsync(id);
            _context.Properties.Remove(properties);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertiesExists(string id)
        {
            return _context.Properties.Any(e => e.UserId == id);
        }
    }
}
