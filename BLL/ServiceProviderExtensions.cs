using BLL.RepositoryServices.Implementations.RepositoryServices;
using DAL;
using Microsoft.Extensions.DependencyInjection;

namespace BLL
{
    public static class ServiceProviderExtensions
    {
        public static void AddBLLServices(this IServiceCollection services)
        {
            services.AddInfrastructure();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<AnimalRepositoryService>();
            services.AddScoped<UserRepositoryService>();
            services.AddScoped<TrophyRepositoryService>();
            services.AddScoped<TypeAnimalRepositoryService>();
        }
    }
}
