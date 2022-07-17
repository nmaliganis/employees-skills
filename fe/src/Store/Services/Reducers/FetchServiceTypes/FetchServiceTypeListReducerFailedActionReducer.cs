using Fluxor;
using smarthotel.ui.Store.Services.Actions.FetchServiceTypes;

namespace smarthotel.ui.Store.Services.Reducers.FetchServiceTypes
{
  public class FetchServiceTypeListReducerFailedActionReducer : Reducer<ServiceState, FetchServiceTypeListFailedAction>
  {
    public override ServiceState Reduce(ServiceState state, FetchServiceTypeListFailedAction action)
    {
      return new ServiceState(
        state.ServiceList,
        state.ServiceTypeList,
        state.ServiceFameYearList,
        state.ServiceFameMonthList,
        state.ServiceFreqYearList,
        state.ServiceFreqYearList,
        action.ErrorMessage,
        state.IsLoading,
        state.Service,
        state.ServiceToBeCreatedPayload,
        state.ServiceToBeUpdatePayload,
        state.ServiceId
        );
    }
  }
}