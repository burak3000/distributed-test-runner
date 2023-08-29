using DistributedTestRunner.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DistributedTestRunner.CompositionRoot;

public static class BaseServiceExtensions
{
    public static IServiceCollection AddDtrDbContext(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>();
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

