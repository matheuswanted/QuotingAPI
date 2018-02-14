using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quoting.API.Handlers;
using Quoting.Domain.Models.Events;
using Quoting.Domain.Queries;
using Quoting.Domain.Repositories;
using Quoting.Domain.Repositories.Queryable;
using Quoting.Domain.Seedworking;
using Quoting.Domain.Services;
using Quoting.Infrastructure;
using Quoting.Infrastructure.Bus;
using Quoting.Infrastructure.Bus.Contracts;
using Quoting.Infrastructure.Queries;
using Quoting.Infrastructure.Repositories;
using Quoting.Infrastructure.Repositories.Queryable;
using Quoting.Infrastructure.Seed;

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
            services.AddScoped<IQuoteQuery, QuoteInformationRequestQuery>();
            services.AddScoped<IQuoteQuery, QuoteStatusRequestQuery>();
            services.AddScoped<IQuotePriceQuery, PriceModifierRulesAppliableToCustomerQuery>();
            services.AddScoped<IQuotePriceQuery, BasePriceRulesAppliableToVehicleQuery>();

            services.AddScoped<IQuoteQueryableRepository, QuoteQueryableRepository>();
            services.AddScoped<IQuotePriceQueryableRepository, QuotePriceQueryableRepository>();

            services.AddTransient<IQuotingCalculator, QuotingCalculator>();
            services.AddTransient<DbSeed>();

            new Infrastructure.Bus.Configuration.Startup().ConfigureServices(services, Configuration);

            services.AddTransient<IPublisher, EventPublisher>();
            services.AddTransient<IEventManager, EventManager>();

            services.AddTransient<IEventHandler<QuoteRequestedEvent>, QuoteRequestedHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IEventManager manager, DbSeed seed)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            seed.Seed().Wait();

            manager.Subscribe<QuoteRequestedEvent, IEventHandler<QuoteRequestedEvent>>();
        }
    }
}
