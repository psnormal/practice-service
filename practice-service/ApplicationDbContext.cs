using Microsoft.EntityFrameworkCore;
using practice_service.Models;
using System.Collections.Generic;

namespace practice_service
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<PracticeProfile> PracticeProfiles { get; set; }
        public DbSet<PracticePeriod> PracticePeriods { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
