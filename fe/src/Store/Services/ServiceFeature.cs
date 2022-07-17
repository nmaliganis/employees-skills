using System;
using System.Collections.Generic;
using Fluxor;
using smarthotel.ui.Models.DTOs.Services;

namespace smarthotel.ui.Store.Services
{
  public class ServiceFeature : Feature<ServiceState>
  {
    public override string GetName() => "Service";

    protected override ServiceState GetInitialState() => new ServiceState(
      new List<ServiceDto>(), 
      new List<ServiceTypeDto>(), 
      new List<ServiceFameDto>(), 
      new List<ServiceFameDto>(), 
      new List<ServiceFreqDto>(), 
      new List<ServiceFreqDto>(), 
      "",
      true,
      new ServiceDto(), 
      new ServiceForCreationDto(), 
      new ServiceForModificationDto(), 
      Guid.Empty
    );
  }
}