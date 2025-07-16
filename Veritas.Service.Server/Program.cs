using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Library.Configuration.Services;
using Library.Database.Context;
using Veritas.Service.Server.Encryption;
using Veritas.Service.Server.Services;

namespace Veritas.Service.Server;

internal abstract class Program
{
    private static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        var socketService = host.Services.GetRequiredService<SocketService>();
        await socketService.StartAsync();
        
        while (true)
        {
            Console.ReadLine();
        }
    }
    
    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.SetMinimumLevel(LogLevel.Debug);
            })
            .ConfigureServices((_, services) =>
            {
                services.AddSingleton<HashGenerator>();
                services.AddSingleton<IConfigurationService, ConfigurationService>();
                services.AddSingleton<SocketService>();
                services.AddDbContext<AccountContext>(options => options.UseSqlServer("Server=ZENITH\\SQLEXPRESS;Database=Account;User Id=sa;Password=ILu4EN.YamG4L.;TrustServerCertificate=True;")
                    .EnableSensitiveDataLogging()
                );
            });
}