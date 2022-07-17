using System;

namespace smarthotel.ui.Models.DTOs.Services
{
  public class ServiceForDeletionDto
  {
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public bool DeletionStatus { get; set; }
    public string Message { get; set; }
  }
}