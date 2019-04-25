using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HttpRestWorkshop
{
    using HttpRestWorkshop.DAL.Models;
    using HttpRestWorkshop.DAL.Service;

    using Microsoft.EntityFrameworkCore;

    using Swashbuckle.AspNetCore.Swagger;

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
            services.AddDbContext<AppDbContext>(o => o.UseSqlServer(Configuration.GetConnectionString("connection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddMemoryCache();
            services.AddTransient<AppDbContext>();
            services.AddTransient<BoardGamesService>();
            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v2", new Info { Title = "Board Games API", Version = "v2" });
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "Board Games API");
                });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
