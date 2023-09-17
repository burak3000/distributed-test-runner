using DistributedTestRunner.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DistributedTestRunner.CompositionRoot;

public static class BaseServiceExtensions
{
    public static IServiceCollection AddDtrDbContext(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(options=>options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        return serviceCollection;
    }
    public static IServiceCollection AddIdentity(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddIdentity<IdentityUser, IdentityRole>(o =>
        {
            o.Password.RequiredLength = 6;
            o.SignIn.RequireConfirmedAccount = true;
        })
        .AddRoles<IdentityRole>()
        .AddRoleManager<RoleManager<IdentityRole>>()
        .AddDefaultTokenProviders()
        .AddEntityFrameworkStores<ApplicationDbContext>();
        serviceCollection.AddDbContext<ApplicationDbContext>();
        return serviceCollection;
    }

}

