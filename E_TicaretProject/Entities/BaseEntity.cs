using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_TicaretProject.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
