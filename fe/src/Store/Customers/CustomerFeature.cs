using System;
using System.Collections.Generic;
using Fluxor;
using smarthotel.ui.Models.DTOs.Customers;

namespace smarthotel.ui.Store.Customers
{
  public class CustomerFeature : Feature<CustomerState>
  {
    public override string GetName() => "Customer";

    protected override CustomerState GetInitialState() => new CustomerState(
      new List<CustomerDto>(), 
      "",
      true,
      new CustomerDto(), 
      new CustomerForCreationDto(), 
      new CustomerForModificationDto(), 
      Guid.Empty
    );
  }
}