using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using employee.skill.fe.Models.DTOs.Employees;
using employee.skill.fe.Models.DTOs.Skills;
using employee.skill.fe.Store.Employees;
using employee.skill.fe.Store.Employees.Actions.Create;
using employee.skill.fe.Store.Employees.Actions.FetchEmployees;
using employee.skill.fe.Store.Employees.Actions.InitEmployee;
using employee.skill.fe.Store.Employees.Actions.UpdateEmployee;
using employee.skill.fe.Store.Skills;
using employee.skill.fe.Store.Skills.Actions.FetchAll;
using employee.skill.fe.Store.Skills.Actions.Init;
using employee.skill.fe.Store.Statuses;
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
    [Inject] public IState<SkillState> SkillState { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public IConfiguration Configuration { get; set; }

    protected TelerikNotification NotificationSaveOrUpdateComponent { get; set; }

    protected bool ManipulationEmployeeIsVisible { get; set; } = false;
    protected EmployeeDto EmployeeToBeManipulated { get; set; } = new EmployeeDto();
    protected bool ValidSubmit { get; set; } = false;
    public string ActionText { get; set; } = "Add";
    private string Message { get; set; } = string.Empty;


    public void HandleValidEmployeeSaveOrUpdate() {
      this.ValidSubmit = true;
      
      if (this.EmployeeToBeManipulated.Id == Guid.Empty) {
                //Create Appointment
                this.Dispatcher.Dispatch(new CreateEmployeeAction(new EmployeeForCreationDto() {
                    Firstname = this.EmployeeToBeManipulated.Firstname,
                    Lastname = this.EmployeeToBeManipulated.Lastname,
                    Email = this.EmployeeToBeManipulated.Email,
                    HiredDate = this.EmployeeToBeManipulated.HiredDate,
                    NonExistingSkill = this.EmployeeToBeManipulated.NonExistingSkill,
                    ExistingSkillIds = this.EmployeeToBeManipulated.ExistingSkillIds,
                }));
      } else
      {
        this.Dispatcher.Dispatch(new UpdateEmployeeAction(this.EmployeeToBeManipulated.Id, new EmployeeForModificationDto() {
          Id = this.EmployeeToBeManipulated.Id,
          Firstname = this.EmployeeToBeManipulated.Firstname,
          Lastname = this.EmployeeToBeManipulated.Lastname,
          Email = this.EmployeeToBeManipulated.Email,
          HiredDate = this.EmployeeToBeManipulated.HiredDate,
          NonExistingSkill = this.EmployeeToBeManipulated.NonExistingSkill,
          ExistingSkillIds = this.EmployeeToBeManipulated.ExistingSkillIds,
        }));
      }
    }

    public void HandleInValidEmployeeSaveOrUpdate() {
      //Todo : Toastr For Invalid Action
    }
    
    protected void CheckValidityActionForCreation() {
      if (this.ValidSubmit) {
        this.ValidSubmit = false;
        if (this.EmployeeState.Value.CreationStatus == CreationStatus.Success) {
          this.Message = "Creation";
          this.ShowSuccessToastr();
          this.FetchEmployeeList();
          this.SetTimer(3000);
        }

        if (this.EmployeeState.Value.CreationStatus == CreationStatus.Failed) {
          this.Message = "Creation";
          this.ShowErrorToastr();
        }
      }
    }

    private void ShowSuccessToastr() {
      this.NotificationSaveOrUpdateComponent.Show(new NotificationModel() {
        Text = $"Success {this.Message} Employee",
        ThemeColor = "success",
        ShowIcon = true,
        Icon = "file-add",
      });
    }

    private void ShowErrorToastr() {
      this.NotificationSaveOrUpdateComponent.Show(new NotificationModel() {
        Text = $"Unsuccessful {this.Message} Employee",
        ThemeColor = "error",
        ShowIcon = true,
        Icon = "file-error",
      });
    }
    
    protected void CheckValidityActionForModification() {
      if (this.ValidSubmit) {
        this.ValidSubmit = false;
        if (this.EmployeeState.Value.ModificationStatus == ModificationStatus.Success) {
          this.Message = "Modification";
          this.ShowSuccessToastr();
          this.FetchEmployeeList();
          this.StateHasChanged();
        }

        if (this.EmployeeState.Value.ModificationStatus == ModificationStatus.Failed) {
          this.Message = "Modification";
          this.ShowErrorToastr();
        }
        this.Dispatcher.Dispatch(new InitEmployeeAction());
      }
    }

    private void FetchEmployeeList() {
      this.Dispatcher.Dispatch(new FetchEmployeeListAction());
    }
    
    private void FetchSkillList() {
      this.Dispatcher.Dispatch(new FetchSkillListAction());
    }
    
    private Timer _timer;
    public event Action OnElapsed;

    public void SetTimer(double interval) {
      this._timer = new Timer(interval);
      this._timer.Elapsed += this.NotifyTimerElapsed;
      this._timer.Enabled = true;
    }

    private void NotifyTimerElapsed(object sender, ElapsedEventArgs e) {
      this._timer.Enabled = false;
      OnElapsed?.Invoke();
      this.ValidateStatusControlAfterEmployeeCreation();
      this._timer.Dispose();
    }
    
    private void ValidateStatusControlAfterEmployeeCreation() {
      this.EmployeeToBeManipulated = new EmployeeDto();
      if (this.ManipulationEmployeeIsVisible) {
        this.ManipulationEmployeeIsVisible = false;
      }
      this.Dispatcher.Dispatch(new InitEmployeeAction());
      this.InvokeAsync(() => this.StateHasChanged());
    }
    
    protected void OnEmployeeToBeManipulatedHiredClicked() {
      //Todo:
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

      this.EditBtnEnabled = SelectedItems.Count() <= 1;
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
    [Parameter] public bool EditBtnEnabled { get; set; } = true;

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