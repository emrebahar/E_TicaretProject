using E_TicaretProject.CustomExtension;
using E_TicaretProject.Entities;
using E_TicaretProject.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_TicaretProject.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CartRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void AddToCart(Product product)
        {
            var list = _httpContextAccessor.HttpContext.Session.GetObject<List<Product>>("Cart");

            if (list == null)
            {
                list = new List<Product>();
                list.Add(product);
            }
            else
            {
                list.Add(product);
            }

            _httpContextAccessor.HttpContext.Session.SetObject("Cart", list);

        }
        public void RemoveToCart(Product product)
        {

            var list = _httpContextAccessor.HttpContext.Session.GetObject<List<Product>>("Cart");

            list.Remove(product);

            _httpContextAccessor.HttpContext.Session.SetObject("Cart", list);
        }

        //Sepetteki ürünleri getir
        public List<Product> GetAllProduct()
        {

            var x = _httpContextAccessor.HttpContext.Session.GetObject<List<Product>>("Cart");
            return x;
        }
    }
}
