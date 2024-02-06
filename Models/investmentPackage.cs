using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StokvelPlatform.Models
{
    public class investmentPackage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "Stokvel Name")]
        [Required]
        public string PackageName { get; set; }

        [Display(Name = "Minimum Joining Amount")]
        [Required]
        public decimal MinimumInvestment { get; set; }
        [Display(Name = "Maximum Joining Amount")]
        [Required]
        public decimal MaximumInvestment { get; set; }

        [Display(Name = "Stokvel Period in Month")]
        [Required]
        public int Duration { get; set; }
        // Other properties as needed
    }


}

