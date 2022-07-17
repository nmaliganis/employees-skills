using Fluxor;
using smarthotel.ui.Store.Services.Actions.FetchFameServices;

namespace smarthotel.ui.Store.Services.Reducers.FetchFameServices
{
  public class FetchFameServiceListReducerSuccessActionReducer : Reducer<ServiceState, FetchFameServiceListSuccessAction>
  {
    public override ServiceState Reduce(ServiceState state, FetchFameServiceListSuccessAction action)
    {
      return new ServiceState(
        state.ServiceList,
        state.ServiceTypeList,
        action.ServiceFameYearList,
        action.ServiceFameMonthList,
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