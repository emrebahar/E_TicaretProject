using E_TicaretProject.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_TicaretProject.Configuration
{
    public class UserCartConfiguration : BaseConfiguration<UserCart>
    {
        public override void Configure(EntityTypeBuilder<UserCart> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.User)
            .WithMany(x => x.UserCarts)
            .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Product)
           .WithMany(x => x.CartList)
           .HasForeignKey(x => x.ProductId);
        }
    }
}
