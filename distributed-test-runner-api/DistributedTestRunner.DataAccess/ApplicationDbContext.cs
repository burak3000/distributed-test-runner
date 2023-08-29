using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DistributedTestRunner.DataAccess;

public class ApplicationDbContext: IdentityDbContext
{
    protected readonly IConfiguration Configuration;
    public ApplicationDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to postgres with connection string from app settings
        options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));

    }
}

