using SkufMusic.Core.Services.AdminServices;
using SkufMusic.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkufMusic.Test.Services
{
    [TestClass]
    public class AdminServiceTests
    {
        [TestMethod]
        public async Task ExportProductsToCsvAsync_CreatesFile()
        {
            var db = TestHelper.GetInMemoryDbContext("ExportDb");
            db.Products.Add(new Product { Name = "Bass", Description = "Low", Price = 250, Stock = 2 });
            db.SaveChanges();

            var path = "products_test_export.csv";
            if (File.Exists(path)) File.Delete(path);

            var adminService = new AdminService(db);
            await adminService.ExportProductsToCsvAsync(path);

            Assert.IsTrue(File.Exists(path));
            var content = File.ReadAllText(path);
            Assert.IsTrue(content.Contains("Bass"));
        }
    }
}
