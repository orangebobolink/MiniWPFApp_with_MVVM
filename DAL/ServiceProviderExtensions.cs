using DAL.Data;
using DAL.Data.Implementation;
using DAL.Domain.Interfaces.ImplementationsOfRepository;
using Microsoft.Extensions.DependencyInjection;

namespace DAL
{
    public static class ServiceProviderExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            //var defaultConnectionString = configuration.GetConnectionString("DefaultConnection");
            //services.AddDbContext<ApplicationContext>(options =>
            //   options.UseLazyLoadingProxies()
            //   .UseSqlServer(defaultConnectionString));
            services.AddScoped<ApplicationContext>();
            services.AddScoped<IAnimalRepository, AnimalRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITrophyRepository, TrophyRepository>();
            services.AddScoped<ITypeAnimalRepository, TypeAnimalRepository>();
        }
    }
}
