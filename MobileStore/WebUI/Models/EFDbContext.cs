
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class EFDbContext : DbContext
    {
        public DbSet<MobilePhone> MobilePhones { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartLine> CartLines { get; set; }
        public DbSet<UpdateDate> UpdateDates { get; set; }


    }
}
