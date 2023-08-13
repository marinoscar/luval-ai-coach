using Luval.Coach.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Coach.Data
{
    public class CoachDbContext : DbContext, ICoachDbContext
    {
        public DbSet<CoachSession> CoachNotes { get; set; }
        public DbSet<CoachNote> CoachNoteResponses { get; set; }
        public DbSet<Metric> Metrics { get; set; }
        public DbSet<MetricCategory> MetricCategories { get; set; }
        public DbSet<MetricValue> MetricValues { get; set; }
        public DbSet<ApplicationKey> ApplicationKeys { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Configuaration
            modelBuilder.Entity<Metric>()
                .Property(p => p.MetricType)
                .HasConversion(
                    v => v.ToString(),
                    v => (MetricType)Enum.Parse(typeof(MetricType), v));
        }

        public virtual async Task<int> SeedDataAsync(CancellationToken cancellationToken = default)
        {
            if (!MetricCategories.Any())
            {
                var categories = MetricCategory.GetInitialValues();
                await MetricCategories.AddRangeAsync(categories, cancellationToken);
            }
            return await SaveChangesAsync(cancellationToken);
        }
    }
}
