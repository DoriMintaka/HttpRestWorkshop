using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using HttpRestWorkshop.DAL.Models;
using HttpRestWorkshop.DAL.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace HttpRestWorkshop
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(o => o.UseSqlServer(configuration.GetConnectionString("connection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddMemoryCache();
            services.AddTransient<IBoardGamesService, BoardGamesService>();
            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Board Games API", Version = "v1" });
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Board Games API");
                });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
