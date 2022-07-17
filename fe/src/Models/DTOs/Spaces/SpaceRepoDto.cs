using System;
using System.ComponentModel.DataAnnotations;

namespace smarthotel.ui.Models.DTOs.Spaces
{
  public class SpaceRepoDto
  {
    public Guid Id { get; set; }
    public string Customer { get; set; }
    public string Space { get; set; }
    public DateTime TimeEntrance { get; set; }
    public DateTime TimeExit { get; set; }
  }
}