using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SkufMusic.Data.Data;
using System.IO;

namespace SkufMusic.Data
{
    public class MusicStoreDbContextFactory : IDesignTimeDbContextFactory<MusicStoreDbContext>
    {
        public MusicStoreDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<MusicStoreDbContext>();
            var connectionString = config.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new MusicStoreDbContext(optionsBuilder.Options);
        }
    }
}
