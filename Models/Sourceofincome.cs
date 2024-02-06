#nullable disable

namespace StokvelPlatform.Models;

public class Sourceofincome
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }


    [Display(Name = "Occupation")]
    [Required]
    public string Occupation { get; set; }

    [Display(Name = "Job Description")]
    [Required]
    public string Jobdescription { get; set; }

    [Display(Name = "Salary Range")]
    [Required]
    public string Salary { get; set; }


    [Display(Name = "Pay-day date")]
    [Required]
    public DateOnly paydate { get; set; }

    [Display(Name = "Company Name")]
    [Required]
    public string CompanyName { get; set; }

    [Display(Name = "Employee Start date")]
    [Required]
    public DateOnly ESDATE { get; set; }


    [Display(Name = "Employee End date")]
    public DateOnly EEDATE { get; set; }



}
