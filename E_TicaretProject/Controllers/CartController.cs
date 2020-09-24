using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using E_TicaretProject.DbContexts;
using E_TicaretProject.Entities;
using E_TicaretProject.Interface;
using E_TicaretProject.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_TicaretProject.Controllers
{
    public class CartController : Controller
    {
        private readonly E_TicaretDbContext _db;
        private readonly ICartRepository _cartRepository;
        public CartController(E_TicaretDbContext db, ICartRepository cartRepository)
        {
            _db = db;
            _cartRepository = cartRepository;
        }

        public IActionResult AddToCart(int Id, string UserName)
        {

            var product = _db.Products.Where(x => x.Id == Id && !x.IsDeleted).FirstOrDefault();
            _cartRepository.AddToCart(product);
            TempData["Info"] = "Ürün sepete eklendi";

            if (UserName != null)
            {
                var userId = _db.Users.Where(x => x.UserName == UserName).FirstOrDefault();
                return RedirectToAction("Index", "Home", userId);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Index(string UserName) {
            var model = new List<Product>();
            model = _cartRepository.GetAllProduct();
            if (UserName!=null)
            {
                ViewBag.UserName = UserName;
            }
            return View(model);
        }

        public IActionResult RemoveCart(int id) {

            var product = _db.Products.Where(x => x.Id == id).FirstOrDefault();
            _cartRepository.RemoveToCart(product);
            return RedirectToAction("Index");
        }

    }
}
