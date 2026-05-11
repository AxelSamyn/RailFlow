using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace RailFlow.TrainService.Infrastructure.Persistence;

public sealed class TrainDbContextFactory
    : IDesignTimeDbContextFactory<TrainDbContext>
{
    public TrainDbContext CreateDbContext( string[ ] args )
    {
        Console.WriteLine( "Design-time factory used" );

        string environment =
            Environment.GetEnvironmentVariable( "ASPNETCORE_ENVIRONMENT" )
            ?? "Production";

        IConfiguration configuration =
            new ConfigurationBuilder()
                .SetBasePath( Path.Combine(Directory.GetCurrentDirectory(), "../RailFlow.TrainService.Api") )
                .AddJsonFile( "appsettings.json", optional: false )
                .AddJsonFile( $"appsettings.{environment}.json", optional: true )
                .AddEnvironmentVariables()
                .Build();

        string connectionString =
            configuration.GetConnectionString( "TrainDb" )
            ?? throw new InvalidOperationException( "Connection string 'TrainDb' not found." );

        DbContextOptions<TrainDbContext> options =
            new DbContextOptionsBuilder<TrainDbContext>()
                .UseSqlServer( connectionString )
                .Options;

        Console.WriteLine( $"Environment: {environment}" );
        Console.WriteLine( $"ConnectionString: {connectionString}" );

        return new TrainDbContext( options );
    }
}
