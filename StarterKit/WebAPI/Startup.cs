using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using Framework.WebAPI.BaseClasses;
using Framework.WebAPI.ConfigModel;
using Framework.WebAPI.Interfaces;

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

#if DEBUG
      services.AddMvc();

      // SwaggerGenサービスの登録
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "My API", Version = "v1" });
        c.SwaggerDoc("v2", new Swashbuckle.AspNetCore.Swagger.Info { Title = "My API", Version = "v2" });

        // XMLコメントを反映
        var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
      });
#else
        // トークンキーを発行
        services.AddMvc(options =>
            options.Filters.Add(new Microsoft.AspNetCore.Mvc.AutoValidateAntiforgeryTokenAttribute()));
#endif

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

#if DEBUG
      var configuration = app.ApplicationServices.GetService<Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration>();
      if(configuration != null)
      {
        configuration.DisableTelemetry = true;
      }

      // Swaggerミドルウェアの登録
      app.UseSwagger();
      // SwaggerUIミドルウェアの登録
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2");

        c.IndexStream = () =>
        {
          using (var readStream = new StreamReader($"{AppContext.BaseDirectory}swaggerIndex.html"))
          {
            var html = readStream.ReadToEnd();

            return new MemoryStream(Encoding.UTF8.GetBytes(html));
          }
        };

      });
#endif

    }
  }
}
