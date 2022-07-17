using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Configuration;
using smarthotel.ui.Models.DTOs.Services;
using smarthotel.ui.Models.DTOs.Visits;
using smarthotel.ui.Store.Services;
using smarthotel.ui.Store.Services.Actions.FetchServices;
using smarthotel.ui.Store.Services.Actions.FetchServiceTypes;
using smarthotel.ui.Store.Visits;
using smarthotel.ui.Store.Visits.Actions.FetchVisits;
using Telerik.Blazor.Components;

namespace smarthotel.ui.Pages.Services
{
  public class ServicesViewModel : FluxorComponent
  {
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IState<ServiceState> ServiceState { get; set; }
    [Inject] public IState<VisitState> VisitState { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public IConfiguration Configuration { get; set; }
    public ServiceFilterDto ServiceFilter { get; set; } = new ServiceFilterDto();

    #region Filter Services

    public async Task OnClickClearHandler(MouseEventArgs args)
    {
      ServiceFilter = new ServiceFilterDto();
    }

    public async Task OnClickSearchHandler(MouseEventArgs args)
    {
      VisitCriteriaSearchDto criteria = new VisitCriteriaSearchDto()
      {
        From = ServiceFilter.From,
        To = ServiceFilter.To,
        HasDate = ServiceFilter.DateSelect,
        ServiceId = SelectedServiceItem.Id,
        Cost = ServiceFilter.CostValue,
        CostSign = ServiceFilter.Cost,
        HasCost = ServiceFilter.CostSelect
      };

      Dispatcher.Dispatch(new FetchVisitByCriteriaListAction(criteria));
    }

    public void FromWasClicked(object theUserInput)
    {
      ServiceFilter.From = ((DateTime) theUserInput);
      ServiceFilter.To = ((DateTime) theUserInput).AddDays(30);
      ServiceFilter.DateSelect = true;
    }

    public void ToWasClicked(object theUserInput)
    {
      ServiceFilter.To = (DateTime) theUserInput;
      ServiceFilter.DateSelect = true;
    }

    public void CostWasClicked(object theUserInput)
    {
      ServiceFilter.CostValue = (decimal) theUserInput;
      ServiceFilter.CostSelect = true;
    }

    public void SearchTypeOnChangeHandler(object searchTypeInput)
    {
      ServiceFilter.Cost = (string) searchTypeInput;
      ServiceFilter.CostSelect = true;
    }

    public DateTime FromDateInputValue { get; set; } = DateTime.Now;

    public TelerikDateInput<DateTime> From;

    public DateTime ToDateInputValue { get; set; } = DateTime.Now;

    public TelerikDateInput<DateTime> To;

    public string SearchTypeCurrentIndex { get; set; } = String.Empty;
    public string[] SearchTypeTypes { get; set; } = {
      "=", ">=", "<="
    };

    public Guid? ServiceTypeCurrentIndex { get; set; }
    public List<ServiceTypeDto> ServiceTypes { get; set; } = new List<ServiceTypeDto>();

    public void ServiceTypeOnChangeHandler(object serviceTypeInput)
    {
    }

    public Telerik.Blazor.Components.TelerikDatePicker<DateTime> TheFromDatePicker;
    public Telerik.Blazor.Components.TelerikDatePicker<DateTime> TheToDatePicker;

    protected async Task HandleValidSubmitForServiceSearch()
    {
    }     
    
    protected async Task HandleInvalidSubmitForServiceSearch()
    {
    }

    public void ServiceSearchOnChangeHandler(object theUserInput)
    {

    }

    #endregion

    #region Initialization

    protected override Task OnInitializedAsync()
    {
      Dispatcher.Dispatch(new FetchServiceListAction());
      Dispatcher.Dispatch(new FetchVisitListAction());
      Dispatcher.Dispatch(new FetchServiceTypeListAction());
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


    #region Visit

    protected void PageVisitChangedHandler(int currentPage)
    {

    }

    public VisitDto SelectedVisitItem { get; set; }
    private IEnumerable<VisitDto> _selectedVisitItems;
    public IEnumerable<VisitDto> SelectedVisitItems
    {
      get
      {
        if (_selectedVisitItems != null && !Equals(_selectedVisitItems, Enumerable.Empty<VisitDto>()))
          return _selectedVisitItems;

        if (VisitState.Value.VisitList == null)
          return _selectedVisitItems = Enumerable.Empty<VisitDto>();
        SelectedVisitItem = VisitState.Value.VisitList.FirstOrDefault();
        return _selectedVisitItems = new List<VisitDto> { SelectedVisitItem };
      }
      set => _selectedVisitItems = value;
    }

    protected void OnVisitSelect(IEnumerable<VisitDto> visitItems)
    {
      SelectedVisitItem = visitItems.FirstOrDefault();
      SelectedVisitItems = new List<VisitDto> { SelectedVisitItem };
    }

    #endregion


    #region Services

    protected void PageChangedHandler(int currentPage)
    {

    }

    public ServiceDto SelectedServiceItem { get; set; }
    private IEnumerable<ServiceDto> _selectedItems;
    public IEnumerable<ServiceDto> SelectedItems
    {
      get
      {
        if (_selectedItems != null && !Equals(_selectedItems, Enumerable.Empty<ServiceDto>()))
          return _selectedItems;

        if (ServiceState.Value.ServiceList == null)
          return _selectedItems = Enumerable.Empty<ServiceDto>();
        SelectedServiceItem = ServiceState.Value.ServiceList.FirstOrDefault();
        return _selectedItems = new List<ServiceDto> { SelectedServiceItem };
      }
      set => _selectedItems = value;
    }

    protected void OnSelect(IEnumerable<ServiceDto> serviceItems)
    {
      SelectedServiceItem = serviceItems.FirstOrDefault();
      SelectedItems = new List<ServiceDto> { SelectedServiceItem };
      ServiceFilter.ServiceTypeName = SelectedServiceItem?.Type;
    }

    #endregion

    #region Commands

    protected void AddCommandFromToolbar(GridCommandEventArgs args)
    {
      NavigationManager.NavigateTo($"Service-details/{Guid.Empty}");
      StateHasChanged();
    }
    protected void EditCommandFromToolbar(GridCommandEventArgs args)
    {
      StateHasChanged();
    }

    protected void DeleteCommandFromToolbar(GridCommandEventArgs args)
    {
      StateHasChanged();
    }

    #endregion

    #region Buttons

    [Parameter] public bool SaveBtnEnabled { get; set; }

    protected async Task OnSaveServiceClickHandler()
    {
    }
    protected async Task OnCancelServiceClickHandler()
    {
    }

    protected async Task OnDeleteServiceClickHandler()
    {
    }

    #endregion
  }
}