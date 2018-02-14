using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit.Configuration;
using RawRabbit.DependencyInjection.ServiceCollection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quoting.Infrastructure.Bus.Configuration
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            services.AddRawRabbit(new RawRabbit.Instantiation.RawRabbitOptions
            {
                ClientConfiguration = config.GetSection("RawRabbit").Get<RawRabbitConfiguration>()
            });
        }
    }
}
