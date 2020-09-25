using E_TicaretProject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_TicaretProject.Configuration
{
    public class UserConfiguration :BaseConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Metadata.FindNavigation(nameof(User.Orders)).SetPropertyAccessMode(PropertyAccessMode.Field);


        }
    }
}
