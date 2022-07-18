using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using employee.skill.fe.Models.DTOs.Skills;
using employee.skill.fe.Store.Skills;
using employee.skill.fe.Store.Skills.Actions.Create;
using employee.skill.fe.Store.Skills.Actions.FetchAll;
using employee.skill.fe.Store.Skills.Actions.Init;
using employee.skill.fe.Store.Skills.Actions.Update;
using employee.skill.fe.Store.Statuses;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Telerik.Blazor.Components;

namespace employee.skill.fe.Pages.Skills
{
  public class SkillsViewModel : FluxorComponent
  {
    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public IState<SkillState> SkillState { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public IConfiguration Configuration { get; set; }

    protected TelerikNotification NotificationSaveOrUpdateComponent { get; set; }
    protected bool ManipulationSkillIsVisible { get; set; } = false;
    protected SkillDto SkillToBeManipulated { get; set; } = new SkillDto();
    protected bool ValidSubmit { get; set; } = false;
    protected string ActionText { get; set; } = "Add";
    private string Message { get; set; } = string.Empty;

    protected void HandleValidSkillSaveOrUpdate() {
      this.ValidSubmit = true;
      
      if (this.SkillToBeManipulated.Id == Guid.Empty)
      {
        this.Dispatcher.Dispatch(new CreateSkillAction(new SkillForCreationDto()
        {
          Name = this.SkillToBeManipulated.Name.ToUpper(),
          Description = this.SkillToBeManipulated.Description
        }));
      } else {
        this.Dispatcher.Dispatch(new UpdateSkillAction(this.SkillToBeManipulated.Id, new SkillForModificationDto() {
          Id = this.SkillToBeManipulated.Id,
          Name = this.SkillToBeManipulated.Name,
          Description = this.SkillToBeManipulated.Description,
        }));
      }
    }

    protected void HandleInValidSkillSaveOrUpdate() {
      //Todo : Toastr For Invalid Action
    }
    
    protected void CheckValidityActionForCreation() {
      if (this.ValidSubmit) {
        this.ValidSubmit = false;
        if (this.SkillState.Value.CreationStatus == CreationStatus.Success) {
          this.Message = "Creation";
          this.ShowSuccessToastr();
          this.FetchSkillList();
          this.SetTimer(3000);
        }

        if (this.SkillState.Value.CreationStatus == CreationStatus.Failed) {
          this.Message = "Creation";
          this.ShowErrorToastr();
        }
      }
    }

    private void ShowSuccessToastr() {
      this.NotificationSaveOrUpdateComponent.Show(new NotificationModel() {
        Text = $"Success {this.Message} Skill",
        ThemeColor = "success",
        ShowIcon = true,
        Icon = "file-add",
      });
    }

    private void ShowErrorToastr() {
      this.NotificationSaveOrUpdateComponent.Show(new NotificationModel() {
        Text = $"Unsuccessful {this.Message} Skill",
        ThemeColor = "error",
        ShowIcon = true,
        Icon = "file-error",
      });
    }
    
    protected void CheckValidityActionForModification() {
      if (this.ValidSubmit) {
        this.ValidSubmit = false;
        if (this.SkillState.Value.ModificationStatus == ModificationStatus.Success) {
          this.Message = "Modification";
          this.ShowSuccessToastr();
          this.FetchSkillList();
          this.StateHasChanged();
        }

        if (this.SkillState.Value.ModificationStatus == ModificationStatus.Failed) {
          this.Message = "Modification";
          this.ShowErrorToastr();
        }
        this.Dispatcher.Dispatch(new InitSkillAction());
      }
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
      this.ValidateStatusControlAfterSkillCreation();
      this._timer.Dispose();
    }
    
    private void ValidateStatusControlAfterSkillCreation() {
      this.SkillToBeManipulated = new SkillDto();
      if (this.ManipulationSkillIsVisible) {
        this.ManipulationSkillIsVisible = false;
      }
      this.Dispatcher.Dispatch(new InitSkillAction());
      this.InvokeAsync(() => this.StateHasChanged());
    }
    
    protected void OnCancelClickHandler() {
      if (this.ManipulationSkillIsVisible) {
        if (this.ActionText == "Add") {
          this.SkillToBeManipulated = new SkillDto();
        }

        this.ManipulationSkillIsVisible = false;
      }

      this.StateHasChanged();
    }
    
    #region Initialization

    protected override Task OnInitializedAsync()
    {
      Dispatcher.Dispatch(new FetchSkillListAction());

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
    public SkillDto SelectedSkillItem { get; set; }
    private IEnumerable<SkillDto> _selectedItems; 
    public IEnumerable<SkillDto> SelectedItems 
    {
      get
      {
        if (_selectedItems != null && !Equals(_selectedItems, Enumerable.Empty<SkillDto>()))
          return _selectedItems;

        if(SkillState.Value.SkillList == null)
          return _selectedItems = Enumerable.Empty<SkillDto>();
        SelectedSkillItem = SkillState.Value.SkillList.FirstOrDefault();
        return _selectedItems = new List<SkillDto> { SelectedSkillItem };
      }
      set => _selectedItems = value;
    }

    protected void OnSelect(IEnumerable<SkillDto> SkillItems)
    {
      SelectedSkillItem = SkillItems.FirstOrDefault();
      SelectedItems = new List<SkillDto> { SelectedSkillItem };
    }

    #endregion

    #region Commands

    protected void AddCommandFromToolbar(GridCommandEventArgs args)
    {
      NavigationManager.NavigateTo($"Skill-details/{Guid.Empty}");
      StateHasChanged();
    }
    protected void EditCommandFromToolbar(GridCommandEventArgs args)
    {
      NavigationManager.NavigateTo($"Skill-details/{SelectedItems?.FirstOrDefault()?.Id}");
      StateHasChanged();
    }

    protected void DeleteCommandFromToolbar(GridCommandEventArgs args)
    {
      StateHasChanged();
    }

    #endregion

    #region Buttons

    [Parameter] public bool SaveBtnEnabled { get; set; } = true;

    protected async Task OnAddSkillClickHandler() {
      this.ActionText = "Add";
      this.ManipulationSkillIsVisible = true;
      this.SkillToBeManipulated = new SkillDto();
    }

    protected async Task OnEditSkillClickHandler() {
      this.ActionText = "Edit";
      this.ManipulationSkillIsVisible = true;
      SkillToBeManipulated = SelectedSkillItem;
    }

    protected async Task OnDeleteSkillClickHandler() {

    }

    #endregion
  }
}