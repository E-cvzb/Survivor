using Microsoft.EntityFrameworkCore;
using Survivor.Entities;

namespace Survivor.Context
{
    public class SurvivorDbContext : DbContext
    {

        public SurvivorDbContext(DbContextOptions<SurvivorDbContext> options) : base(options)
        {
            
        }
        public DbSet<CategoryEntity > Categorys => Set<CategoryEntity>();
        public DbSet<CompetitorEntity> Competitors => Set<CompetitorEntity>();
     }
}
