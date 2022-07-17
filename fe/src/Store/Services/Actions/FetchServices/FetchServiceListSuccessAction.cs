using System.Collections.Generic;
using smarthotel.ui.Models.DTOs.Services;

namespace smarthotel.ui.Store.Services.Actions.FetchServices
{
  public class FetchServiceListSuccessAction
  {
    public List<ServiceDto> ServiceList { get; private set; }

    public FetchServiceListSuccessAction(List<ServiceDto> serviceList)
    {
      ServiceList  = serviceList;
    }
  }
}