
using FluentValidation;
using FluentValidation.AspNetCore;
using FormHelper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using NovaErp.Common.CacheService;
using NovaErp.Common.CacheService.Redis;
using NovaErp.Models.SiparisModels;
using NovaErp.UI.Filters;
using NovaErp.UI.Hubs;
using NovaErp.UI.Models;
using NovaErp.UI.Models.Validators;
using System;
using System.Globalization;
using Westwind.AspNetCore.LiveReload;

namespace NovaErp.UI
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
            services.AddLiveReload(config =>
            {
                // optional - use config instead
                //config.LiveReloadEnabled = true;
                //config.FolderToMonitor = Path.GetFullname(Path.Combine(Env.ContentRootPath,"..")) ;
            });
            services.AddControllersWithViews().AddMvcOptions(mvcOptions =>
            {
                mvcOptions.Filters.Add(typeof(TransactionActionFilter));
            });
            services.AddControllersWithViews();
            services.AddScoped<TransactionActionFilter>();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddMvc().AddRazorRuntimeCompilation();
            services.AddControllersWithViews().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => { options.LoginPath = "/Login/GirisYap"; });
            services.AddSession();
            services.AddDistributedMemoryCache();
            services.AddCors(options => options.AddDefaultPolicy(policy =>
                                       policy.AllowAnyMethod()
                                       .AllowAnyHeader()
                                       .AllowCredentials()
                                       .SetIsOriginAllowed(origin => true)
                                      ));


            //services.AddAutoMapper(/*typeof(MüsteriProfil)*/);

            //services.AddControllers().AddNewtonsoftJson();
            //services.AddStackExchangeRedisCache(options =>
            //{
            //    options.Configuration = Configuration.GetSection("Redis")["ConnectionString"];
            //});

            services.AddSingleton<RedisServer>();
            services.AddSingleton<ICacheService, RedisCacheService>();
            services.AddControllersWithViews()
                    .AddFormHelper(options =>
                    {
                        options.CheckTheFormFieldsMessage = "Form alanlarýný kontrol ediniz.";
                        //options.EmbeddedFiles = true;
                        options.RedirectDelay = 2000;
                        options.ToastrDefaultPosition = ToastrPosition.BottomCenter;

                    });

            services.AddSignalR();
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddTransient<IValidator<Musteri>, MusteriValidator>();
            #region ViewModels
            services.AddTransient<TaslakSiparisOlusturModel>(x => new TaslakSiparisOlusturModel());
            #endregion
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            app.UseLiveReload();
            app.UseAuthentication();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors();
            app.UseRouting();

            app.UseSession();
            app.UseFormHelper();
            app.UseAuthorization();
            var cultureInfo = new CultureInfo("tr-TR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            app.UseEndpoints(endpoints =>
            {


                endpoints.MapHub<MusteriHub>("/MusteriHub");
                endpoints.MapHub<SiparisTaslakHub>("/SiparisTaslakHub");
                endpoints.MapControllerRoute("Anasayfa", "Anasayfa", new { controller = "Home", action = "Index" });
                endpoints.MapControllerRoute("Siparis", "YeniSiparis", new { controller = "YeniSiparis", action = "YeniSiparis" });
                endpoints.MapControllerRoute("Siparis", "TaslakSiparisler", new { controller = "TaslakSiparis", action = "TaslakSiparis" });
                endpoints.MapControllerRoute("Planlama", "PlanlamaTaslakSiparisler", new { controller = "Planlama", action = "PlanlamaTaslakSiparisler" });
                endpoints.MapControllerRoute("Login", "{controller=Login}/{action=GirisYap}");
            });




            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //});
        }
    }
}
