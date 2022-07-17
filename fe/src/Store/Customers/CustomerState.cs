using System;
using System.Collections.Generic;
using smarthotel.ui.Models.DTOs.Customers;

namespace smarthotel.ui.Store.Customers
{
  public class CustomerState
  {
    public bool IsLoading { get; private set; }
    public string ErrorMessage { get; private set; }
    public List<CustomerDto> CustomerList { get; private set; }
    public CustomerDto Customer { get; private set; }
    public CustomerForCreationDto CustomerToBeCreatedPayload { get; private set; }
    public CustomerForModificationDto CustomerToBeUpdatePayload { get; }
    public Guid CustomerId { get; }

    public CustomerState(
      List<CustomerDto> customerList, 
      string errorMessage, 
      bool isLoading,
      CustomerDto customer, 
      CustomerForCreationDto customerToBeCreatedPayload, 
      CustomerForModificationDto customerToBeUpdatePayload, 
      Guid customerId
    )
    {
      CustomerList  = customerList;
      ErrorMessage = errorMessage;
      IsLoading = isLoading;
      Customer = customer;
      CustomerToBeCreatedPayload = customerToBeCreatedPayload;
      CustomerToBeUpdatePayload = customerToBeUpdatePayload;
      CustomerId = customerId;
    }
  }
}