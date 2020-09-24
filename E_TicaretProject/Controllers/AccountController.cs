using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_TicaretProject.DbContexts;
using E_TicaretProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_TicaretProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly E_TicaretDbContext _db;
        public AccountController(E_TicaretDbContext db)
        {
            _db = db;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserModel model)
        {
            var user = _db.Users.Where(x => !x.IsDeleted && x.Email == model.Email && x.Password == model.Password).FirstOrDefault();

            if (user != null)
            {
                return RedirectToAction("Index", "Home", new { userId = user.Id });

            }
            ViewBag.Message = "Email veya şifre hatalı";
            return View();
        }

        public ActionResult Logout() {

            return RedirectToAction("Index", "Home");
        }
    }
}
