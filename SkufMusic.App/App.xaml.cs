using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SkufMusic.App;
using System.IO;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using SkufMusic.Data.Data;
using SkufMusic.Core.Services.AdminServices.AdminServicesInterfaces;
using SkufMusic.Core.Services.AdminServices;
using SkufMusic.Core.Services.CartServices.CartServicesInterfaces;
using SkufMusic.Core.Services.CartServices;
using SkufMusic.Core.Services.CatalogServices.CatalogServicesInterfaces;
using SkufMusic.Core.Services.CatalogServices;
using SkufMusic.Core.Services.OrderServices.OrderServicesInterfaces;
using SkufMusic.Core.Services.OrderServices;
using SkufMusic.Core.Services.UserServices.UserServicesInterfaces;
using SkufMusic.Core.Services.UserServices;

public partial class App : Application
{
    public static IHost AppHost { get; private set; }

    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                string connectionString = context.Configuration.GetConnectionString("DefaultConnection");

                services.AddDbContext<MusicStoreDbContext>(options =>
                    options.UseSqlServer(connectionString));

                services.AddScoped<IUserService, UserService>();
                services.AddScoped<ICatalogService, CatalogService>();
                services.AddScoped<ICartService, CartService>();
                services.AddScoped<IOrderService, OrderService>();
                services.AddScoped<IAdminService, AdminService>();

            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost.StartAsync();
        base.OnStartup(e);

        var mainWindow = new MainWindow();
        mainWindow.Show();
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost.StopAsync();
        base.OnExit(e);
    }
}

