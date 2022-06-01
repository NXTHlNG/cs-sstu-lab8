using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using cs_sstu_lab8.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace cs_sstu_lab8.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product>? Product { get; set; }

        public DbSet<cs_sstu_lab8.Models.Order>? Order { get; set; }

        public DbSet<cs_sstu_lab8.Models.OrderItem>? OrderItem { get; set; }

    }
}
