using AutoMapper;
using Core;
using Data;
using DataEntity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Modules.CityModule;
using Modules.ContactModule;
using Modules.ContactTypeModule;
using Modules.PhoneModule;
using Modules.SpecializationModule;
using Modules.SubjectModule;
using Modules.TutorModule;
using Newtonsoft.Json;
using TutorsOfMogilev_NetCore.Models;
using TutorsOfMogilev_NetCore.Services;

namespace TutorsOfMogilev_NetCore
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(opts =>
                opts.UseSqlServer(
                    _configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });

            IMapper mapper = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MapperProfile());
                    mc.AddProfile(new VMMapperProfile());
                })
                .CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<CityRepository>();
            services.AddScoped<ContactRepository>();
            services.AddScoped<ContactTypeRepository>();
            services.AddScoped<PhoneRepository>();
            services.AddScoped<SpecializationRepository>();
            services.AddScoped<SubjectRepository>();
            services.AddScoped<TutorRepository>();

            services.AddTransient<ExceptionHandler>();
            services.AddTransient<TutorService>();
            services.AddTransient<ImageService>();

            services.AddCors();

            services.AddMvc()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        public void Configure(
            IApplicationBuilder app,
            IHostEnvironment env,
            ILoggerFactory loggerFactory
            )
        {
            loggerFactory.AddFile("Logs/{Date}.txt");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();

            if (env.IsDevelopment())
            {
                app.UseCors(builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                );
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Tutors}/{action=List}/{id?}");
            });
        }
    }
}
