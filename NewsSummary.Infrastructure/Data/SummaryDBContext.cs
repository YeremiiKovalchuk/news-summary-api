using Microsoft.EntityFrameworkCore;
using NewsSummary.Core.Models;

namespace NewsSummary.Infrastructure.Data
{
    public partial class SummaryDBContext : DbContext
    {
        public SummaryDBContext()
        {
        }

        public SummaryDBContext(DbContextOptions<SummaryDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CityDto> Cities { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
