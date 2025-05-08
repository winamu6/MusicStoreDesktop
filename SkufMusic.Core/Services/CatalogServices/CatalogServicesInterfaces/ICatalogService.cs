using SkufMusic.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkufMusic.Core.Services.CatalogServices.CatalogServicesInterfaces
{
    public interface ICatalogService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<List<Product>> SearchProductsAsync(string searchTerm);
    }

}
