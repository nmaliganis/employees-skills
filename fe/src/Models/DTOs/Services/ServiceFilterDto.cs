using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace smarthotel.ui.Models.DTOs.Services
{
  public class ServiceFilterDto
  {
    [Required] public Guid ServiceTypeId { get; set; } = Guid.Empty;
    [Required] public string ServiceTypeName { get; set; } = String.Empty;
    [Required] public bool CostSelect { get; set; } = false;
    [Required] public string Cost { get; set; } = "=";
    [Required] public decimal CostValue { get; set; } = 0;
    [Required] public bool DateSelect { get; set; } = false;
    [Required]
    public DateTime From { get; set; } = DateTime.Now;
    [Required]
    public DateTime To { get; set; } = DateTime.Now.AddDays(10);

    [Required]
    public List<ServiceTypeDto> ServiceTypes { get; set; } = new List<ServiceTypeDto>();

    [Required]
    public List<SearchTypeDto> SearchTypes { get; set; } = new List<SearchTypeDto>()
    {
      new SearchTypeDto()
      {
        Name = "=",
      },
      new SearchTypeDto()
      {
        Name = ">=",
      },
      new SearchTypeDto()
      {
        Name = "=<",
      },
    };
  }
}
