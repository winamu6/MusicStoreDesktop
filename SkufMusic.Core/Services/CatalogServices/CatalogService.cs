using Microsoft.EntityFrameworkCore;
using SkufMusic.Core.Services.CatalogServices.CatalogServicesInterfaces;
using SkufMusic.Data.Data;
using SkufMusic.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkufMusic.Core.Services.CatalogServices
{
    public class CatalogService : ICatalogService
    {
        private readonly MusicStoreDbContext _db;

        public CatalogService(MusicStoreDbContext db)
        {
            _db = db;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task<List<Product>> SearchProductsAsync(string searchTerm)
        {
            return await _db.Products
                .Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm))
                .ToListAsync();
        }
    }

}
