using E_TicaretProject.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_TicaretProject.DbContexts
{
    public static class E_TicaretDbSeed
    {
        public static async void Initialize(IServiceProvider serviceProvider, bool create = false)
        {
            using var serviceScope = serviceProvider.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetService<E_TicaretDbContext>();

            dbContext.Database.Migrate();

            if (dbContext.Users.Any())
            {
                return;
            }


            //User appUser = new User
            //{
            //    UserName = "emre bahar",
            //    Email = "emrebahar@hotmail.com"
            //};

            //IdentityResult result = await UserManager.CreateAsync(appUser, "12345");

            var user = new User();
            user.UserName = "Emre BAHAR";
            user.Email = "emrebahar@hotmail.com";
            user.CreatedDate = DateTime.Now;
            user.Password = "12345";

            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            if (dbContext.Products.Any())
            {
                return;
            }

            List<string> productName = new List<string>() { "Bilgisayar", "Elma","Elbise","Portakal"};
            List<double> productPrice = new List<double>() { 5000, 8, 88, 6 };
            List<string> imageUrl = new List<string>() { "iconfinder_computer_1055084.png", "iconfinder_icon_fruit_maca_3316506.png", "iconfinder_shopping-shop-buy-discount-08_4038857.png", "iconfinder_Orange_2137820.png" };

            for (int i = 0; i < 4; i++)
            {
                var product = new Product();
                product.Name = productName[i];
                product.Price = productPrice[i];
                product.CreatedDate = DateTime.Now;
                product.ImageUrl = imageUrl[i];
                product.Description = "MacOS işletim sistemine sahip bilgisayarlar, Windows'ta olduğu kadar oyun imkânı sunmasa da daha güvenli kullanıcı deneyimi vadetmektedir. macOS Catalina sürümüyle birçok yeniliği beraberinde getiren Apple, iOS işletim sistemine sahip diğer cihazlarla uyumlu olmasıyla da çok fazla tercih edilmektedir.";
                dbContext.Products.Add(product);
                dbContext.SaveChanges();

            }




        }
    }
}
