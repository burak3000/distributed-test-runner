using DistributedTestRunner.Core;
using DistributedTestRunner.Core.Domain;
using DistributedTestRunner.Core.Repositories;
using DistributedTestRunner.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace DistributeTestRunner.Tests;

public class Tests
{
    IServiceCollection testServices = new ServiceCollection();
    IHost? AppHost;

    IHost? BuildHost()
    {
        var serviceBuilder = Host.CreateDefaultBuilder()
              .ConfigureServices((context, services) =>
              {
                  services.Add(testServices);
              });
        var host = serviceBuilder.Build();
        return host;
    }


    [SetUp]
    public void Setup()
    {
        /*EF in memory provider is not recommended(see https://learn.microsoft.com/en-us/ef/core/testing/ 
         * and https://learn.microsoft.com/en-us/ef/core/providers/in-memory/?tabs=dotnet-core-cli).
         * Even though it is not recommended, we've to use it for repository pattern testing.
         * For other tests, we can use mocked repositories.
         * */
        testServices.AddScoped(typeof(IRepository<>), typeof(Repository<>))
                    .AddTransient<IUnitOfWork, UnitOfWork>()
                    .AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("BloggingControllerTest")
                        .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning)));
    }



    [Test]
    public async Task Test1Async()
    {
        AppHost = BuildHost();
        var repository = AppHost.Services.GetRequiredService<IRepository<TestRequest>>();
        IUnitOfWork unitOfWork = AppHost.Services.GetService<IUnitOfWork>();
        await unitOfWork.Repository<TestRequest>().AddAsync(new TestRequest { TestName = "BurakTest1" });
        await unitOfWork.CommitAsync();

        var allRequests = await repository.GetAllAsync();
        var found = await repository.Find(t => t.TestName.Contains("Burak"));
        Assert.That(found, Is.Not.Null);
    }
}
