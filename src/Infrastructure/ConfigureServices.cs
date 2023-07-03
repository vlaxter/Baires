using Baires.Application.Common.Interfaces;
using Baires.Domain.Entities;
using Baires.Infrastructure.Persistence;
using Baires.Infrastructure.Persistence.Interceptors;
using Baires.Infrastructure.Repositories;
using Baires.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("BairesDb"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IPeopleRepository, PeopleRepository>();
        services.AddTransient<IJsonFileDeserializer<Person>, PeopleJsonFileDeserializerService>();
        services.AddTransient<IMLModel<Person>, FakeMLModelService>();

        return services;
    }
}
