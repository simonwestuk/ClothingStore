using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ClothingStore.Models;

namespace ClothingStore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<CategoryModel> Categories { get; set; }

        public DbSet<TypeModel> Types { get; set; }

        public DbSet<ProductModel> Products { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
