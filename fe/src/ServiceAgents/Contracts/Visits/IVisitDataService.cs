using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using smarthotel.ui.Models.DTOs.Visits;

namespace smarthotel.ui.ServiceAgents.Contracts.Visits
{
  public interface IVisitDataService
  {
    Task<List<VisitDto>> GetVisitListByCriteria(VisitCriteriaSearchDto criteria);
    Task<List<VisitDto>> GetVisitList();
    Task<VisitDto> GetVisit(Guid actionVisitId);
    Task<int> GetTotalVisitCount();
  }
}