using System;

namespace smarthotel.ui.Models.DTOs.Services
{
  public class ServiceTypeDto
  {
    public Guid Id { get; set; } = Guid.Empty;
    public string Message { get; set; }
    public string Type { get; set; } = String.Empty;
  }
}
