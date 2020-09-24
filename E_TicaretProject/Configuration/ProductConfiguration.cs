using E_TicaretProject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_TicaretProject.Configuration
{
    public class ProductConfiguration : BaseConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder.Metadata.FindNavigation(nameof(Product.CartList)).SetPropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}
