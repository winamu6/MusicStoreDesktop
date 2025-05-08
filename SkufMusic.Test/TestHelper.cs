using Microsoft.EntityFrameworkCore;
using SkufMusic.Data;
using SkufMusic.Data.Data;

namespace SkufMusic.Test
{
    public static class TestHelper
    {
        public static MusicStoreDbContext GetInMemoryDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<MusicStoreDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var db = new MusicStoreDbContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }
    }
}
