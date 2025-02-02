using Hospital.Application.Interfaces;
using Hospital.Infrastructure.Contexts;
using Hospital.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Hospital.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<INameRepository, NameRepository>();
    
        services.AddDbContext<IPatientDbContext, PatientDbContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString(nameof(PatientDbContext)))
                .LogTo(Console.WriteLine));
    
        return services;
    }
}