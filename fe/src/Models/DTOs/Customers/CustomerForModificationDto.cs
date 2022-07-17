using System;
using System.ComponentModel.DataAnnotations;

namespace smarthotel.ui.Models.DTOs.Customers
{
  public class CustomerForModificationDto
  {
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Enter a Firstname")]
    [StringLength(10, ErrorMessage = "That Firstname is too long")]
    public string Firstname { get; set; }
    [Required(ErrorMessage = "Enter a Lastname")]
    [StringLength(10, ErrorMessage = "That Lastname is too long")]
    public string Lastname { get; set; }
    [Required(ErrorMessage = "Enter a Email")]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string Email { get; set; }
    [Required(ErrorMessage = "Enter a PhoneLong")]
    [Range(6900000000,6999999999, ErrorMessage = "Enter a PhoneLong Number With 10 digits and starts with 69")]
    public long PhoneLong { get; set; }
    [Required(ErrorMessage = "Enter a Nfc")]
    [StringLength(10, ErrorMessage = "That Nfc is too long")]
    public string Nfc { get; set; }
    [Required]
    [Range(typeof(DateTime), "1/1/1900", "1/12/2000",
      ErrorMessage = "Value for {0} must be between {1:dd MMM yyyy} and {2:dd MMM yyyy}")]
    public DateTime DateOfBirth { get; set; }
    public string Phone
    {
      get => PhoneLong.ToString();
      set => PhoneLong = Convert.ToInt64(value);
    }
  }
}