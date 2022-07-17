using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using employee.skill.fe.Models.DTOs.Employees;
using employee.skill.fe.ServiceAgents.Base;
using employee.skill.fe.ServiceAgents.Contracts.Employees;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace employee.skill.fe.ServiceAgents.Impls.Employees
{
  public class EmployeeDataService : IEmployeeDataService
  {
    public IConfiguration Configuration { get; set; }
    public string BaseAddr { get; private set; }
    public string Version { get; private set; }

    public EmployeeDataService(IConfiguration configuration)
    {
      Configuration = configuration;
      OnCreated();
    }

    private void OnCreated()
    {
      BaseAddr = Configuration["env"] == "prod" ? Configuration["RemoteUrl"] : Configuration["LocalUrl"];
      Version = Configuration["version"];
    }

    public async Task<List<EmployeeDto>> GetEmployeeList(string authorizationToken = null)
    {
      List<EmployeeDto> result = new List<EmployeeDto>();

      var client = new RestClient($"{BaseAddr}/api/{Version}/employees");
      var request = new RestRequest(Method.GET);

      request.AddHeader("Content-Type", "application/json");
      request.AddHeader("Authorization", $"bearer {authorizationToken}");

      try
      {
        var response = await client.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
          result = JsonConvert.DeserializeObject<List<EmployeeDto>>(response.Content);
        }
        else if (response.StatusCode == HttpStatusCode.BadRequest)
        {
          EmployeeErrorModel resultError = JsonConvert.DeserializeObject<EmployeeErrorModel>(response.Content);
          throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
        }
      }
      catch (Exception e)
      {
        throw new ServiceHttpRequestException<string>(HttpStatusCode.Conflict, e.Message);
      }

      return result;
    }

    public async Task<EmployeeDto> GetEmployee(Guid id)
    {
      EmployeeDto result = new EmployeeDto();

      var client = new RestClient($"{BaseAddr}/api/{Version}/employees/{id}");
      var request = new RestRequest(Method.GET);

      request.AddHeader("Content-Type", "application/json");

      try
      {
        var response = await client.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
          result = JsonConvert.DeserializeObject<EmployeeDto>(response.Content);
        }
        else if (response.StatusCode == HttpStatusCode.BadRequest)
        {
          EmployeeErrorModel resultError = JsonConvert.DeserializeObject<EmployeeErrorModel>(response.Content);
          throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
        }
        else
        {
          EmployeeErrorModel resultError = JsonConvert.DeserializeObject<EmployeeErrorModel>(response.Content);
          throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
        }
      }
      catch (Exception e)
      {
        throw new ServiceHttpRequestException<string>(HttpStatusCode.Conflict, e.Message);
      }

      return result;
    }

    public async Task<int> GetTotalEmployeeCount()
    {
      int result = 0;

      var client = new RestClient($"{BaseAddr}/api/{Version}/employees/count");
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
          EmployeeErrorModel resultError = JsonConvert.DeserializeObject<EmployeeErrorModel>(response.Content);
          throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
        }
        else
        {
          EmployeeErrorModel resultError = JsonConvert.DeserializeObject<EmployeeErrorModel>(response.Content);
          throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
        }
      }
      catch (Exception e)
      {
        throw new ServiceHttpRequestException<string>(HttpStatusCode.Conflict, e.Message);
      }

      return result;
    }

    public async Task<EmployeeDto> CreateEmployee(EmployeeForCreationDto employeeToBeCreated)
    {
      EmployeeDto result = new EmployeeDto();

      var client = new RestClient($"{BaseAddr}/api/{Version}/employees");
      var request = new RestRequest("", Method.POST);

      request.AddJsonBody(employeeToBeCreated);
      request.AddHeader("Content-Type", "application/json");

      var response = await client.ExecuteAsync(request);
      if (response.IsSuccessful)
      {
        result = JsonConvert.DeserializeObject<EmployeeDto>(response.Content);
      }
      else if (response.StatusCode == HttpStatusCode.BadRequest)
      {
        EmployeeErrorModel resultError = JsonConvert.DeserializeObject<EmployeeErrorModel>(response.Content);
        throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
      }

      return result;
    }

    public async Task<EmployeeDto> UpdateEmployee(Guid employeeIdToBeUpdated,
      EmployeeForModificationDto employeeToBeUpdated)
    {
      EmployeeDto result = new EmployeeDto();

      var client = new RestClient($"{BaseAddr}/api/{Version}/employees/{employeeIdToBeUpdated}");
      var request = new RestRequest("", Method.PUT);

      request.AddJsonBody(employeeToBeUpdated);
      request.AddHeader("Content-Type", "application/json");

      var response = await client.ExecuteAsync(request);
      if (response.IsSuccessful)
      {
        result = JsonConvert.DeserializeObject<EmployeeDto>(response.Content);
      }
      else if (response.StatusCode == HttpStatusCode.BadRequest)
      {
        EmployeeErrorModel resultError = JsonConvert.DeserializeObject<EmployeeErrorModel>(response.Content);
        throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
      }

      return result;
    }

    public async Task<EmployeeDto> DeleteEmployee(Guid employeeIdToBeDeleted)
    {
      EmployeeDto result = new EmployeeDto();

      var client = new RestClient($"{BaseAddr}/api/{Version}/employees/{employeeIdToBeDeleted}");
      var request = new RestRequest("", Method.DELETE);

      request.AddHeader("Content-Type", "application/json");

      var response = await client.ExecuteAsync(request);
      if (response.IsSuccessful)
      {
        result = JsonConvert.DeserializeObject<EmployeeDto>(response.Content);
      }
      else if (response.StatusCode == HttpStatusCode.BadRequest)
      {
        EmployeeErrorModel resultError = JsonConvert.DeserializeObject<EmployeeErrorModel>(response.Content);
        throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
      }

      return result;
    }
  }
}