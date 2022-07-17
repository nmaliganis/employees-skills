using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using smarthotel.ui.Models.DTOs.Services;
using smarthotel.ui.ServiceAgents.Base;
using smarthotel.ui.ServiceAgents.Contracts.Services;

namespace smarthotel.ui.ServiceAgents.Impls.Services
{
  public class ServiceDataService : IServiceDataService
  {
    private readonly HttpClient _httpClient;
    public IConfiguration Configuration { get; set; }
    public string BaseAddr { get; private set; }
    public string Version { get; private set; }
    public ServiceDataService(IConfiguration configuration, HttpClient httpClient)
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

    public async Task<List<ServiceFameDto>> GetServiceFameList(bool isYear)
    {
      List<ServiceFameDto> result = new List<ServiceFameDto>();
      string valuePeriod = "year";

      if(!isYear)
        valuePeriod = "month";

      var client = new RestClient($"{BaseAddr}/api/Services/fame-{valuePeriod}");
      var request = new RestRequest(Method.GET);
      
      request.AddHeader("Content-Type", "application/json");

      try
      {
        var response = await client.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
          result = JsonConvert.DeserializeObject<List<ServiceFameDto>>(response.Content);
        }
        else if (response.StatusCode == HttpStatusCode.BadRequest)
        {
          ServiceErrorModel resultError = JsonConvert.DeserializeObject<ServiceErrorModel>(response.Content);
          throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
        }
      }
      catch (Exception e)
      {
        throw new ServiceHttpRequestException<string>(HttpStatusCode.Conflict, e.Message);
      }
      return result;
    }

