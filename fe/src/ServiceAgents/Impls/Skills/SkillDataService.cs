using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using employee.skill.fe.Models.DTOs.Skills;
using employee.skill.fe.ServiceAgents.Base;
using employee.skill.fe.ServiceAgents.Contracts.Skills;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace employee.skill.fe.ServiceAgents.Impls.Skills
{
  public class SkillDataService : ISkillDataService
  {
    public IConfiguration Configuration { get; set; }
    public string BaseAddr { get; private set; }
    public string Version { get; private set; }

    public SkillDataService(IConfiguration configuration)
    {
      Configuration = configuration;
      OnCreated();
    }

    private void OnCreated()
    {
      BaseAddr = Configuration["env"] == "prod" ? Configuration["RemoteUrl"] : Configuration["LocalUrl"];
      Version = Configuration["version"];
    }

    public async Task<List<SkillDto>> GetSkillList(string authorizationToken = null)
    {
      List<SkillDto> result = new List<SkillDto>();

      var client = new RestClient($"{BaseAddr}/api/{Version}/Skills");
      var request = new RestRequest(Method.GET);

      request.AddHeader("Content-Type", "application/json");
      request.AddHeader("Authorization", $"bearer {authorizationToken}");

      try
      {
        var response = await client.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
          result = JsonConvert.DeserializeObject<List<SkillDto>>(response.Content);
        }
        else if (response.StatusCode == HttpStatusCode.BadRequest)
        {
          SkillErrorModel resultError = JsonConvert.DeserializeObject<SkillErrorModel>(response.Content);
          throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
        }
      }
      catch (Exception e)
      {
        throw new ServiceHttpRequestException<string>(HttpStatusCode.Conflict, e.Message);
      }

      return result;
    }

    public async Task<SkillDto> GetSkill(Guid id)
    {
      SkillDto result = new SkillDto();

      var client = new RestClient($"{BaseAddr}/api/Skills/{id}");
      var request = new RestRequest(Method.GET);

      request.AddHeader("Content-Type", "application/json");

      try
      {
        var response = await client.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
          result = JsonConvert.DeserializeObject<SkillDto>(response.Content);
        }
        else if (response.StatusCode == HttpStatusCode.BadRequest)
        {
          SkillErrorModel resultError = JsonConvert.DeserializeObject<SkillErrorModel>(response.Content);
          throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
        }
        else
        {
          SkillErrorModel resultError = JsonConvert.DeserializeObject<SkillErrorModel>(response.Content);
          throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
        }
      }
      catch (Exception e)
      {
        throw new ServiceHttpRequestException<string>(HttpStatusCode.Conflict, e.Message);
      }

      return result;
    }

    public async Task<int> GetTotalSkillCount()
    {
      int result = 0;

      var client = new RestClient($"{BaseAddr}/api/Skills/count");
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
          SkillErrorModel resultError = JsonConvert.DeserializeObject<SkillErrorModel>(response.Content);
          throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
        }
        else
        {
          SkillErrorModel resultError = JsonConvert.DeserializeObject<SkillErrorModel>(response.Content);
          throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
        }
      }
      catch (Exception e)
      {
        throw new ServiceHttpRequestException<string>(HttpStatusCode.Conflict, e.Message);
      }

      return result;
    }

    public async Task<SkillDto> CreateSkill(SkillForCreationDto SkillToBeCreated)
    {
      SkillDto result = new SkillDto();

      var client = new RestClient($"{BaseAddr}/api/{Version}/skills");
      var request = new RestRequest("", Method.POST);

      request.AddJsonBody(SkillToBeCreated);
      request.AddHeader("Content-Type", "application/json");

      var response = await client.ExecuteAsync(request);
      if (response.IsSuccessful)
      {
        result = JsonConvert.DeserializeObject<SkillDto>(response.Content);
      }
      else if (response.StatusCode == HttpStatusCode.BadRequest)
      {
        SkillErrorModel resultError = JsonConvert.DeserializeObject<SkillErrorModel>(response.Content);
        throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
      }

      return result;
    }

    public async Task<SkillDto> UpdateSkill(Guid SkillIdToBeUpdated,
      SkillForModificationDto SkillToBeUpdated)
    {
      SkillDto result = new SkillDto();

      var client = new RestClient($"{BaseAddr}/api/{Version}/skills/{SkillIdToBeUpdated}");
      var request = new RestRequest("", Method.PUT);

      request.AddJsonBody(SkillToBeUpdated);
      request.AddHeader("Content-Type", "application/json");

      var response = await client.ExecuteAsync(request);
      if (response.IsSuccessful)
      {
        result = JsonConvert.DeserializeObject<SkillDto>(response.Content);
      }
      else if (response.StatusCode == HttpStatusCode.BadRequest)
      {
        SkillErrorModel resultError = JsonConvert.DeserializeObject<SkillErrorModel>(response.Content);
        throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
      }

      return result;
    }

    public async Task<SkillDto> DeleteSkill(Guid SkillIdToBeDeleted)
    {
      SkillDto result = new SkillDto();

      var client = new RestClient($"{BaseAddr}/api/{Version}/skills/{SkillIdToBeDeleted}");
      var request = new RestRequest("", Method.DELETE);

      request.AddHeader("Content-Type", "application/json");

      var response = await client.ExecuteAsync(request);
      if (response.IsSuccessful)
      {
        result = JsonConvert.DeserializeObject<SkillDto>(response.Content);
      }
      else if (response.StatusCode == HttpStatusCode.BadRequest)
      {
        SkillErrorModel resultError = JsonConvert.DeserializeObject<SkillErrorModel>(response.Content);
        throw new ServiceHttpRequestException<string>(response.StatusCode, resultError.errorMessage);
      }

      return result;
    }
  }
}