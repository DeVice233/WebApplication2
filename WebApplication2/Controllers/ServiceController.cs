using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication2.Controllers
{
    public class ServiceController : Controller
    {

        private ApplicationContext _context;
        public ServiceController(ApplicationContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "admin")]
        public IActionResult CreateService()
        {
            return View();
        }
     
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> DetailsService(int? id)
        {
            if (id != null)
            {
                Service service = await _context.Services.FirstOrDefaultAsync(p => p.Id == id);
                if (service != null)
                    return View(service);
            }
            return NotFound();
        }
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Services()
        {
            return View(await _context.Services.ToListAsync());
        }
        [HttpPost, Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateService(Service service)
        {
            _context.Services.Add(service);
            await _context.SaveChangesAsync();
            return RedirectToAction("Services");
        }
       
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditService(int? id)
        {
            if (id != null)
            {
                Service service = await _context.Services.FirstOrDefaultAsync(p => p.Id == id);
                if (service != null)
                    return View(service);
            }
            return NotFound();
        }

        [HttpPost, Authorize(Roles = "admin")]
        public async Task<IActionResult> EditService(Service service)
        {
            _context.Services.Update(service);
            await _context.SaveChangesAsync();
            return RedirectToAction("Services");
        }

        [HttpGet]
        [ActionName("DeleteService"), Authorize(Roles = "admin")]
        public async Task<IActionResult> ConfirmDeleteService(int? id)
        {
            if (id != null)
            {
                Service service = await _context.Services.FirstOrDefaultAsync(p => p.Id == id);
                if (service != null)
                    return View(service);
            }
            return NotFound();
        }
        [HttpPost]
        [ActionName("DeleteService"), Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteService(int? id)
        {
            if (id != null)
            {
                Service service = new Service { Id = id.Value };
                _context.Entry(service).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return RedirectToAction("Services");
            }
            return NotFound();
        }

        public IActionResult AddOrder(int? id)
        {
            var email = User.Identity.Name;
            if (id != null)
            {
                User user =_context.Users.FirstOrDefault(p => p.Email == email);
                Service service = _context.Services.FirstOrDefault(p => p.Id == id);
                Order order = new Order { ServiceId = service.Id, UserId = user.Id };
               _context.Orders.Add(order);
                 _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return NotFound();
        }
    }
}
