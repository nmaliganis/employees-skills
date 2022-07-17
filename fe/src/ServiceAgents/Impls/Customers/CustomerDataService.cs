using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using smarthotel.ui.Models.DTOs.Customers;
using smarthotel.ui.ServiceAgents.Base;
using smarthotel.ui.ServiceAgents.Contracts.Customers;

namespace smarthotel.ui.ServiceAgents.Impls.Customers
{
  public class CustomerDataService : ICustomerDataService
  {
    private readonly HttpClient _httpClient;
    public IConfiguration Configuration { get; set; }
    public string BaseAddr { get; private set; }
    public string Version { get; private set; }
    public CustomerDataService(IConfiguration configuration, HttpClient httpClient)
    {
      Configuration = configuration;
      _httpClient = httpClient;
      OnCreated();
    }
    private void OnCreated()
    {
      BaseAddr = Configuration["env"] == "prod" ? Configuration["RemoteUrl"] : Configuration["LocalUrl"];
      Version = Configuration["version"];
    }

    public async Task<List<CustomerDto>> GetCustomerList(string authorizationToken = null)
    {
      List<CustomerDto> result = new List<CustomerDto>();

      var client = new RestClient($"{BaseAddr}/api/customers");
      var request = new RestRequest(Method.GET);
      
      request.AddHeader("Content-Type", "application/json");
      request.AddHeader("Authorization", $"bearer {authorizationToken}");

      try
      {
        var response = await client.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
          result = JsonConvert.DeserializeObject<List<CustomerDto>>(response.Content);
        }
        else if (response.StatusCode == HttpStatusCode.BadRequest)
        {
          CustomerErrorModel resultError = JsonConvert.DeserializeObject<CustomerErrorModel>(response.Content);
          throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
        }
      }
      catch (Exception e)
      {
        throw new ServiceHttpRequestException<string>(HttpStatusCode.Conflict, e.Message);
      }
      return result;
    }

    public async Task<CustomerDto> GetCustomer(Guid id)
    {
      CustomerDto result = new CustomerDto();

      var client = new RestClient($"{BaseAddr}/api/Customers/{id}");
      var request = new RestRequest(Method.GET);

      request.AddHeader("Content-Type", "application/json");

      try
      {
        var response = await client.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
          result = JsonConvert.DeserializeObject<CustomerDto>(response.Content);
        }
        else if (response.StatusCode == HttpStatusCode.BadRequest)
        {
          CustomerErrorModel resultError = JsonConvert.DeserializeObject<CustomerErrorModel>(response.Content);
          throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
        }
        else
        {
          CustomerErrorModel resultError = JsonConvert.DeserializeObject<CustomerErrorModel>(response.Content);
          throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
        }
      }
      catch (Exception e)
      {
        throw new ServiceHttpRequestException<string>(HttpStatusCode.Conflict, e.Message);
      }

