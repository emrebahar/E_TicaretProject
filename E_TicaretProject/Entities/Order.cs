using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_TicaretProject.Entities
{
    public class Order : BaseEntity
    {
        public string OrderNumber { get; set; }
        public double Total { get; set; }
        public DateTime OrderDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Neighborhood { get; set; }
        public string PostCode { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
   
}
