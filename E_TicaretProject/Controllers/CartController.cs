using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using E_TicaretProject.CustomExtension;
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

        private readonly IHttpContextAccessor _httpContextAccessor;
        public CartController(E_TicaretDbContext db, ICartRepository cartRepository, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _cartRepository = cartRepository;
            _httpContextAccessor = httpContextAccessor;
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

        public IActionResult Index(string UserName)
        {
            var model = new List<Product>();
            model = _cartRepository.GetAllProduct();
            if (UserName != null)
            {
                ViewBag.UserName = UserName;
            }
            return View(model);
        }

        public IActionResult RemoveCart(int id)
        {

            var product = _db.Products.Where(x => x.Id == id).FirstOrDefault();
            _cartRepository.RemoveToCart(product);
            return RedirectToAction("Index");
        }
        public ActionResult Checkout(string UserName)
        {
            ViewBag.UserName = UserName;
            return View(new ShippingDetails());
        }

        //Siparişi tamamla(adres verileri vs giriliyor.)
        [HttpPost]
        public ActionResult Checkout(ShippingDetails entity)
        {
            if (entity.UserName == null)
            {
                ViewBag.Message = "Lütfen giriş yapınız";
                return View(new ShippingDetails());
            }
            else
            {
                var list = _httpContextAccessor.HttpContext.Session.GetObject<List<Product>>("Cart");

                if (list.Count == 0)
                {
                    ViewBag.UserName = entity.UserName;
                    ModelState.AddModelError("UrunYokError", "Sepetinizde ürün bulunmamaktadır..");
                }
                if (ModelState.IsValid)
                {
                    SaveOrder(list, entity);
                    //veri tabanına kaydet
                    // cart.Clear();
                    ViewBag.UserName = entity.UserName;
                    return View("Completed");
                }
                else
                {
                    ViewBag.UserName = entity.UserName;
                    return View(entity);
                }
            }


        }
        private void SaveOrder(List<Product> list, ShippingDetails entity)
        {

            var order = new Order();

            if (entity.UserName != null)
            {
                var user = _db.Users.Where(x => x.UserName == entity.UserName && !x.IsDeleted).FirstOrDefault();
                if (user != null)
                {
                    order.UserId = user.Id;
                    order.Address = entity.Adres;
                    order.City = entity.Sehir;
                    order.District = entity.Semt;
                    order.Neighborhood = entity.Mahalle;
                    order.OrderDate = DateTime.Now;

                    _db.Orders.Add(order);
                    _db.SaveChanges();

                    foreach (var item in list)
                    {
                        var ordDetail = new OrderDetail();
                        ordDetail.ProductId = item.Id;
                        ordDetail.OrderId = order.Id;

                        _db.OrderDetail.Add(ordDetail);
                        _db.SaveChanges();

                    }

                }
               
            }
        
           
        }

    }
}
