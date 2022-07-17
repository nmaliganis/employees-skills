using System;
using System.Threading.Tasks;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using smarthotel.ui.Models.DTOs.Customers;
using smarthotel.ui.Store.Customers;
using smarthotel.ui.Store.Customers.Actions.CreateCustomer;
using smarthotel.ui.Store.Customers.Actions.FetchCustomer;
using smarthotel.ui.Store.Customers.Actions.InitCustomer;
using smarthotel.ui.Store.Customers.Actions.UpdateCustomer;
using Telerik.Blazor.Components;

namespace smarthotel.ui.Pages.Customers.CustomersDetails
{
  public class CustomerDetailsViewModel : FluxorComponent
  {
    [Inject] public IDispatcher Dispatcher { get; set; }

    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public IState<CustomerState> CustomerState { get; set; }
    [Parameter] public Guid Id { get; set; }
    public string HeaderLabel { get; set; }
    public bool SaveBtnEnabled { get; set; }
    public TelerikNotification NotificationComponent { get; set; }

    #region Form

    public CustomerDto ValidationCustomerDto { get; set; } = new CustomerDto();

    protected async Task HandleValidSubmitForCustomer()
    {
      if (Id == Guid.Empty)
      {
        Dispatcher.Dispatch(new CreateCustomerAction(new CustomerForCreationDto()
        {
          Firstname = ValidationCustomerDto.Firstname,
          Lastname = ValidationCustomerDto.Lastname,
          Email = ValidationCustomerDto.Email,
          PhoneLong = ValidationCustomerDto.PhoneLong,
          DateOfBirth = ValidationCustomerDto.DateOfBirth,
          Nfc = ValidationCustomerDto.Nfc,
        }));
      }
      else
      {
        Dispatcher.Dispatch(new UpdateCustomerAction(Id, new CustomerForModificationDto()
        {
          Id = Id,
          Firstname = ValidationCustomerDto.Firstname,
          Lastname = ValidationCustomerDto.Lastname,
          Email = ValidationCustomerDto.Email,
          PhoneLong = ValidationCustomerDto.PhoneLong,
          DateOfBirth = ValidationCustomerDto.DateOfBirth,
          Nfc = ValidationCustomerDto.Nfc,
        }));
      }

    }    
    
    protected void CreationError()
    {
      //Toastr
      NotificationComponent.Show(new NotificationModel
      {
        Text = "The password isn't right. Can we help you recover your password?",
        ThemeColor = "error",
        CloseAfter = 0
      });
    }

    protected void LoadSucceeded()
    {
      ValidationCustomerDto.Firstname = CustomerState.Value.Customer.Firstname;
      ValidationCustomerDto.Lastname = CustomerState.Value.Customer.Lastname;
      ValidationCustomerDto.Email = CustomerState.Value.Customer.Email;
      ValidationCustomerDto.Phone = CustomerState.Value.Customer.Phone;
      ValidationCustomerDto.PhoneLong = CustomerState.Value.Customer.PhoneLong;
      ValidationCustomerDto.DateOfBirth = CustomerState.Value.Customer.DateOfBirth;
      ValidationCustomerDto.Nfc = CustomerState.Value.Customer.Nfc;
      Dispatcher.Dispatch(new ClearCustomerAction());
    }

    protected void CreationSucceeded()
    {
      //Toastr
      NotificationComponent.Show(new NotificationModel
      {
        Text = "Customer added to db. Yay!",
        ThemeColor = "success",
        CloseAfter = 0
      });
    }

    protected void ModificationSucceeded()
    {
      //Toastr
      NotificationComponent.Show(new NotificationModel
      {
        Text = "Customer modified to db. Yay!",
        ThemeColor = "success",
        CloseAfter = 0
      });
    }

    protected void DispatcherToInit()
    {
      ValidationCustomerDto = new CustomerDto();
      Dispatcher.Dispatch(new InitCustomerAction());
    }
    
    protected async Task HandleInvalidSubmitForCustomer()
    {
    } 

    #endregion

    #region General

    protected override Task OnInitializedAsync()
    {
      this.SaveBtnEnabled = true;
      if (Id == Guid.Empty)
      {
        InitializeForCreation();
      }
      else
      {
        InitializeForModification();
      }

      StateHasChanged();
      return base.OnInitializedAsync();
    }
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
    }

    #endregion

    #region Common

    private void NavigateBackToCustomers()
    {
      NavigationManager.NavigateTo($"customers");
      StateHasChanged();
    }

    #endregion

    #region Events

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
      return base.OnAfterRenderAsync(firstRender);
    }

    protected async Task OnBackIconClick()
    {
      NavigateBackToCustomers();
    }

    protected async Task OnCancelCustomerClickHandler()
    {
      NavigateBackToCustomers();
    }

    protected async Task OnSaveCustomerClickHandler()
    {
    }

    #endregion

    private void InitializeForCreation()
    {
      this.HeaderLabel = "Create";
    }

    private void InitializeForModification()
    {
      this.HeaderLabel = "Update";
      Dispatcher.Dispatch(new FetchCustomerAction(Id));
    }
  }
}