using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Oldrook.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Oldrook.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Oldrook
{
    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddRazorPages();
                //.AddRazorPagesOptions(options =>
                //{
                //    options.Conventions.AuthorizeFolder("/Admin");
                //    //options.Conventions.AuthorizeFolder("/Account");
                //    //options.Conventions.AllowAnonymousToPage("/Account/Login");
                //});
            services.AddSignalR();

            //Authentication
            IConfigurationSection extAuthSection = Configuration.GetSection("ExternalAuth");
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie()
                .AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = extAuthSection["FacebookId"];
                    facebookOptions.AppSecret = extAuthSection["FacebookSec"];
                })
                .AddGoogle(options =>
                {
                    options.ClientId = extAuthSection["GoogleId"];
                    options.ClientSecret = extAuthSection["GoogleSec"];
                })
                .AddMicrosoftAccount(microsoftOptions =>
                {
                    microsoftOptions.ClientId = extAuthSection["MicrosoftId"];
                    microsoftOptions.ClientSecret = extAuthSection["MicrosoftSec"];
                })
                .AddTwitter(twitterOptions =>
                {
                    twitterOptions.ConsumerKey = extAuthSection["TwitterId"];
                    twitterOptions.ConsumerSecret = extAuthSection["TwitterSec"];
                    twitterOptions.RetrieveUserDetails = true;
                });

            services.AddSingleton<IChatRoomService, InMemoryChatRoomService>();
            services.AddScoped<ICelebService, CelebService>();
        }   

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapHub<RookHub>("/rookHub");
            });
        }
    }
}
