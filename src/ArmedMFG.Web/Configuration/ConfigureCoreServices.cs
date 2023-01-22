using ArmedMFG.ApplicationCore;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Services;
using ArmedMFG.Infrastructure.Data;
using ArmedMFG.Infrastructure.Data.Queries;
using ArmedMFG.Infrastructure.Logging;
using ArmedMFG.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArmedMFG.Web.Configuration;

public static class ConfigureCoreServices
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

        services.AddScoped<IBasketService, BasketService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IBasketQueryService, BasketQueryService>();
        services.AddSingleton<IUriComposer>(new UriComposer(configuration.Get<CatalogSettings>()));
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
        services.AddTransient<IEmailSender, EmailSender>();

        return services;
    }
}
