using System;
using System.ComponentModel.DataAnnotations;

namespace smarthotel.ui.Models.DTOs.Services
{
  public class SearchTypeDto
  {
    [Required] public string Name { get; set; } = String.Empty;
  }
}