      return result;
    }

    public async Task<int> GetTotalCustomerCount()
    {
      int result = 0;

      var client = new RestClient($"{BaseAddr}/api/Customers/count");
      var request = new RestRequest(Method.GET);

      request.AddHeader("Content-Type", "application/json");

      try
      {
        var response = await client.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
          result = JsonConvert.DeserializeObject<int>(response.Content);
        }
        else if (response.StatusCode == HttpStatusCode.BadRequest)
        {
          CustomerErrorModel resultError = JsonConvert.DeserializeObject<CustomerErrorModel>(response.Content);
          throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
        }
        else
        {
          CustomerErrorModel resultError = JsonConvert.DeserializeObject<CustomerErrorModel>(response.Content);
          throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
        }
      }
      catch (Exception e)
      {
        throw new ServiceHttpRequestException<string>(HttpStatusCode.Conflict, e.Message);
      }

      return result;
    }

    public async Task<CustomerDto> CreateCustomer(CustomerForCreationDto customerToBeCreated)
    {
      CustomerDto result = new CustomerDto();

      var client = new RestClient($"{BaseAddr}/api/customers");
      var request = new RestRequest("", Method.POST);

      request.AddJsonBody(customerToBeCreated);
      request.AddHeader("Content-Type", "application/json");

      var response = await client.ExecuteAsync(request);
      if(response.IsSuccessful)
      {
        result = JsonConvert.DeserializeObject<CustomerDto>(response.Content);
      }
      else if(response.StatusCode == HttpStatusCode.BadRequest)
      {
        CustomerErrorModel resultError = JsonConvert.DeserializeObject<CustomerErrorModel>(response.Content);
        throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
      }

      return result;
    }

    public async Task<CustomerDto> UpdateCustomer(Guid customerIdToBeUpdated, CustomerForModificationDto customerToBeUpdated)
    {
      CustomerDto result = new CustomerDto();

      var client = new RestClient($"{BaseAddr}/api/customers/{customerIdToBeUpdated}");
      var request = new RestRequest("", Method.PUT);

      request.AddJsonBody(customerToBeUpdated);
      request.AddHeader("Content-Type", "application/json");

      var response = await client.ExecuteAsync(request);
      if(response.IsSuccessful)
      {
        result = JsonConvert.DeserializeObject<CustomerDto>(response.Content);
      }
      else if(response.StatusCode == HttpStatusCode.BadRequest)
      {
        CustomerErrorModel resultError = JsonConvert.DeserializeObject<CustomerErrorModel>(response.Content);
        throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
      }

      return result;
    }

    public async Task<CustomerDto> DeleteCustomer(Guid customerIdToBeDeleted)
    {
      CustomerDto result = new CustomerDto();

      var client = new RestClient($"{BaseAddr}/api/customers/{customerIdToBeDeleted}");
      var request = new RestRequest("", Method.DELETE);

      request.AddHeader("Content-Type", "application/json");

      var response = await client.ExecuteAsync(request);
      if(response.IsSuccessful)
      {
        result = JsonConvert.DeserializeObject<CustomerDto>(response.Content);
      }
      else if(response.StatusCode == HttpStatusCode.BadRequest)
      {
        CustomerErrorModel resultError = JsonConvert.DeserializeObject<CustomerErrorModel>(response.Content);
        throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
      }

      return result;
    }

    public async Task<int> FetchAvailableCustomersCount(string authorizationToken = null)
    {
      int result = 0;

      try
      {
        var client = new RestClient($"{BaseAddr}/api/Customers/count-available");
        var request = new RestRequest(Method.GET);

        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Authorization", $"bearer {authorizationToken}");

        var response = await client.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
          result = JsonConvert.DeserializeObject<int>(response.Content);
        }
        else if (response.StatusCode == HttpStatusCode.BadRequest)
        {
          CustomerErrorModel resultError = JsonConvert.DeserializeObject<CustomerErrorModel>(response.Content);
          throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
        }
      }
      catch (Exception e)
      {
        throw new ServiceHttpRequestException<string>(HttpStatusCode.Conflict, e.Message);
      }

      return result;
    }

    public async Task<int> FetchCustomersInUseCount(string authorizationToken = null)
    {
      int result = 0;

      try
      {
        var client = new RestClient($"{BaseAddr}/api/Customers/count-use");
        var request = new RestRequest(Method.GET);

        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Authorization", $"bearer {authorizationToken}");

        var response = await client.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
          result = JsonConvert.DeserializeObject<int>(response.Content);
        }
        else if (response.StatusCode == HttpStatusCode.BadRequest)
        {
          CustomerErrorModel resultError = JsonConvert.DeserializeObject<CustomerErrorModel>(response.Content);
          throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
        }
      }
      catch (Exception e)
      {
        throw new ServiceHttpRequestException<string>(HttpStatusCode.Conflict, e.Message);
      }
      return result;
    }
  }
}