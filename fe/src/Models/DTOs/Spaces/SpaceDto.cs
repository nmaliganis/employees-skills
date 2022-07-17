using System;
using System.ComponentModel.DataAnnotations;

namespace smarthotel.ui.Models.DTOs.Spaces
{
  public class SpaceDto
  {
    public Guid Id { get; set; }
    public string Message { get; set; }
    public string Name { get; set; }
    public string Number { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Description { get; set; }
    public int Flour { get; set; }
    public int Elevator { get; set; }
    public string Corridor { get; set; }
  }
}