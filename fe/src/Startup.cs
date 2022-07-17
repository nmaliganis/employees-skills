using System.Net.Http;
using Blazored.LocalStorage;
using employee.skill.fe.ServiceAgents.Contracts.Employees;
using employee.skill.fe.ServiceAgents.Contracts.Skills;
using employee.skill.fe.ServiceAgents.Impls.Employees;
using employee.skill.fe.ServiceAgents.Impls.Skills;
using Fluxor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Westwind.AspNetCore.LiveReload;

namespace employee.skill.fe
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddRazorPages();
      services.AddServerSideBlazor();
      services.AddTelerikBlazor();

      services.AddLiveReload(config =>
      {
        config.LiveReloadEnabled = true;
        config.ClientFileExtensions = ".css,.js,.htm,.html";
        config.FolderToMonitor = "~/../";
      });

      services.AddFluxor(options =>
      {
        options.ScanAssemblies(typeof(Startup).Assembly);
        options.UseRouting();
      });

      services.AddBlazoredLocalStorage();
      services.AddBlazoredLocalStorage(config =>
        config.JsonSerializerOptions.WriteIndented = true);

      services.AddScoped<IEmployeeDataService, EmployeeDataService>();
      services.AddScoped<ISkillDataService, SkillDataService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapBlazorHub();
        endpoints.MapFallbackToPage("/_Host");
      });
    }
  }
}
