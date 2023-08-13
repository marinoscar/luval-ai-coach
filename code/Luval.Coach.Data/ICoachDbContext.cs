using Luval.Coach.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Luval.Coach.Data
{
    public interface ICoachDbContext
    {
        DbSet<CoachNoteResponse> CoachNoteResponses { get; set; }
        DbSet<CoachNote> CoachNotes { get; set; }
        DbSet<MetricCategory> MetricCategories { get; set; }
        DbSet<Metric> Metrics { get; set; }
        DbSet<MetricValue> MetricValues { get; set; }
        DbSet<ApplicationKey> ApplicationKeys { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}