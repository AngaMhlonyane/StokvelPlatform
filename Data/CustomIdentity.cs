#nullable disable

namespace StokvelPlatform.Data;

public class CustomIdentity:IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string COB { get; set; }
    public string IDNo { get; set; }
    public string Gender { get; set; }
    public string Race { get; set; }
}
