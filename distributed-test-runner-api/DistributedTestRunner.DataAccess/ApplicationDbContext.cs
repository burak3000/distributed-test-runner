using System.ComponentModel.DataAnnotations.Schema;
using DistributedTestRunner.Core.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DistributedTestRunner.DataAccess;

public class ApplicationDbContext: IdentityDbContext
{
    protected readonly IConfiguration Configuration;
    public ApplicationDbContext(DbContextOptions options, IConfiguration configuration):base(options)
    {
        Configuration = configuration;
    }

    public DbSet<TestRequest> TestRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TestRequest>()
            .HasKey(i => i.Id);
    }
}

