using System.ComponentModel.DataAnnotations;

namespace TravelApi.Models
{
  public class Review
  {
    public int ReviewId { get; set; }
    
    [StringLength(20)]
    [Required]
    public string UserName { get; set; }
    [Required]
    [StringLength(20)]
    public string LocationCity { get; set; }
    [Required]
    [StringLength(20)]
    public string LocationCountry { get; set; }
    [Required]
    [StringLength(250)]
    public string ReviewText { get; set; }
  }
}