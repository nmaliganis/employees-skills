using System.Collections.Generic;
using smarthotel.ui.Models.DTOs.Services;

namespace smarthotel.ui.Store.Services.Actions.FetchServiceTypes
{
  public class FetchServiceTypeListSuccessAction
  {
    public List<ServiceTypeDto> ServiceTypeList { get; private set; }

    public FetchServiceTypeListSuccessAction(List<ServiceTypeDto> serviceTypeList)
    {
      ServiceTypeList  = serviceTypeList;
    }
  }
}