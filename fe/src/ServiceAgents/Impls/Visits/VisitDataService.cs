using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using smarthotel.ui.Models.DTOs.Visits;
using smarthotel.ui.ServiceAgents.Base;
using smarthotel.ui.ServiceAgents.Contracts.Visits;

namespace smarthotel.ui.ServiceAgents.Impls.Visits
{
  public class VisitDataService : IVisitDataService
  {
    private readonly HttpClient _httpClient;
    public IConfiguration Configuration { get; set; }
    public string BaseAddr { get; private set; }
    public string Version { get; private set; }
    public VisitDataService(IConfiguration configuration, HttpClient httpClient)
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

    public async Task<List<VisitDto>> GetVisitListByCriteria(VisitCriteriaSearchDto criteria)
    {
      List<VisitDto> result = new List<VisitDto>();

      var client = new RestClient($"{BaseAddr}/api/visits/search-criteria");
      var request = new RestRequest(Method.POST);
      
      request.AddJsonBody(criteria);
      request.AddHeader("Content-Type", "application/json");

      try
      {
        var response = await client.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
          result = JsonConvert.DeserializeObject<List<VisitDto>>(response.Content);
        }
        else if (response.StatusCode == HttpStatusCode.BadRequest)
        {
          VisitErrorModel resultError = JsonConvert.DeserializeObject<VisitErrorModel>(response.Content);
          throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
        }
      }
      catch (Exception e)
      {
        throw new ServiceHttpRequestException<string>(HttpStatusCode.Conflict, e.Message);
      }
      return result;
    }

    public async Task<List<VisitDto>> GetVisitList()
    {
      List<VisitDto> result = new List<VisitDto>();

      var client = new RestClient($"{BaseAddr}/api/Visits");
      var request = new RestRequest(Method.GET);
      
      request.AddHeader("Content-Type", "application/json");

      try
      {
        var response = await client.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
          result = JsonConvert.DeserializeObject<List<VisitDto>>(response.Content);
        }
        else if (response.StatusCode == HttpStatusCode.BadRequest)
        {
          VisitErrorModel resultError = JsonConvert.DeserializeObject<VisitErrorModel>(response.Content);
          throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
        }
      }
      catch (Exception e)
      {
        throw new ServiceHttpRequestException<string>(HttpStatusCode.Conflict, e.Message);
      }
      return result;
    }

    public async Task<VisitDto> GetVisit(Guid id)
    {
      VisitDto result = new VisitDto();

      var client = new RestClient($"{BaseAddr}/api/{Version}/Visits/{id}");
      var request = new RestRequest(Method.GET);

      request.AddHeader("Content-Type", "application/json");

      try
      {
        var response = await client.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
          result = JsonConvert.DeserializeObject<VisitDto>(response.Content);
        }
        else if (response.StatusCode == HttpStatusCode.BadRequest)
        {
          VisitErrorModel resultError = JsonConvert.DeserializeObject<VisitErrorModel>(response.Content);
          throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
        }
        else
        {
          VisitErrorModel resultError = JsonConvert.DeserializeObject<VisitErrorModel>(response.Content);
          throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
        }
      }
      catch (Exception e)
      {
        throw new ServiceHttpRequestException<string>(HttpStatusCode.Conflict, e.Message);
      }

      return result;
    }

    public async Task<int> GetTotalVisitCount()
    {
      int result = 0;

      var client = new RestClient($"{BaseAddr}/api/Visits/count");
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
          VisitErrorModel resultError = JsonConvert.DeserializeObject<VisitErrorModel>(response.Content);
          throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
        }
        else
        {
          VisitErrorModel resultError = JsonConvert.DeserializeObject<VisitErrorModel>(response.Content);
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