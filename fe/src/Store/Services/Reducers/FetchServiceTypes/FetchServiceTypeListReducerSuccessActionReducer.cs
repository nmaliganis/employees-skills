using Fluxor;
using smarthotel.ui.Store.Services.Actions.FetchServiceTypes;

namespace smarthotel.ui.Store.Services.Reducers.FetchServiceTypes
{
  public class FetchServiceTypeListReducerSuccessActionReducer : Reducer<ServiceState, FetchServiceTypeListSuccessAction>
  {
    public override ServiceState Reduce(ServiceState state, FetchServiceTypeListSuccessAction action)
    {
      return new ServiceState(
        state.ServiceList,
        action.ServiceTypeList,
        state.ServiceFameYearList,
        state.ServiceFameMonthList,
        state.ServiceFreqYearList,
        state.ServiceFreqYearList,
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