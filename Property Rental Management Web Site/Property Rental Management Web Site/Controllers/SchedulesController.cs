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
    public class SchedulesController : Controller
    {
        private readonly PropertyRentalDBContext _context;

        public SchedulesController(PropertyRentalDBContext context)
        {
            _context = context;
        }

        // GET: Schedules
        public async Task<IActionResult> Index()
        {
            var propertyRentalDBContext = _context.Schedules.Include(s => s.Appointment).Include(s => s.Employee).Include(s => s.Tenant);
            return View(await propertyRentalDBContext.ToListAsync());
        }

        // GET: Schedules/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedules = await _context.Schedules
                .Include(s => s.Appointment)
                .Include(s => s.Employee)
                .Include(s => s.Tenant)
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (schedules == null)
            {
                return NotFound();
            }

            return View(schedules);
        }

        // GET: Schedules/Create
        public IActionResult Create()
        {
            ViewData["AppointmentId"] = new SelectList(_context.Appointments, "AppointmentId", "AppointmentId");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            ViewData["TenantId"] = new SelectList(_context.Tenants1, "TenantId", "TenantId");
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScheduleId,ScheduleDate,ScheduleTime,TenantId,EmployeeId,AppointmentId")] Schedules schedules)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schedules);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppointmentId"] = new SelectList(_context.Appointments, "AppointmentId", "AppointmentId", schedules.AppointmentId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", schedules.EmployeeId);
            ViewData["TenantId"] = new SelectList(_context.Tenants1, "TenantId", "TenantId", schedules.TenantId);
            return View(schedules);
        }

        // GET: Schedules/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedules = await _context.Schedules.FindAsync(id);
            if (schedules == null)
            {
                return NotFound();
            }
            ViewData["AppointmentId"] = new SelectList(_context.Appointments, "AppointmentId", "AppointmentId", schedules.AppointmentId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", schedules.EmployeeId);
            ViewData["TenantId"] = new SelectList(_context.Tenants1, "TenantId", "TenantId", schedules.TenantId);
            return View(schedules);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ScheduleId,ScheduleDate,ScheduleTime,TenantId,EmployeeId,AppointmentId")] Schedules schedules)
        {
            if (id != schedules.ScheduleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schedules);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchedulesExists(schedules.ScheduleId))
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
            ViewData["AppointmentId"] = new SelectList(_context.Appointments, "AppointmentId", "AppointmentId", schedules.AppointmentId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", schedules.EmployeeId);
            ViewData["TenantId"] = new SelectList(_context.Tenants1, "TenantId", "TenantId", schedules.TenantId);
            return View(schedules);
        }

        // GET: Schedules/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedules = await _context.Schedules
                .Include(s => s.Appointment)
                .Include(s => s.Employee)
                .Include(s => s.Tenant)
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (schedules == null)
            {
                return NotFound();
            }

            return View(schedules);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var schedules = await _context.Schedules.FindAsync(id);
            _context.Schedules.Remove(schedules);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchedulesExists(string id)
        {
            return _context.Schedules.Any(e => e.ScheduleId == id);
        }
    }
}
