using System.Collections.Generic;
using smarthotel.ui.Models.DTOs.Services;

namespace smarthotel.ui.Store.Services.Actions.FetchFreqServices
{
  public class FetchFreqServiceListSuccessAction
  {
    public List<ServiceFreqDto> ServiceFreqYearList { get; private set; }
    public List<ServiceFreqDto> ServiceFreqMonthList { get; private set; }

    public FetchFreqServiceListSuccessAction(List<ServiceFreqDto> serviceFreqYearList, List<ServiceFreqDto> serviceFreqMonthList)
    {
      ServiceFreqYearList  = serviceFreqYearList;
      ServiceFreqMonthList = serviceFreqMonthList;
    }
  }
}