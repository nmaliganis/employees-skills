using Fluxor;
using smarthotel.ui.Store.Services.Actions.FetchServiceTypes;

namespace smarthotel.ui.Store.Services.Reducers.FetchServiceTypes
{
  public class FetchServiceTypeListReducer : Reducer<ServiceState, FetchServiceTypeListAction>
  {
    public override ServiceState Reduce(ServiceState state, FetchServiceTypeListAction action)
    {
      return new ServiceState(
        state.ServiceList,
        state.ServiceTypeList,
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