#nullable disable
namespace StokvelPlatform.Models;

public class InvestementPackage
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    [Required]
    public string Name { get; set; }

    [Required]
    [DataType(DataType.Currency)]
    public decimal MinimumInvestment { get; set; }

    [Required]
    [DataType(DataType.Currency)]
    public decimal MaximumInvestment { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime LastModified { get; set; } = DateTime.Now;
}
