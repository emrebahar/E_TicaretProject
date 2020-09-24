using E_TicaretProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_TicaretProject.Interface
{
    public interface ICartRepository
    {
        void AddToCart(Product poduct);
        void RemoveToCart(Product poduct);
        List<Product> GetAllProduct();
    }
}