    public async Task<List<ServiceFreqDto>> GetServiceFreqList(bool isYear)
    {
      List<ServiceFreqDto> result = new List<ServiceFreqDto>();
      string valuePeriod = "year";

      if(!isYear)
        valuePeriod = "month";

      var client = new RestClient($"{BaseAddr}/api/Services/freq-{valuePeriod}");
      var request = new RestRequest(Method.GET);
      
      request.AddHeader("Content-Type", "application/json");

      try
      {
        var response = await client.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
          result = JsonConvert.DeserializeObject<List<ServiceFreqDto>>(response.Content);
        }
        else if (response.StatusCode == HttpStatusCode.BadRequest)
        {
          ServiceErrorModel resultError = JsonConvert.DeserializeObject<ServiceErrorModel>(response.Content);
          throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
        }
      }
      catch (Exception e)
      {
        throw new ServiceHttpRequestException<string>(HttpStatusCode.Conflict, e.Message);
      }
      return result;
    }

    public async Task<List<ServiceTypeDto>> GetServiceTypeList(string authorizationToken = null)
    {
      List<ServiceTypeDto> result = new List<ServiceTypeDto>();

      var client = new RestClient($"{BaseAddr}/api/services/types/list");
      var request = new RestRequest(Method.GET);

      request.AddHeader("Content-Type", "application/json");
      request.AddHeader("Authorization", $"bearer {authorizationToken}");

      var response = await client.ExecuteAsync(request);
      if (response.IsSuccessful)
      {
        result = JsonConvert.DeserializeObject<List<ServiceTypeDto>>(response.Content);
      }
      else if (response.StatusCode == HttpStatusCode.BadRequest)
      {
        ServiceErrorModel resultError = JsonConvert.DeserializeObject<ServiceErrorModel>(response.Content);
        throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
      }
      return result;
    }

    public async Task<List<ServiceDto>> GetServiceList(string authorizationToken = null)
    {
      List<ServiceDto> result = new List<ServiceDto>();

      var client = new RestClient($"{BaseAddr}/api/services/list");
      var request = new RestRequest(Method.GET);

      request.AddHeader("Content-Type", "application/json");
      request.AddHeader("Authorization", $"bearer {authorizationToken}");

      var response = await client.ExecuteAsync(request);
      if (response.IsSuccessful)
      {
        result = JsonConvert.DeserializeObject<List<ServiceDto>>(response.Content);
      }
      else if (response.StatusCode == HttpStatusCode.BadRequest)
      {
        ServiceErrorModel resultError = JsonConvert.DeserializeObject<ServiceErrorModel>(response.Content);
        throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
      }
      return result;
    }

    public async Task<ServiceDto> GetService(int id)
    {
      ServiceDto result = new ServiceDto();

      var client = new RestClient($"{BaseAddr}/api/{Version}/Services/{id}");
      var request = new RestRequest(Method.GET);

      request.AddHeader("Content-Type", "application/json");

      var response = await client.ExecuteAsync(request);
      if (response.IsSuccessful)
      {
        result = JsonConvert.DeserializeObject<ServiceDto>(response.Content);
      }
      else if (response.StatusCode == HttpStatusCode.BadRequest)
      {
        ServiceErrorModel resultError = JsonConvert.DeserializeObject<ServiceErrorModel>(response.Content);
        throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
      }
      else
      {
        ServiceErrorModel resultError = JsonConvert.DeserializeObject<ServiceErrorModel>(response.Content);
        throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
      }
      return result;
    }

    public async Task<int> GetTotalServiceCount()
    {
      int result = 0;

      var client = new RestClient($"{BaseAddr}/api/Services/count");
      var request = new RestRequest(Method.GET);

      request.AddHeader("Content-Type", "application/json");

      var response = await client.ExecuteAsync(request);
      if (response.IsSuccessful)
      {
        result = JsonConvert.DeserializeObject<int>(response.Content);
      }
      else if (response.StatusCode == HttpStatusCode.BadRequest)
      {
        ServiceErrorModel resultError = JsonConvert.DeserializeObject<ServiceErrorModel>(response.Content);
        throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
      }
      else
      {
        ServiceErrorModel resultError = JsonConvert.DeserializeObject<ServiceErrorModel>(response.Content);
        throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
      }
      return result;
    }

    public async Task<float> GetTotalServiceTotals()
    {
      float result = 0;

      var client = new RestClient($"{BaseAddr}/api/services/totals");
      var request = new RestRequest(Method.GET);

      request.AddHeader("Content-Type", "application/json");

      var response = await client.ExecuteAsync(request);
      if (response.IsSuccessful)
      {
        result = JsonConvert.DeserializeObject<float>(response.Content);
      }
      else if (response.StatusCode == HttpStatusCode.BadRequest)
      {
        ServiceErrorModel resultError = JsonConvert.DeserializeObject<ServiceErrorModel>(response.Content);
        throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
      }
      else
      {
        ServiceErrorModel resultError = JsonConvert.DeserializeObject<ServiceErrorModel>(response.Content);
        throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
      }
      return result;
    }

    public Task<ServiceDto> CreateService(ServiceForCreationDto serviceToBeCreated)
    {
      throw new System.NotImplementedException();
    }

    public Task<ServiceDto> UpdateService(Guid serviceIdToBeUpdated, ServiceForModificationDto serviceToBeUpdated)
    {
      throw new System.NotImplementedException();
    }

    public Task<ServiceDto> DeleteService(Guid serviceIdToBeDeleted)
    {
      throw new System.NotImplementedException();
    }

    public async Task<int> FetchFinishedServiceCount(string authorizationToken = null)
    {
      int result = 0;

      var client = new RestClient($"{BaseAddr}/evcharge/api/count-finished");
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
        ServiceErrorModel resultError = JsonConvert.DeserializeObject<ServiceErrorModel>(response.Content);
        throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
      }
      return result;
    }

    public async Task<int> FetchActiveServiceCount(string authorizationToken = null)
    {
      int result = 0;

      var client = new RestClient($"{BaseAddr}/evcharge/api/count-active");
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
        ServiceErrorModel resultError = JsonConvert.DeserializeObject<ServiceErrorModel>(response.Content);
        throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
      }

      return result;
    }
  }
}