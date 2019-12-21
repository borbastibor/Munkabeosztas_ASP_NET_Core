using Microsoft.EntityFrameworkCore;
using Munkabeosztas_ASP_NET_Core.Models;

namespace Munkabeosztas_ASP_NET_Core.Data
{
    public class MunkabeosztasDbContext : DbContext
    {
        public DbSet<Dolgozo> Dolgozok { get; set; }
        public DbSet<Gepjarmu> Gepjarmuvek { get; set; }
        public DbSet<Munka> Munkak { get; set; }
        public DbSet<DolgozoMunka> DolgozoMunkak { get; set; }
        public DbSet<Adminuser> Adminusers { get; set; }

        public MunkabeosztasDbContext(DbContextOptions<MunkabeosztasDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DolgozoMunka>()
            .HasKey(t => new { t.DolgozoId, t.MunkaId });

            modelBuilder.Entity<DolgozoMunka>()
                .HasOne(dm => dm.Dolgozo)
                .WithMany(d => d.DolgozoMunkak)
                .HasForeignKey(dm => dm.DolgozoId);

            modelBuilder.Entity<DolgozoMunka>()
                .HasOne(dm => dm.Munka)
                .WithMany(m => m.DolgozoMunkak)
                .HasForeignKey(dm => dm.MunkaId);
        }
    }
}
