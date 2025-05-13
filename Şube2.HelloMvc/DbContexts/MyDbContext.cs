using Microsoft.EntityFrameworkCore;
using Şube2.HelloMvc.Models;

namespace Şube2.HelloMvc.DbContexts
{
    public class MyDbContext : DbContext
    {
        public DbSet<Ogrenci> Ogrenciler { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=HelloMvcDB;Integrated Security=true;TrustServerCertificate=true");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Ogrenci>().Property(o => o.Ad).HasMaxLength(25).IsRequired();
            modelBuilder.Entity<Ogrenci>().Property(o => o.Soyad).HasMaxLength(30).IsRequired();
        }
    }
}