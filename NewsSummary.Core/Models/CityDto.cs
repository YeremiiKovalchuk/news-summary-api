using System.ComponentModel.DataAnnotations;

namespace NewsSummary.Core.Models;
public partial class CityDto
{
    [Key]
    public int CityId { get; set; }
    [StringLength(32)]
    public string CityName { get; set; } = null!;
    [StringLength(2)]
    public string Country { get; set; } = null!;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}