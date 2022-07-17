using Fluxor;
using smarthotel.ui.Store.Services.Actions.FetchFreqServices;

namespace smarthotel.ui.Store.Services.Reducers.FetchFameServices
{
  public class FetchFreqServiceListReducer : Reducer<ServiceState, FetchFreqServiceListAction>
  {
    public override ServiceState Reduce(ServiceState state, FetchFreqServiceListAction action)
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