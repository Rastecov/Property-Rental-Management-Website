using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Property_Rental_Management_Web_Site.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Property_Rental_Management_Web_Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly PropertyRentalDBContext _context;

        const string SessionId = "_Id";

        public HomeController(PropertyRentalDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult TenantsView()
        {
            var AptList = _context.Apartments.ToList();
            List<Apartments>Listofapartments= new List<Apartments>();   

            foreach (var apt in AptList)
            {
                Listofapartments.Add(_context.Apartments.FirstOrDefault(a=> a.ApartmentNumber== apt.ApartmentNumber));

            }
            return View(Listofapartments);
        }

        public IActionResult Login(string error)
        {
            if (error != null)
            {

                ViewData["ErrorMessage"] = error;
                ViewData["hasMessage"] = "True";


            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string userid, string password)
        {
            bool uservalidation = _context.Users1.Any(u => u.UserId == userid && u.Password == password);
            if (uservalidation)
            {
                string type = (from Us in _context.Users1 where Us.UserId == userid select Us.UserType).FirstOrDefault();
                
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Sid, userid.ToString()),
                        new Claim(ClaimTypes.Role, type.ToString()),
                    };
                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(claimsIdentity);
                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.Now.AddMinutes(30),
                };
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal, authProperties).Wait();
                if (type == "Tenants")
                {
                    HttpContext.Session.SetString(SessionId, userid);
                    Authentification.userLogged = _context.Users1.FirstOrDefault(x => x.UserId == userid)!;
                    return RedirectToAction("TenantsView", "Home");

                }

                if (type == "Employee")
                {
                    HttpContext.Session.SetString(SessionId, userid);
                    Authentification.userLogged = _context.Users1.FirstOrDefault(x => x.UserId == userid)!;
                    return RedirectToAction("Index", "Home");
                }

                if (type == "Owner")
                {
                    HttpContext.Session.SetString(SessionId, userid);
                    Authentification.userLogged = _context.Users1.FirstOrDefault(x => x.UserId == userid)!;
                    return RedirectToAction("Index", "Home");

                }

            }
            else
            {
                //return View();
            }
            return RedirectToAction("Login", new { Error = "Invalid Password or Id" });

        }

        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(Users1 user)
        {
            using (PropertyRentalDBContext context = new
            PropertyRentalDBContext())
            {
                context.Users1.Add(user);
                TempData["Message"] = "Signup was successfull";
                context.SaveChanges();
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // FormsAuthentication.SignOut();
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
