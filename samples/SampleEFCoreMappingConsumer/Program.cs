using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SampleEFCoreMappingConsumer.Data;
using SampleEFCoreMappingConsumer.Dto;

namespace SampleEFCoreMappingConsumer;
public class Application : IHostedService {
    public static bool SuspendEFLogs = true;
    public static readonly StringBuilder EFLogs = new StringBuilder();

    private readonly SampleContext _context;
    private readonly AutomapGenerator.IMapper _mapper;

    public Application(SampleContext context, AutomapGenerator.IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task StartAsync(CancellationToken cancellationToken) {
        SuspendEFLogs = true;
        await _context.Database.MigrateAsync(cancellationToken);
        await SeedData.SeedAsync(_context, cancellationToken);
        SuspendEFLogs = false;

        var entities = await
            _mapper.ProjectTo<SourceDto>(
                _context.SourceEntities
            )
            .ToListAsync(cancellationToken);
        var serialized = JsonSerializer.Serialize(entities, new JsonSerializerOptions() {
            WriteIndented = true,
        });

        Debug.WriteLine(EFLogs.ToString());
        Debug.WriteLine("Entities received: {0}", serialized);
    }

    public async Task StopAsync(CancellationToken cancellationToken) {
        SuspendEFLogs = true;
        await _context.GetService<IMigrator>().MigrateAsync("0", cancellationToken);
        SuspendEFLogs = false;
    }
}

public class Program {
    public static async Task Main() {
        var builder = Host.CreateDefaultBuilder();

        builder.ConfigureHostConfiguration(icb => {
            icb.AddUserSecrets<Program>();
        });

        builder.ConfigureLogging((ctx, lb) => {
            lb.ClearProviders();
            lb.AddConfiguration(ctx.Configuration.GetSection("Logging"));
            lb.AddConsole();
        });

        builder.ConfigureServices((host, services) => {
            services.AddDbContext<SampleContext>(db => {
                var connectionStr = host.Configuration.GetConnectionString("SampleConnection");
                db.UseSqlServer(connectionStr);
                db.LogTo(
                    action: msg => Application.EFLogs.AppendLine(msg),
                    filter: (id, level) => level >= LogLevel.Information && !Application.SuspendEFLogs
                ).EnableSensitiveDataLogging();
            }, ServiceLifetime.Singleton);

            services.AddSingleton<AutomapGenerator.IMapper, AutomapGenerator.Mapper>();

            services.AddHostedService<Application>();
        });

        await builder.Build().RunAsync();
    }
}
