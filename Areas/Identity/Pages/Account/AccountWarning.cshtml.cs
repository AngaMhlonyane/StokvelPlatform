#nullable disable
namespace StokvelPlatform.Areas.Identity.Pages.Account;

public class AccountWarning : PageModel
{
    private readonly UserManager<CustomIdentity> _UserManager;
    private readonly IEmailSender _EmailSender;
    public AccountWarning(UserManager<CustomIdentity> userManager, IEmailSender emailSender)
    {
        _UserManager = userManager;
        _EmailSender = emailSender;
    }

    public string Email { get; set; }

    public async Task<IActionResult> OngetAsync(string userId, string returnUrl = null)
    {
        if (userId == null)
        {
            return RedirectToPage("/Index");
        }

        var usr = await _UserManager.FindByIdAsync(userId);

        if (usr == null)
        {
            return NotFound($"Unable to load user with id {userId}");
        }

        Email = usr.Email;


        return Page();
    }


}
