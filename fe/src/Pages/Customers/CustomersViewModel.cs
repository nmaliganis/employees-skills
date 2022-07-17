using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using smarthotel.ui.Models.DTOs.Customers;
using smarthotel.ui.Store.Customers;
using smarthotel.ui.Store.Customers.Actions.FetchCustomers;
using Telerik.Blazor.Components;

namespace smarthotel.ui.Pages.Customers
{
  public class CustomersViewModel : FluxorComponent
  {
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IState<CustomerState> CustomerState { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public IConfiguration Configuration { get; set; }

    #region Initialization

    protected override Task OnInitializedAsync()
    {
      Dispatcher.Dispatch(new FetchCustomerListAction());

      StateHasChanged();
      return base.OnInitializedAsync();
    }

    protected override void Dispose(bool disposing)
    {
      _selectedItems = null;
      base.Dispose(disposing);
      GC.SuppressFinalize(this);
    }

    public void Dispose()
    {
      GC.SuppressFinalize(this);
    }

    #endregion

    #region Functions

    protected void PageChangedHandler(int currentPage)
    {

    }

    public CustomerDto SelectedCustomerItem { get; set; }
    private IEnumerable<CustomerDto> _selectedItems; 
    public IEnumerable<CustomerDto> SelectedItems 
    {
      get
      {
        if (_selectedItems != null && !Equals(_selectedItems, Enumerable.Empty<CustomerDto>()))
          return _selectedItems;

        if(CustomerState.Value.CustomerList == null)
          return _selectedItems = Enumerable.Empty<CustomerDto>();
        SelectedCustomerItem = CustomerState.Value.CustomerList.FirstOrDefault();
        return _selectedItems = new List<CustomerDto> { SelectedCustomerItem };
      }
      set => _selectedItems = value;
    }

    protected void OnSelect(IEnumerable<CustomerDto> customerItems)
    {
      SelectedCustomerItem = customerItems.FirstOrDefault();
      SelectedItems = new List<CustomerDto> { SelectedCustomerItem };
    }

    #endregion

    #region Commands

    protected void AddCommandFromToolbar(GridCommandEventArgs args)
    {
      NavigationManager.NavigateTo($"customer-details/{Guid.Empty}");
      StateHasChanged();
    }
    protected void EditCommandFromToolbar(GridCommandEventArgs args)
    {
      NavigationManager.NavigateTo($"customer-details/{SelectedItems?.FirstOrDefault()?.Id}");
      StateHasChanged();
    }

    protected void DeleteCommandFromToolbar(GridCommandEventArgs args)
    {
      StateHasChanged();
    }

    #endregion

    #region Buttons

    [Parameter] public bool SaveBtnEnabled { get; set; } = true;

    protected async Task OnAddCustomerClickHandler()
    {
      NavigationManager.NavigateTo($"customer-details/{Guid.Empty}");
      StateHasChanged();
    }
    protected async Task OnEditCustomerClickHandler()
    {
      NavigationManager.NavigateTo($"customer-details/{SelectedItems?.FirstOrDefault()?.Id}");
      StateHasChanged();
    }

    protected async Task OnDeleteCustomerClickHandler()
    {
      NavigationManager.NavigateTo($"customer-details/{Guid.Empty}");
      StateHasChanged();
    }

    #endregion
  }
}