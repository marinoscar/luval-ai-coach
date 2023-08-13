using Luval.Coach.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Luval.Coach.Data
{
    public interface ICoachDbContext
    {
        DbSet<CoachNote> CoachNoteResponses { get; set; }
        DbSet<CoachSession> CoachNotes { get; set; }
        DbSet<MetricCategory> MetricCategories { get; set; }
        DbSet<Metric> Metrics { get; set; }
        DbSet<MetricValue> MetricValues { get; set; }
        DbSet<ApplicationKey> ApplicationKeys { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}