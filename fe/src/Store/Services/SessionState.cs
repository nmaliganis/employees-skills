using System;
using System.Collections.Generic;
using smarthotel.ui.Models.DTOs.Services;

namespace smarthotel.ui.Store.Services
{
  public class ServiceState
  {
    public bool IsLoading { get; private set; }
    public string ErrorMessage { get; private set; }
    public List<ServiceDto> ServiceList { get; private set; }
    public List<ServiceTypeDto> ServiceTypeList { get; set; }
    public List<ServiceFameDto> ServiceFameYearList { get; }
    public List<ServiceFameDto> ServiceFameMonthList { get; }
    public List<ServiceFreqDto> ServiceFreqYearList { get; }
    public List<ServiceFreqDto> ServiceFreqMonthList { get; }
    public ServiceDto Service { get; private set; }
    public ServiceForCreationDto ServiceToBeCreatedPayload { get; private set; }
    public ServiceForModificationDto ServiceToBeUpdatePayload { get; }
    public Guid ServiceId { get; }

    public ServiceState(
      List<ServiceDto> serviceList, 
      List<ServiceTypeDto> serviceTypeList,
      List<ServiceFameDto> serviceFameYearList,
      List<ServiceFameDto> serviceFameMonthList,
      List<ServiceFreqDto> serviceFreqYearList,
      List<ServiceFreqDto> serviceFreqMonthList,
      string errorMessage, 
      bool isLoading,
      ServiceDto service, 
      ServiceForCreationDto serviceToBeCreatedPayload, 
      ServiceForModificationDto serviceToBeUpdatePayload, 
      Guid serviceId
    )
    {
      ServiceList  = serviceList;
      ServiceTypeList = serviceTypeList;
      ServiceFameYearList = serviceFameYearList;
      ServiceFameMonthList = serviceFameMonthList;
      ServiceFreqYearList = serviceFreqYearList;
      ServiceFreqMonthList = serviceFreqMonthList;
      ErrorMessage = errorMessage;
      IsLoading = isLoading;
      Service = service;
      ServiceToBeCreatedPayload = serviceToBeCreatedPayload;
      ServiceToBeUpdatePayload = serviceToBeUpdatePayload;
      ServiceId = serviceId;
    }
  }
}