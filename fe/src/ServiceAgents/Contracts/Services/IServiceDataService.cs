using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using smarthotel.ui.Models.DTOs.Services;

namespace smarthotel.ui.ServiceAgents.Contracts.Services
{
  public interface IServiceDataService
  {
    Task<List<ServiceFameDto>> GetServiceFameList(bool isYear);
    Task<List<ServiceFreqDto>> GetServiceFreqList(bool isYear);
    Task<List<ServiceTypeDto>> GetServiceTypeList(string authorizationToken = null);
    Task<List<ServiceDto>> GetServiceList(string authorizationToken = null);
    Task<ServiceDto> GetService(int actionServiceId);
    Task<int> GetTotalServiceCount();
    Task<float> GetTotalServiceTotals();

    Task<ServiceDto> CreateService(ServiceForCreationDto serviceToBeCreated);
    Task<ServiceDto> UpdateService(Guid serviceIdToBeUpdated, ServiceForModificationDto serviceToBeUpdated);
    Task<ServiceDto> DeleteService(Guid serviceIdToBeDeleted);
    Task<int> FetchFinishedServiceCount(string authorizationToken = null);
    Task<int> FetchActiveServiceCount(string authorizationToken = null);
  }
}