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
   // [Authorize(Roles = "Owner")]
    public class Users1Controller : Controller
    {
        
        private readonly PropertyRentalDBContext _context;

        public Users1Controller(PropertyRentalDBContext context)
        {
            _context = context;
        }




       
        // GET: Users1
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users1.ToListAsync());
        }

        // GET: Users1/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users1 = await _context.Users1
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (users1 == null)
            {
                return NotFound();
            }

            return View(users1);
        }

        // GET: Users1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Username,Password,UserType")] Users1 users1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(users1);
                await _context.SaveChangesAsync();
                TempData["Message"] = "User was created successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(users1);
        }

        // GET: Users1/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users1 = await _context.Users1.FindAsync(id);
            if (users1 == null)
            {
                return NotFound();
            }
            return View(users1);
        }

        // POST: Users1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserId,Username,Password,UserType")] Users1 users1)
        {
            if (id != users1.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Users1Exists(users1.UserId))
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
            return View(users1);
        }

        // GET: Users1/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users1 = await _context.Users1
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (users1 == null)
            {
                return NotFound();
            }

            return View(users1);
        }

        // POST: Users1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var users1 = await _context.Users1.FindAsync(id);
            
            if (users1.UserType == "Employee")
            {
                var userSchedule = _context.Schedules.FirstOrDefault(x => x.EmployeeId == users1.UserId);
                if (userSchedule != null)
                {
                    _context.Schedules.Remove(userSchedule);
                }

                var userappointment = _context.Appointments.FirstOrDefault(x => x.EmployeeId == users1.UserId);
                
                if (userappointment != null)
                {
                    _context.Appointments.Remove(userappointment);
                }

                var userRental = _context.Rentals.FirstOrDefault(x => x.EmployeeId == users1.UserId);
                if (userRental != null)
                {
                    _context.Rentals.Remove(userRental);
                }

                var userEmp = _context.Employees.FirstOrDefault(x => x.EmployeeId == users1.UserId);
                if (userEmp != null)
                {
                    _context.Employees.Remove(userEmp);
                }
            } 

            if (users1.UserType == "Tenants")
            {
                var userSchedule = _context.Schedules.FirstOrDefault(x => x.TenantId == users1.UserId);
                if (userSchedule != null)
                {
                    _context.Schedules.Remove(userSchedule);
                }

                var userappointment = _context.Appointments.FirstOrDefault(x => x.TenantId == users1.UserId);
                if (userappointment != null)
                {
                    _context.Appointments.Remove(userappointment);
                }

                var userRental = _context.Rentals.FirstOrDefault(x => x.TenantId == users1.UserId);
                if (userRental != null)
                {
                    _context.Rentals.Remove(userRental);
                }

                var userMessage = _context.Messages.FirstOrDefault(x => x.TenantId == users1.UserId);
                if (userMessage != null)
                {
                    _context.Messages.Remove(userMessage);
                }

                var userTenant = _context.Tenants1.FirstOrDefault(t => t.TenantId == users1.UserId);
                if (userTenant != null)
                {
                    _context.Tenants1.Remove(userTenant);
                }
            }

            
            _context.Users1.Remove(users1);
            await _context.SaveChangesAsync();
            TempData["Message"] = "User was deleted successfully!";

            return RedirectToAction(nameof(Index));
        }

        private bool Users1Exists(string id)
        {
            return _context.Users1.Any(e => e.UserId == id);
        }
    }
}
