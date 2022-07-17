using System.Net.Http;
using Blazored.LocalStorage;
using Fluxor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using smarthotel.ui.ServiceAgents.Contracts.Customers;
using smarthotel.ui.ServiceAgents.Contracts.Services;
using smarthotel.ui.ServiceAgents.Contracts.Spaces;
using smarthotel.ui.ServiceAgents.Contracts.Visits;
using smarthotel.ui.ServiceAgents.Impls.Customers;
using smarthotel.ui.ServiceAgents.Impls.Services;
using smarthotel.ui.ServiceAgents.Impls.Spaces;
using smarthotel.ui.ServiceAgents.Impls.Visits;
using Syncfusion.Blazor;
using Westwind.AspNetCore.LiveReload;

namespace smarthotel.ui
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
      services.AddSyncfusionBlazor();

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

      services.AddScoped<HttpClient>(s =>
      {
        var remoteUrl = Configuration["env"] == "prod" ? Configuration["RemoteUrl"] : Configuration["LocalUrl"];
        var client = new HttpClient {BaseAddress = new System.Uri(remoteUrl)};
        return client;
      });

      services.AddScoped<ICustomerDataService, CustomerDataService>();
      services.AddScoped<IServiceDataService, ServiceDataService>();
      services.AddScoped<ISpaceDataService, SpaceDataService>();
      services.AddScoped<IVisitDataService, VisitDataService>();
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
