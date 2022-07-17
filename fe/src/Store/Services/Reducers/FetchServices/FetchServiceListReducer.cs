using Fluxor;
using smarthotel.ui.Store.Services.Actions.FetchServices;

namespace smarthotel.ui.Store.Services.Reducers.FetchServices
{
  public class FetchServiceListReducer : Reducer<ServiceState, FetchServiceListAction>
  {
    public override ServiceState Reduce(ServiceState state, FetchServiceListAction action)
    {
      return new ServiceState(
        state.ServiceList,
        state.ServiceTypeList,
        state.ServiceFameYearList,
        state.ServiceFameMonthList,
        state.ServiceFreqYearList,
        state.ServiceFreqMonthList,
        "",
        state.IsLoading,
        state.Service,
        state.ServiceToBeCreatedPayload,
        state.ServiceToBeUpdatePayload,
        state.ServiceId
        );
    }
  }
}