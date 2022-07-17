using System;
using System.Collections.Generic;
using Fluxor;
using smarthotel.ui.Models.DTOs.Spaces;

namespace smarthotel.ui.Store.Spaces
{
  public class SpaceFeature : Feature<SpaceState>
  {
    public override string GetName() => "Space";

    protected override SpaceState GetInitialState() => new SpaceState(
      new List<SpaceDto>(), 
      new List<SpaceFameDto>(), 
      new List<SpaceFameDto>(), 
      "",
      true,
      new SpaceDto(), 
      Guid.Empty
    );
  }
}