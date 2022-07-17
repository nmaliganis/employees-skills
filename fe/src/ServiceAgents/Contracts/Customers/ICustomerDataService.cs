using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using smarthotel.ui.Models.DTOs.Customers;

namespace smarthotel.ui.ServiceAgents.Contracts.Customers
{
  public interface ICustomerDataService
  {
    Task<List<CustomerDto>> GetCustomerList(string authorizationToken = null);
    Task<CustomerDto> GetCustomer(Guid actionCustomerId);
    Task<int> GetTotalCustomerCount();

    Task<CustomerDto> CreateCustomer(CustomerForCreationDto customerToBeCreated);
    Task<CustomerDto> UpdateCustomer(Guid customerIdToBeUpdated, CustomerForModificationDto customerToBeUpdated);
    Task<CustomerDto> DeleteCustomer(Guid customerIdToBeDeleted);
  }
}