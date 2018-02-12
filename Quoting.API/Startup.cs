using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quoting.Domain.Queries;
using Quoting.Domain.Repositories;
using Quoting.Domain.Repositories.Queryable;
using Quoting.Domain.Seedworking;
using Quoting.Infrastructure;
using Quoting.Infrastructure.Queries;
using Quoting.Infrastructure.Repositories;
using Quoting.Infrastructure.Repositories.Queryable;

namespace Quoting.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddMvc();
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<QuotingDbContext>((DbContextOptionsBuilder options) =>
                {
                    options.UseSqlServer(Configuration["ConnectionStrings:QuotingDbConnection"],
                        sqlServerOptionsAction: sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(typeof(QuotingDbContext).Assembly.GetName().Name);
                            sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                        });
                }, ServiceLifetime.Scoped);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IQuoteRepository, QuoteRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IQuoteQueryableRepository, QuoteQueryableRepository>();
            services.AddScoped<IQuoteQuery, QuoteInformationRequestQuery>();
            services.AddScoped<IQuoteQuery, QuoteStatusRequestQuery>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
