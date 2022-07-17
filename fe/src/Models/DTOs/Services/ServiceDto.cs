using System;

namespace smarthotel.ui.Models.DTOs.Services
{
  public class ServiceDto
  {
    public Guid Id { get; set; }
    public string Message { get; set; }
    public string ServiceName { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string Type { get; set; }
    public string CreatedDate { get; set; }
    public string Value { get; set; }
  }
}
