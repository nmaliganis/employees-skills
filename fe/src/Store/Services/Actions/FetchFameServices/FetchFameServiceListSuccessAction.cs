using System.Collections.Generic;
using smarthotel.ui.Models.DTOs.Services;

namespace smarthotel.ui.Store.Services.Actions.FetchFameServices
{
  public class FetchFameServiceListSuccessAction
  {
    public List<ServiceFameDto> ServiceFameYearList { get; private set; }
    public List<ServiceFameDto> ServiceFameMonthList { get; private set; }

    public FetchFameServiceListSuccessAction(List<ServiceFameDto> serviceFameYearList, List<ServiceFameDto> serviceFameMonthList)
    {
      ServiceFameYearList  = serviceFameYearList;
      ServiceFameMonthList = serviceFameMonthList;
    }
  }
}