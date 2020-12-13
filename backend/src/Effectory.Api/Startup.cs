using Effectory.Application.MessageHandler;
using Effectory.Core.Ports;
using Effectory.Infra.Repository;
using Effectory.Infra.Repository.Interfaces;
using Effectory.Infra.ServiceBus;
using Effectory.Infra.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Caching.Distributed;
using System;
using Effectory.Api.Extensions;
using Effectory.Infra.Repository.Mapping;
using MediatR;
using Newtonsoft.Json;
using Effectory.Shared.JsonConfiguration;
using MassTransit;

namespace Effectory.Api
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Effectory.Api", Version = "v1" });
            });

            services.AddScoped<IMongoClient>(sp => new MongoClient(Configuration.GetConnectionString("MongoDb")));

            services.RegisterServiceBus(Configuration);
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSingleton(new JsonSerializerSettings().WithPrivateSetterContractResolver());

            services.AddSingleton<ISubscribe, BusMessageSubscriber>();
            services.AddSingleton<IEventSender, EventSender>();

            services.AddScoped<IQuestionnaireRepository, QuestionnaireRepository>();
            services.AddScoped<ISurveyResponseRepository, SurveyResponseRepository>();

            services.AddScoped<IQuestionnaireUnitOfWork, QuestionnaireUnitOfWork>();
            services.AddScoped<ISurveyResponseUnitOfWork, SurveyResponseUnitOfWork>();

            services.AddSingleton(new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow =
                        TimeSpan.FromHours(double.Parse(Configuration.GetSection("Cache")["ExpireTimeHours"]))
            });
            services.AddStackExchangeRedisCache((options =>
            {
                options.Configuration = Configuration.GetSection("Cache")["Host"];
                options.InstanceName = Configuration.GetSection("Cache")["InstanceName"];
            }));

            services.RegisterAllTypes<IEntityMapper>(AppDomain.CurrentDomain.GetAssemblies());

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder
                        .AllowCredentials()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed(o => true)
                        .Build();
                    });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Effectory.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.RunMappers();

            app.UseCors();

            app.UseAuthorization();

            app.UseResponseCaching();

            app.ApplicationServices.GetRequiredService<IBusControl>().Start();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
