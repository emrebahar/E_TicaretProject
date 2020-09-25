using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_TicaretProject.Entities
{
    public class User :BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        //Kullanıcının siparişleri
        //Navigation Property
        public virtual List<Order> Orders { get; set; } = new List<Order>();
    }
}
