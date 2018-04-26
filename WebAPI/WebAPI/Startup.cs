using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commons.ConfigModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebAPIFramework.BaseClasses;
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

      // DIの設定
      services.AddScoped<IRepositoryBase, RepositoryBase>();

      services.AddMvc();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      // 静的ファイルのデフォルト設定を有効にする
      app.UseDefaultFiles();

      // 静的ファイルを使用する
      app.UseStaticFiles();

      app.UseMvc();
    }
  }
}
