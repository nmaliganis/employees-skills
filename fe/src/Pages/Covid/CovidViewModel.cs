using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using smarthotel.ui.Models.DTOs.Customers;
using smarthotel.ui.Store.Covids;
using smarthotel.ui.Store.Covids.Actions.FetchCovids;
using smarthotel.ui.Store.Covids.Actions.FetchSimilarCovids;
using smarthotel.ui.Store.Customers;
using smarthotel.ui.Store.Customers.Actions.FetchCustomers;

namespace smarthotel.ui.Pages.Covid
{
  public class CovidViewModel : FluxorComponent
  {
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public IState<CustomerState> CustomerState { get; set; }
    [Inject] public IState<CovidState> CovidState { get; set; }
    [Inject] public IConfiguration Configuration { get; set; }

    #region Initialization
    public string[] ZoomToolbar = new string[] { "Zoom", "ZoomIn", "ZoomOut", "Pan", "Reset" };
    protected override Task OnInitializedAsync()
    {
      Dispatcher.Dispatch(new FetchCustomerListAction());
      StateHasChanged();
      return base.OnInitializedAsync();
    }

    protected override void Dispose(bool disposing)
    {
      _selectedCustomerItems = null;
      base.Dispose(disposing);
      GC.SuppressFinalize(this);
    }

    public void Dispose()
    {
      GC.SuppressFinalize(this);
    }

    #endregion

    #region Functions Nfc

    protected void PageChangedCustomerHandler(int currentPage)
    {
    }

    public CustomerDto SelectedCustomerItem { get; set; }
    private IEnumerable<CustomerDto> _selectedCustomerItems; 
    public IEnumerable<CustomerDto> SelectedCustomerItems 
    {
      get
      {
        if (_selectedCustomerItems != null && !Equals(_selectedCustomerItems, Enumerable.Empty<CustomerDto>()))
          return _selectedCustomerItems;

        if(CustomerState.Value.CustomerList == null)
          return _selectedCustomerItems = Enumerable.Empty<CustomerDto>();
        SelectedCustomerItem = CustomerState.Value.CustomerList.FirstOrDefault();
        return _selectedCustomerItems = new List<CustomerDto> { SelectedCustomerItem };
      }
      set => _selectedCustomerItems = value;
    }

    protected void OnCustomerSelect(IEnumerable<CustomerDto> customerItems)
    {
      SelectedCustomerItem = customerItems.FirstOrDefault();
      SelectedCustomerItems = new List<CustomerDto> { SelectedCustomerItem };

      Dispatcher.Dispatch(new FetchCovidListAction(SelectedCustomerItem.Nfc));
      Dispatcher.Dispatch(new FetchCovidSimilarListAction(SelectedCustomerItem.Nfc));
    }

    #endregion
  }
}