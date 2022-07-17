using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using smarthotel.ui.Models.DTOs.Customers;
using smarthotel.ui.Models.DTOs.Services;
using smarthotel.ui.Models.DTOs.Spaces;
using smarthotel.ui.Store.Customers;
using smarthotel.ui.Store.Customers.Actions.FetchCustomers;
using smarthotel.ui.Store.Dashboard;
using smarthotel.ui.Store.Dashboard.Actions.FetchDashboards;
using smarthotel.ui.Store.Services;
using smarthotel.ui.Store.Services.Actions.FetchServices;
using smarthotel.ui.Store.Spaces;
using smarthotel.ui.Store.Spaces.Actions.FetchSpaces;

namespace smarthotel.ui.Pages.Dashboard
{
  public class DashboardViewModel : FluxorComponent
  {
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IState<DashboardState> DashboardState { get; set; }
    [Inject] public IState<SpaceState> SpaceState { get; set; }
    [Inject] public IState<CustomerState> CustomerState { get; set; }
    [Inject] public IState<ServiceState> ServiceState { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public IConfiguration Configuration { get; set; }

    #region Initialization
    public string[] ZoomToolbar = new string[] { "Zoom", "ZoomIn", "ZoomOut", "Pan", "Reset" };
    protected override Task OnInitializedAsync()
    {
      Dispatcher.Dispatch(new FetchDashboardAction());
      Dispatcher.Dispatch(new FetchCustomerListAction());
      Dispatcher.Dispatch(new FetchSpaceListAction());
      Dispatcher.Dispatch(new FetchServiceListAction());
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


    #region Functions Customers

    protected void PageChangedHandlerCustomer(int currentPage)
    {
    }

    public async Task OnConfigureCustomerClickHandler()
    {
      NavigationManager.NavigateTo($"customers");
    }

    public CustomerDto SelectedCustomerItem { get; set; }
    private IEnumerable<CustomerDto> _selectedItemsCustomer; 
    public IEnumerable<CustomerDto> SelectedItemsCustomer 
    {
      get
      {
        if (_selectedItemsCustomer != null && !Equals(_selectedItemsCustomer, Enumerable.Empty<CustomerDto>()))
          return _selectedItemsCustomer;

        if(CustomerState.Value.CustomerList == null)
          return _selectedItemsCustomer = Enumerable.Empty<CustomerDto>();
        SelectedCustomerItem = CustomerState.Value.CustomerList.FirstOrDefault();
        return _selectedItemsCustomer = new List<CustomerDto> { SelectedCustomerItem };
      }
      set => _selectedItemsCustomer = value;
    }

    protected void OnSelectCustomer(IEnumerable<CustomerDto> customerItems)
    {
      SelectedCustomerItem = customerItems.FirstOrDefault();
      SelectedItemsCustomer = new List<CustomerDto> { SelectedCustomerItem };
    }

    #endregion

    #region Functions Spaces

    protected void PageChangedHandlerSpace(int currentPage)
    {
    }

    public SpaceDto SelectedSpaceItem { get; set; }
    private IEnumerable<SpaceDto> _selectedItemsSpace; 
    public IEnumerable<SpaceDto> SelectedItemsSpace 
    {
      get
      {
        if (_selectedItemsSpace != null && !Equals(_selectedItemsSpace, Enumerable.Empty<SpaceDto>()))
          return _selectedItemsSpace;

        if(SpaceState.Value.SpaceList == null)
          return _selectedItemsSpace = Enumerable.Empty<SpaceDto>();
        SelectedSpaceItem = SpaceState.Value.SpaceList.FirstOrDefault();
        return _selectedItemsSpace = new List<SpaceDto> { SelectedSpaceItem };
      }
      set => _selectedItemsSpace = value;
    }

    protected void OnSelectSpace(IEnumerable<SpaceDto> spaceItems)
    {
      SelectedSpaceItem = spaceItems.FirstOrDefault();
      SelectedItemsSpace = new List<SpaceDto> { SelectedSpaceItem };
    }

    #endregion


    #region Functions Services

    protected void PageChangedHandlerService(int currentPage)
    {
    }

    public async Task OnConfigureServiceClickHandler()
    {
      NavigationManager.NavigateTo($"service");
    }

    public ServiceDto SelectedServiceItem { get; set; }
    private IEnumerable<ServiceDto> _selectedItemsService; 
    public IEnumerable<ServiceDto> SelectedItemsService 
    {
      get
      {
        if (_selectedItemsService != null && !Equals(_selectedItemsService, Enumerable.Empty<ServiceDto>()))
          return _selectedItemsService;

        if(ServiceState.Value.ServiceList == null)
          return _selectedItemsService = Enumerable.Empty<ServiceDto>();
        SelectedServiceItem = ServiceState.Value.ServiceList.FirstOrDefault();
        return _selectedItemsService = new List<ServiceDto> { SelectedServiceItem };
      }
      set => _selectedItemsService = value;
    }

    protected void OnSelectService(IEnumerable<ServiceDto> serviceItems)
    {
      SelectedServiceItem = serviceItems.FirstOrDefault();
      SelectedItemsService = new List<ServiceDto> { SelectedServiceItem };
    }

    #endregion

    #region Functions

    // protected void PageChangedHandler(int currentPage)
    // {
    //
    // }

    // public StationDto SelectedStationItem { get; set; }
    // private IEnumerable<StationDto> _selectedItems; 
    //public IEnumerable<StationDto> SelectedItems 
    //{
    //  get
    //  {
    //    if (_selectedItems != null && !Equals(_selectedItems, Enumerable.Empty<StationDto>()))
    //      return _selectedItems;

    //    if(Store.Stations.StationState. Value.StationList == null)
    //      return _selectedItems = Enumerable.Empty<StationDto>();
    //    SelectedStationItem = Store.Stations.StationState.Value.StationList.FirstOrDefault();
    //    return _selectedItems = new List<StationDto> { SelectedStationItem };
    //  }
    //  set => _selectedItems = value;
    //}

    //protected void OnSelect(IEnumerable<StationDto> stationItems)
    //{
    //  //SelectedStationItem = stationItems.FirstOrDefault();
    //  //SelectedItems = new List<StationDto> { SelectedStationItem };
    //}

    #endregion
  }
}