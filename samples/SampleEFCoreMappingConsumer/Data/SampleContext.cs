using Microsoft.EntityFrameworkCore;
using SampleEFCoreMappingConsumer.Entities;

namespace SampleEFCoreMappingConsumer.Data;
public class SampleContext : DbContext {
    public SampleContext(DbContextOptions options) : base(options) {
    }

    public DbSet<SourceEntity> SourceEntities { get; set; }
}
