using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TGB.WebAPI.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using ReflectionIT.Mvc.Paging;

namespace TGB.WebAPI
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                CultureInfo.CurrentCulture = new CultureInfo("en-US");
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddRoleManager<RoleManager<IdentityRole>>()
                    .AddDefaultUI()
                    .AddDefaultTokenProviders()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddPaging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            CreateRoles(serviceProvider).Wait();
        }

        

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var _userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string[] roleNames = { "Admin", "User" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            IdentityUser admin = await _userManager.FindByEmailAsync("Admin1@gmail.com");

            if (admin == null)
            {
                admin = new IdentityUser()
                {
                    UserName = "Admin1@gmail.com",
                    Email = "Admin1@gmail.com",
                };
                await _userManager.CreateAsync(admin, "Admin1@123");
            }
            await _userManager.AddToRoleAsync(admin, "Admin");

            IdentityUser user1 = await _userManager.FindByEmailAsync("testuser1@gmail.com");

            if (user1 == null)
            {
                user1 = new IdentityUser()
                {
                    UserName = "testuser1@gmail.com",
                    Email = "testuser1@gmail.com",
                };
                await _userManager.CreateAsync(user1, "Testuser1@123");
            }
            await _userManager.AddToRoleAsync(user1, "User");

            IdentityUser user2 = await _userManager.FindByEmailAsync("testuser2@gmail.com");

            if (user2 == null)
            {
                user2 = new IdentityUser()
                {
                    UserName = "testuser2@gmail.com ",
                    Email = "testuser2@gmail.com",
                };
                await _userManager.CreateAsync(user2, "Testuser2@123");
            }
            await _userManager.AddToRoleAsync(user2, "User");
        }


    }

}
