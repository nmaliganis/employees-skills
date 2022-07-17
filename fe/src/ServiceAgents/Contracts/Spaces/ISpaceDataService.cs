using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using smarthotel.ui.Models.DTOs.Spaces;

namespace smarthotel.ui.ServiceAgents.Contracts.Spaces
{
  public interface ISpaceDataService
  {
    Task<List<SpaceFameDto>> GetSpaceFameList(bool isYear);
    Task<List<SpaceDto>> GetSpaceList();
    Task<List<SpaceRepoDto>> GetSpaceCovidList(string nfc);
    Task<List<SpaceRepoDto>> GetSpaceSimilarCovidList(string nfc);
    Task<SpaceDto> GetSpace(Guid actionSpaceId);
    Task<int> GetTotalSpaceCount();
  }
}