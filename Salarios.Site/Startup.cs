using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Salarios.Applicacion.Aplicacion;
using Salarios.Applicacion.Interfaces;
using Salarios.Dominio.Interfaces;
using Salarios.Infraestructura.Datos;
using Salarios.Infraestructura.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Salarios.Site
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

            services.AddDbContext<ContextBase>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            services.AddHttpContextAccessor();
            services.AddSession();

            services.AddSingleton<IEmpleadoRepository, EmpleadoRepository>();
            services.AddSingleton<IApplicationEmpleados, ApplicationEmpleados>();

            services.AddSingleton<IDivisionRepository, DivisionRepository>();
            services.AddSingleton<IApplicationDivision, ApplicationDivision>();

            services.AddSingleton<ISalarioRepository, SalarioRepository>();
            services.AddSingleton<IApplicationSalarios, ApplicationSalarios>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
