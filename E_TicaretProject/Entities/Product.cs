using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace E_TicaretProject.Entities
{
    public class Product :BaseEntity ,IEquatable<Product>
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public virtual List<UserCart> CartList { get; set; } = new List<UserCart>();

        public bool Equals([AllowNull] Product other)
        {
            return Id == other.Id && Name == other.Name && ImageUrl == other.ImageUrl && Price == other.Price && Description == other.Description;
        }
    }
}
