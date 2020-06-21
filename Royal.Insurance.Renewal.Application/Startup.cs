using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Royal.Insurance.Renewal.Application.Service;

namespace Royal.Insurance.Renewal.Application
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
            services.AddSingleton<IProductTypeInfo, ProductTypeInfo>();
            services.AddSingleton<IService, CustomerInsuranceService>();
            services.AddSingleton<IMappingSerrvice,MappingService>();
            services.AddSingleton<DefaultPremium>()
                .AddScoped<IPremiumCalculation, DefaultPremium>(s => s.GetService<DefaultPremium>());           
            services.AddSingleton<StandardCover>()
                .AddScoped<IPremiumCalculation, StandardCover>(s => s.GetService<StandardCover>());
            services.AddSingleton<SpecialCover>()
                .AddScoped<IPremiumCalculation, SpecialCover>(s => s.GetService<SpecialCover>());
            services.AddSingleton<EnhancedCover>().AddScoped<IPremiumCalculation, EnhancedCover>(s => s.GetService<EnhancedCover>());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSwagger();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
