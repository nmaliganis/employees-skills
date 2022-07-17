using System;
using System.Collections.Generic;
using Fluxor;
using smarthotel.ui.Models.DTOs.Spaces;

namespace smarthotel.ui.Store.Covids
{
  public class CovidFeature : Feature<CovidState>
  {
    public override string GetName() => "Covid";

    protected override CovidState GetInitialState() => new CovidState(
      new List<SpaceRepoDto>(), 
      new List<SpaceRepoDto>(), 
      "",
      true,
      String.Empty
    );
  }
}