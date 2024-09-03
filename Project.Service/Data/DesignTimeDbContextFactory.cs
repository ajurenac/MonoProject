using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Project.Service.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<VehicleDbContext>
{
    public VehicleDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<VehicleDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        builder.UseSqlServer(connectionString);

        return new VehicleDbContext(builder.Options);
    }
}
