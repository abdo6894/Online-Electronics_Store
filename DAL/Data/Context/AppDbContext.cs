using BL.Data.Configuration;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
