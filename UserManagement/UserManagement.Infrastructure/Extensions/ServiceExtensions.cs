using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UserManagement.Infrastructure.Extensions;


public static class ServiceExtensions
{
    
    public static void ConfigureAutoMapper(this IServiceCollection services)
    {
        /*services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<ProductMappingProfile>();
        }, AppDomain.CurrentDomain.GetAssemblies());*/
    }
    
    public static void ConfigureRepository(this IServiceCollection services)
    {
        /*services.AddScoped<IProductRepository, ProductRepository>();*/
    }
    
    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<ApplicationContext>(opts =>
            opts.UseNpgsql(configuration.GetConnectionString("sqlConnection")));
    
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        /*services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo { Title = "Inno Shop API", Version = "v1"
            });
        });*/
    }
    
    public static void AddValidators(this IServiceCollection services)
    {
        /*services.AddValidatorsFromAssemblyContaining<ProductValidator>();*/
    }
}