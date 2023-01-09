using Microsoft.EntityFrameworkCore;
using SampleEFCoreMappingConsumer.Entities;

namespace SampleEFCoreMappingConsumer.Data;
internal static class SeedData {
    public static async Task SeedAsync(SampleContext context, CancellationToken cancellationToken) {
        if (!await context.SourceEntities.AnyAsync(cancellationToken)) {
            await SeedSourceEntitiesAsync(context, cancellationToken);       
        }
    }

    private static async Task SeedSourceEntitiesAsync(SampleContext context, CancellationToken cancellationToken) {
        await context.SourceEntities.AddRangeAsync(
            new SourceEntity[] {
                new() { Name = "Src1", Description = "The 1st Source", Type = "The 1st Type" },
                new() { Name = "Src2", Description = "The 2nd Source", Type = "The 2nd Type" },
                new() { Name = "Src3", Description = "The 3rd Source", Type = "The 3rd Type" },
                new() { Name = "Src4", Description = "The 4th Source", Type = "The 4th Type" },
                new() { Name = "Src5", Description = "The 5th Source", Type = "The 5th Type" }
            },
            cancellationToken
        );

        await context.SaveChangesAsync(cancellationToken);
    }
}
