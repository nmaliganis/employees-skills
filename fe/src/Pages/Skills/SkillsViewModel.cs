using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using employee.skill.fe.Models.DTOs.Skills;
using employee.skill.fe.Store.Skills;
using employee.skill.fe.Store.Skills.Actions.FetchAll;
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

    protected async Task OnAddSkillClickHandler()
    {
      NavigationManager.NavigateTo($"Skill-details/{Guid.Empty}");
      StateHasChanged();
    }
    protected async Task OnEditSkillClickHandler()
    {
      NavigationManager.NavigateTo($"Skill-details/{SelectedItems?.FirstOrDefault()?.Id}");
      StateHasChanged();
    }

    protected async Task OnDeleteSkillClickHandler()
    {
      NavigationManager.NavigateTo($"Skill-details/{Guid.Empty}");
      StateHasChanged();
    }

    #endregion
  }
}