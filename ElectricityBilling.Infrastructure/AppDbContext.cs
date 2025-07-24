using ElectricityBilling.Domain.Entities;
using ElectricityBilling.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBilling.Infrastructure
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Plan> Plans { get; set; }
        public DbSet<PricingTier> PricingTiers { get; set; }
        public DbSet<TaxGroup> TaxGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PricingTier>()
                .HasOne(pt => pt.Plan)
                .WithMany(p => p.PricingTiers)
                .HasForeignKey(pt => pt.PlanID);

            modelBuilder.Entity<Plan>()
               .HasIndex(p => p.Name)
               .IsUnique();
        }
    }

}
