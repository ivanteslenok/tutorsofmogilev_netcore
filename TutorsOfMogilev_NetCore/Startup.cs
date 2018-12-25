using AutoMapper;
using DataEntity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.ContactModule;
using Modules.ContactTypeModule;
using Modules.DistrictModule;
using Modules.PhoneModule;
using Modules.SpecializationModule;
using Modules.SubjectModule;
using Modules.TutorModule;
using Newtonsoft.Json;
using TutorsOfMogilev_NetCore.Filters;
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

            services.AddAutoMapper(mc =>
            {
                mc.AddProfiles(new[] { "Data", "TutorsOfMogilev_NetCore" });
            });

            services.AddScoped<ContactRepository>();
            services.AddScoped<ContactTypeRepository>();
            services.AddScoped<DistrictRepository>();
            services.AddScoped<PhoneRepository>();
            services.AddScoped<SpecializationRepository>();
            services.AddScoped<SubjectRepository>();
            services.AddScoped<TutorRepository>();

            services.AddTransient<TutorService>();
            services.AddTransient<ImageService>();

            services.AddCors();

            services.AddMvc(config =>
                {
                    config.Filters.Add(new CustomExceptionFilter(new HostingEnvironment()));
                })
                .AddJsonOptions(
                    options => options.SerializerSettings.ReferenceLoopHandling =
                        ReferenceLoopHandling.Ignore);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(builder =>
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                );
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Tutors}/{action=List}/{id?}");
            });
        }
    }
}
