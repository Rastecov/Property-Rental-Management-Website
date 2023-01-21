using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Property_Rental_Management_Web_Site.Models;

namespace Property_Rental_Management_Web_Site.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        private readonly PropertyRentalDBContext _context;

        public MessagesController(PropertyRentalDBContext context)
        {
            _context = context;
        }

        // GET: Messages
        public async Task<IActionResult> Index()
        {
            ViewBag.Id = HttpContext.Session.GetString("_Id");
            var propertyRentalDBContext = _context.Messages.Include(m => m.Employee).Include(m => m.Tenant);
            return View(await propertyRentalDBContext.ToListAsync());
        }

        // GET: Messages/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messages = await _context.Messages
                .Include(m => m.Employee)
                .Include(m => m.Tenant)
                .FirstOrDefaultAsync(m => m.MessageId == id);
            if (messages == null)
            {
                return NotFound();
            }

            return View(messages);
        }

        // GET: Messages/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName");
            ViewData["TenantId"] = new SelectList(_context.Tenants1, "TenantId", "FirstName");
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MessageId,Message,TenantId,EmployeeId")] Messages messages)
        {

            if (ModelState.IsValid)
            {
                _context.Add(messages);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", messages.EmployeeId);
            ViewData["TenantId"] = new SelectList(_context.Tenants1, "TenantId", "TenantId", messages.TenantId);
            return View(messages);
        }

        // GET: Messages/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messages = await _context.Messages.FindAsync(id);
            if (messages == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", messages.EmployeeId);
            ViewData["TenantId"] = new SelectList(_context.Tenants1, "TenantId", "TenantId", messages.TenantId);
            return View(messages);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MessageId,Message,TenantId,EmployeeId")] Messages messages)
        {
            if (id != messages.MessageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(messages);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessagesExists(messages.MessageId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", messages.EmployeeId);
            ViewData["TenantId"] = new SelectList(_context.Tenants1, "TenantId", "TenantId", messages.TenantId);
            return View(messages);
        }

        // GET: Messages/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messages = await _context.Messages
                .Include(m => m.Employee)
                .Include(m => m.Tenant)
                .FirstOrDefaultAsync(m => m.MessageId == id);
            if (messages == null)
            {
                return NotFound();
            }

            return View(messages);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var messages = await _context.Messages.FindAsync(id);
            _context.Messages.Remove(messages);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessagesExists(string id)
        {
            return _context.Messages.Any(e => e.MessageId == id);
        }
    }
}
