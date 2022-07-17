using System;

namespace smarthotel.ui.Models.DTOs.Visits
{
  public class VisitCriteriaSearchDto
  {
    public Guid ServiceId { get; set; }
    public bool HasDate { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public bool HasCost { get; set; }
    public string CostSign { get; set; }
    public decimal Cost { get; set; }
  }
}