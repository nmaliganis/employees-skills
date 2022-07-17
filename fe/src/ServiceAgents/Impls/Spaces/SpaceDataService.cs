using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using smarthotel.ui.Models.DTOs.Spaces;
using smarthotel.ui.ServiceAgents.Base;
using smarthotel.ui.ServiceAgents.Contracts.Spaces;

namespace smarthotel.ui.ServiceAgents.Impls.Spaces
{
  public class SpaceDataService : ISpaceDataService
  {
    private readonly HttpClient _httpClient;
    public IConfiguration Configuration { get; set; }
    public string BaseAddr { get; private set; }
    public string Version { get; private set; }
    public SpaceDataService(IConfiguration configuration, HttpClient httpClient)
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

    public async Task<List<SpaceFameDto>> GetSpaceFameList(bool isYear)
    {
      List<SpaceFameDto> result = new List<SpaceFameDto>();
      string valuePeriod = "year";

      if(!isYear)
        valuePeriod = "month";

      var client = new RestClient($"{BaseAddr}/api/Spaces/fame-{valuePeriod}");
      var request = new RestRequest(Method.GET);
      
      request.AddHeader("Content-Type", "application/json");

      try
      {
        var response = await client.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
          result = JsonConvert.DeserializeObject<List<SpaceFameDto>>(response.Content);
        }
        else if (response.StatusCode == HttpStatusCode.BadRequest)
        {
          SpaceErrorModel resultError = JsonConvert.DeserializeObject<SpaceErrorModel>(response.Content);
          throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
        }
      }
      catch (Exception e)
      {
        throw new ServiceHttpRequestException<string>(HttpStatusCode.Conflict, e.Message);
      }
      return result;
    }

    public async Task<List<SpaceDto>> GetSpaceList()
    {
      List<SpaceDto> result = new List<SpaceDto>();

      var client = new RestClient($"{BaseAddr}/api/Spaces");
      var request = new RestRequest(Method.GET);
      
      request.AddHeader("Content-Type", "application/json");

      try
      {
        var response = await client.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
          result = JsonConvert.DeserializeObject<List<SpaceDto>>(response.Content);
        }
        else if (response.StatusCode == HttpStatusCode.BadRequest)
        {
          SpaceErrorModel resultError = JsonConvert.DeserializeObject<SpaceErrorModel>(response.Content);
          throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
        }
      }
      catch (Exception e)
      {
        throw new ServiceHttpRequestException<string>(HttpStatusCode.Conflict, e.Message);
      }
      return result;
    }
    //curl -X GET "http://localhost:2100/api/Spaces/covid/customer/BM-1234321" -H "accept: */*"

    public async Task<List<SpaceRepoDto>> GetSpaceCovidList(string nfc)
    {
      List<SpaceRepoDto> result = new List<SpaceRepoDto>();

      var client = new RestClient($"{BaseAddr}/api/spaces/covid/customer/{nfc}");
      var request = new RestRequest(Method.GET);
      
      request.AddHeader("Content-Type", "application/json");

      try
      {
        var response = await client.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
          result = JsonConvert.DeserializeObject<List<SpaceRepoDto>>(response.Content);
        }
        else if (response.StatusCode == HttpStatusCode.BadRequest)
        {
          SpaceErrorModel resultError = JsonConvert.DeserializeObject<SpaceErrorModel>(response.Content);
          throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
        }
      }
      catch (Exception e)
      {
        throw new ServiceHttpRequestException<string>(HttpStatusCode.Conflict, e.Message);
      }
      return result;
    }
    //curl -X GET "http://localhost:2100/api/Spaces/covid-similar/customer/BM-1234321" -H "accept: */*"
    public async Task<List<SpaceRepoDto>> GetSpaceSimilarCovidList(string nfc)
    {
      List<SpaceRepoDto> result = new List<SpaceRepoDto>();

      var client = new RestClient($"{BaseAddr}/api/spaces/covid-similar/customer/{nfc}");
      var request = new RestRequest(Method.GET);
      
      request.AddHeader("Content-Type", "application/json");

      try
      {
        var response = await client.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
          result = JsonConvert.DeserializeObject<List<SpaceRepoDto>>(response.Content);
        }
        else if (response.StatusCode == HttpStatusCode.BadRequest)
        {
          SpaceErrorModel resultError = JsonConvert.DeserializeObject<SpaceErrorModel>(response.Content);
          throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
        }
      }
      catch (Exception e)
      {
        throw new ServiceHttpRequestException<string>(HttpStatusCode.Conflict, e.Message);
      }
      return result;
    }

    public async Task<SpaceDto> GetSpace(Guid id)
    {
      SpaceDto result = new SpaceDto();

      var client = new RestClient($"{BaseAddr}/api/{Version}/Spaces/{id}");
      var request = new RestRequest(Method.GET);

      request.AddHeader("Content-Type", "application/json");

      try
      {
        var response = await client.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
          result = JsonConvert.DeserializeObject<SpaceDto>(response.Content);
        }
        else if (response.StatusCode == HttpStatusCode.BadRequest)
        {
          SpaceErrorModel resultError = JsonConvert.DeserializeObject<SpaceErrorModel>(response.Content);
          throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
        }
        else
        {
          SpaceErrorModel resultError = JsonConvert.DeserializeObject<SpaceErrorModel>(response.Content);
          throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
        }
      }
      catch (Exception e)
      {
        throw new ServiceHttpRequestException<string>(HttpStatusCode.Conflict, e.Message);
      }

      return result;
    }

    public async Task<int> GetTotalSpaceCount()
    {
      int result = 0;

      var client = new RestClient($"{BaseAddr}/api/Spaces/count");
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
          SpaceErrorModel resultError = JsonConvert.DeserializeObject<SpaceErrorModel>(response.Content);
          throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
        }
        else
        {
          SpaceErrorModel resultError = JsonConvert.DeserializeObject<SpaceErrorModel>(response.Content);
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