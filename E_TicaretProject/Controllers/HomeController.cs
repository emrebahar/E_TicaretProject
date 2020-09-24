using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using E_TicaretProject.Models;
using E_TicaretProject.DbContexts;

namespace E_TicaretProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly E_TicaretDbContext _db;

        public HomeController(E_TicaretDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(int? userId)  
        {
            if (userId != null)
            {
                var user = _db.Users.Where(x => !x.IsDeleted && x.Id == userId).FirstOrDefault();

                ViewBag.UserName = user.UserName;
                ViewBag.UserId = userId;
            }
            ViewBag.ProductList = _db.Products.Where(x => !x.IsDeleted).ToList();
            return View();
        }

        public IActionResult ProductDetail(int? UserId,int ProductId) {

            if (UserId != null)
            {
                var user = _db.Users.Where(x => !x.IsDeleted && x.Id == UserId).FirstOrDefault();

                ViewBag.UserName = user.UserName;
                ViewBag.Id = UserId;
            }
            ViewBag.ProductDetail = _db.Products.Where(x => !x.IsDeleted && x.Id==ProductId).FirstOrDefault();
            return View();

        }
    }
}
