using System;
using System.Threading.Tasks;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using smarthotel.ui.Store.Services;
using smarthotel.ui.Store.Services.Actions.FetchFreqServices;

namespace smarthotel.ui.Pages.Queries2
{
  public class QueryFreqServiceViewModel : FluxorComponent
  {
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }

    [Inject] public IState<ServiceState> ServiceState { get; set; }
    [Inject] public IConfiguration Configuration { get; set; }

    #region Initialization
    public string[] ZoomToolbar = new string[] { "Zoom", "ZoomIn", "ZoomOut", "Pan", "Reset" };
    protected override Task OnInitializedAsync()
    {
      Dispatcher.Dispatch(new FetchFreqServiceListAction());
      StateHasChanged();
      return base.OnInitializedAsync();
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      GC.SuppressFinalize(this);
    }

    public void Dispose()
    {
      GC.SuppressFinalize(this);
    }

    #endregion
  }
}