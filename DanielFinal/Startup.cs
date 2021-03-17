using AutoMapper;
using DanielFinal.Data;
using DanielFinal.Infrastructure;
using DanielFinal.Models.Profiles;
using DanielFinal.Services.Abstraction;
using DanielFinal.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DanielFinal
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
            services.AddDbContext<DanielFinalDbContext>((serviceProvider, options) =>
            {
                options.UseSqlServer(Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>().DefaultConnection,
                    optionsBuilder =>
                    {
                        optionsBuilder.EnableRetryOnFailure();
                        optionsBuilder.CommandTimeout(60);
                        optionsBuilder.MigrationsAssembly("DanielFinal.Data");
                    });
                options
                    .UseInternalServiceProvider(serviceProvider)
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            }).AddEntityFrameworkSqlServer();

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(OptionProfile));
            }).CreateMapper();
            services.AddSingleton(mapper);

            services.AddTransient<IOptionService, OptionService>();
            services.AddTransient<IQuestionService, QuestionService>();
            //services.AddTransient<IPlayerMatchService, PlayerMatchService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DanielFinal", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DanielFinal v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
