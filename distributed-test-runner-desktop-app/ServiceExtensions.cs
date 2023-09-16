using DistributedTestRunnerDesktop.Core;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace WpfApp
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddFormFactory<TForm>(this IServiceCollection services) where TForm : class
        {
            services.AddTransient<TForm>();
            services.AddSingleton<Func<TForm>>(x => () => x.GetService<TForm>()!);
            services.AddSingleton<IAbstractFactory<TForm>, AbstractFactory<TForm>>();
            return services;
        }
    }
}
