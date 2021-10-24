using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            db = context;
        }

        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Index()
        {
            string role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
            //return Content($"ваша роль: {role}");
            ViewData["role"] = $"Ваша роль: {role}";
            return View(await db.Users.ToListAsync()); 
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Users()
        {
            return View(await db.Users.ToListAsync());
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Orders()
        {
            List<Service> Services = await db.Services.ToListAsync();
            List<User> Users = await db.Users.ToListAsync();
            return View(await db.Orders.ToListAsync());
        }
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        public IActionResult CreateService()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        
        public IActionResult ForPushkin()
        {
            return View();
        }
        public IActionResult Contacts()
        {
            return View();
        }
        
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Services()
        {
            return View(await db.Services.ToListAsync());
        }
       
        [HttpPost, Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(User user)
        {
            user.Role = await db.Roles.FirstOrDefaultAsync(p => p.Name == "user");
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(p => p.Id == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(p => p.Id == id);
                if(id == 1)
                {
                    return RedirectToAction("Index");
                }
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }
        
        [HttpPost, Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(User user)
        {
            user.Role = await db.Roles.FirstOrDefaultAsync(p => p.Name == "user");
            db.Users.Update(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        

        [HttpGet]
        [ActionName("Delete"), Authorize(Roles = "admin")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                if (id == 1)
                {
                    return RedirectToAction("Index");
                }
                User user = await db.Users.FirstOrDefaultAsync(p => p.Id == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }
        [HttpPost, Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                User user = new User { Id = id.Value };
                db.Entry(user).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
