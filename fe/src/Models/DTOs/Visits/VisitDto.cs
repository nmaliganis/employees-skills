using System;

namespace smarthotel.ui.Models.DTOs.Visits
{
  public class VisitDto
  {
    public Guid Id { get; set; }
    public DateTime TimeEntrance { get; set; }
    public DateTime TimeExit { get; set; }
    public string SpaceName { get; set; }
    public double Total { get; set; }
    public string CustomerName { get; set; }
  }
}
