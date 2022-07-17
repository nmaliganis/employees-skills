using Fluxor;
using smarthotel.ui.Store.Services.Actions.FetchServices;

namespace smarthotel.ui.Store.Services.Reducers.FetchServices
{
  public class FetchServiceListReducerSuccessActionReducer : Reducer<ServiceState, FetchServiceListSuccessAction>
  {
    public override ServiceState Reduce(ServiceState state, FetchServiceListSuccessAction action)
    {
      return new ServiceState(
        action.ServiceList,
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