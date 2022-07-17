using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using employee.skill.fe.Models.DTOs.Employees;
using employee.skill.fe.Store.Employees;
using employee.skill.fe.Store.Employees.Actions.FetchEmployees;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Telerik.Blazor.Components;

namespace employee.skill.fe.Pages.Employees
{
  public class EmployeesViewModel : FluxorComponent
  {
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IState<EmployeeState> EmployeeState { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public IConfiguration Configuration { get; set; }

    protected TelerikNotification NotificationSaveOrUpdateComponent { get; set; }

    protected bool ManipulationEmployeeIsVisible { get; set; } = false;
    protected EmployeeDto EmployeeToBeManipulated { get; set; } = new EmployeeDto();
    protected bool ValidSubmit { get; set; } = false;
    public string ActionText { get; set; } = "Add";

    public void HandleValidEmployeeSaveOrUpdate()
    {
      this.ValidSubmit = true;
    }

    public void HandleInValidEmployeeSaveOrUpdate()
    {
      //Todo : Toastr For Invalid Action
    }
    
    protected void OnCancelClickHandler() {
      if (this.ManipulationEmployeeIsVisible) {
        if (this.ActionText == "Add") {
          this.EmployeeToBeManipulated = new EmployeeDto();
        }

        this.ManipulationEmployeeIsVisible = false;
      }

      this.StateHasChanged();
    }

    #region Initialization

    protected override Task OnInitializedAsync()
    {
      Dispatcher.Dispatch(new FetchEmployeeListAction());

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

    public EmployeeDto SelectedEmployeeItem { get; set; }
    private IEnumerable<EmployeeDto> _selectedItems;

    public IEnumerable<EmployeeDto> SelectedItems
    {
      get
      {
        if (_selectedItems != null && !Equals(_selectedItems, Enumerable.Empty<EmployeeDto>()))
          return _selectedItems;

        if (EmployeeState.Value.EmployeeList == null)
          return _selectedItems = Enumerable.Empty<EmployeeDto>();
        SelectedEmployeeItem = EmployeeState.Value.EmployeeList.FirstOrDefault();
        return _selectedItems = new List<EmployeeDto> { SelectedEmployeeItem };
      }
      set => _selectedItems = value;
    }

    protected void OnSelect(IEnumerable<EmployeeDto> EmployeeItems)
    {
      SelectedEmployeeItem = EmployeeItems.FirstOrDefault();
      SelectedItems = new List<EmployeeDto> { SelectedEmployeeItem };
    }

    #endregion

    #region Commands

    protected void AddCommandFromToolbar(GridCommandEventArgs args)
    {
      NavigationManager.NavigateTo($"Employee-details/{Guid.Empty}");
      StateHasChanged();
    }

    protected void EditCommandFromToolbar(GridCommandEventArgs args)
    {
      NavigationManager.NavigateTo($"Employee-details/{SelectedItems?.FirstOrDefault()?.Id}");
      StateHasChanged();
    }

    protected void DeleteCommandFromToolbar(GridCommandEventArgs args)
    {
      StateHasChanged();
    }

    #endregion

    #region Buttons

    [Parameter] public bool SaveBtnEnabled { get; set; } = true;

    protected async Task OnAddEmployeeClickHandler() {
      this.ActionText = "Add";
      this.ManipulationEmployeeIsVisible = true;
      this.EmployeeToBeManipulated = new EmployeeDto();
    }

    protected async Task OnEditEmployeeClickHandler() {
      this.ActionText = "Edit";
      this.ManipulationEmployeeIsVisible = true;
      EmployeeToBeManipulated = SelectedEmployeeItem;
    }

    protected async Task OnDeleteEmployeeClickHandler() {

    }

    #endregion
  }
}