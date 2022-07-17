using System;
using System.Collections.Generic;
using Fluxor;
using smarthotel.ui.Models.DTOs.Visits;

namespace smarthotel.ui.Store.Visits
{
  public class VisitFeature : Feature<VisitState>
  {
    public override string GetName() => "Visit";

    protected override VisitState GetInitialState() => new VisitState(
      new List<VisitDto>(), 
      "",
      true,
      new VisitDto(), 
      Guid.Empty
    );
  }
}