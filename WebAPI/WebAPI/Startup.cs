using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebAPIFramework.BaseClasses;
using WebAPIFramework.ConfigModel;
using WebAPIFramework.Interfaces;

namespace WebAPI
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
      // Configを専用Modelに設定
      services.Configure<DatabaseConfigModel>(Configuration.GetSection("DB"));

      // トークン設定
      services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

      // DIの設定
      services.AddScoped<IRepositoryBase, RepositoryBase>();

      // セッションの設定
      // Adds a default in-memory implementation of IDistributedCache.
      services.AddDistributedMemoryCache();

      // session
      services.AddSession(options =>
      {
        // Set a short timeout for easy testing.
        // options.IdleTimeout = TimeSpan.FromSeconds(10);
        // options.Cookie.HttpOnly = true;

        options.Cookie.Name = ControllerWithRepositoryBase.SessionCookieName;
      });

      services.AddMvc();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, IAntiforgery antiforgery)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      // session有効化
      app.UseSession();

      // ルートアクセス時にトークン発行
      app.Use(next => context =>
      {
        if (
            string.Equals(context.Request.Path.Value, "/", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(context.Request.Path.Value, "/index.html", StringComparison.OrdinalIgnoreCase))
        {
          // We can send the request token as a JavaScript-readable cookie, and Angular will use it by default.
          var tokens = antiforgery.GetAndStoreTokens(context);
          context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken,
                    new CookieOptions() { HttpOnly = false });
        }

        return next(context);
      });

      // 静的ファイルのデフォルト設定を有効にする
      app.UseDefaultFiles();

      // 静的ファイルを使用する
      app.UseStaticFiles();

      app.UseMvc();
    }
  }
}
