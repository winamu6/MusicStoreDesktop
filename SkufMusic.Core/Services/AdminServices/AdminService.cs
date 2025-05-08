using Microsoft.EntityFrameworkCore;
using SkufMusic.Core.Services.AdminServices.AdminServicesInterfaces;
using SkufMusic.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkufMusic.Core.Services.AdminServices
{
    public class AdminService : IAdminService
    {
        private readonly MusicStoreDbContext _db;

        public AdminService(MusicStoreDbContext db)
        {
            _db = db;
        }

        public async Task ExportProductsToCsvAsync(string filePath)
        {
            var products = await _db.Products.ToListAsync();
            using var writer = new StreamWriter(filePath);
            await writer.WriteLineAsync("Id,Name,Description,Price,Stock");
            foreach (var p in products)
            {
                var line = $"{p.Id},\"{p.Name}\",\"{p.Description}\",{p.Price},{p.Stock}";
                await writer.WriteLineAsync(line);
            }
        }
    }

}
