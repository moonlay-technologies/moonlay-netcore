using ExtCore.Infrastructure.Actions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Core.Mvc
{
    public class DefaultServices : IConfigureServicesAction
    {
        public int Priority => 1;

        public void Execute(IServiceCollection services, IServiceProvider serviceProvider)
        {
            services.AddSingleton<IWebApiContext>(c =>
            {
                var d = new WebApiContext().SetApiVersion("1.0.0");
                d.SetCurrentUser("System");

                return d;
            });

            services.AddSingleton<IWorkContext>(c => c.GetRequiredService<IWebApiContext>());
        }
    }
}
