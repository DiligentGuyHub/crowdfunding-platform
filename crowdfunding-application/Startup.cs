using crowdfunding_application.Controllers;
using crowdfunding_application.Data;
using crowdfunding_application.Models.CloudinaryService;
using crowdfunding_application.Models.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crowdfunding_application.Models.Hubs;

namespace crowdfunding_application
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

           

            services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequiredLength = 5;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddSignalR();
            services.AddRazorPages();

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    IConfigurationSection googleAuthNSection = Configuration.GetSection("Google");

                    options.ClientId = googleAuthNSection["ClientId"];
                    options.ClientSecret = googleAuthNSection["ClientSecret"];
                });
                
            

            // Application Database Context services
            services.AddScoped<ICampaignService, CampaignService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IBonusService, BonusService>();
            services.AddScoped<IBonusTransactionService, BonusTransactionService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IRatingService, RatingService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddTransient<CommentHub>();

            services.AddScoped<ICloudinaryService, CloudinaryService>();

            services.AddSingleton(Configuration.GetSection("Cloudinary").Get<CloudinaryConfiguration>());

            // Controllers and their services
            //services.AddSingleton<HomeController>(services =>
            //{
            //    return new HomeController(
            //        services.GetRequiredService<ILogger<HomeController>>(),
            //        services.GetRequiredService<INewsService>()
            //        );
            //});

            //services.AddSingleton<CampaignController>(services =>
            //{
            //    return new CampaignController(
            //        services.GetRequiredService<ICampaignService>(), 
            //        services.GetRequiredService<UserManager<IdentityUser>>(),
            //        services.GetRequiredService<INewsService>(),                    
            //        services.GetRequiredService<IBonusService>()
            //        );
            //});

            //services.AddSingleton(services =>
            //{
            //    return new NewsController(
            //        services.GetRequiredService<INewsService>()
            //        );
            //});

            //services.AddSingleton(services =>
            //{
            //    return new TransactionController(
            //        services.GetRequiredService<ITransactionService>(),
            //        services.GetRequiredService<ICampaignService>(),
            //        services.GetRequiredService<UserManager<IdentityUser>>(),
            //        services.GetRequiredService<INewsService>()
            //        );
            //});

            //services.AddSingleton(services =>
            //{
            //    return new BonusController(
            //        services.GetRequiredService<IBonusService>(),
            //        services.GetRequiredService<ITransactionService>(),
            //        services.GetRequiredService<ICampaignService>(),
            //        services.GetRequiredService<UserManager<IdentityUser>>()
            //        );
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapHub<CommentHub>("/commentHub");
            });
        }
    }
}
