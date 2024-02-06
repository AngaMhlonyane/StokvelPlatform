#nullable disable
namespace StokvelPlatform.Areas.Identity.Pages.Account;

public class RegisterModel : PageModel
{
    private readonly SignInManager<CustomIdentity> _signInManager;
    private readonly UserManager<CustomIdentity> _userManager;
    private readonly IUserStore<CustomIdentity> _userStore;
    private readonly IUserEmailStore<CustomIdentity> _emailStore;
    private readonly ILogger<RegisterModel> _logger;
    private readonly IConfiguration _config;
    private readonly IEmailSender _emailSender;

    public RegisterModel(
        UserManager<CustomIdentity> userManager,
        IUserStore<CustomIdentity> userStore,
        SignInManager<CustomIdentity> signInManager,
        ILogger<RegisterModel> logger,
        IEmailSender emailSender,
        IConfiguration config)
    {
        _userManager = userManager;
        _userStore = userStore;
        _emailStore = GetEmailStore();
        _signInManager = signInManager;
        _logger = logger;
        _emailSender = emailSender;
        _config = config;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public string ReturnUrl { get; set; }

    public IList<AuthenticationScheme> ExternalLogins { get; set; }

    public class InputModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Country Of Birth")]
        public string COB { get; set; }

        [Required]
        [MaxLength(13, ErrorMessage = "SA Identity Document Number Must Contain 13 Numbers")]
        [MinLength(13, ErrorMessage = "SA Identity Document Number Must Contain 13 Numbers")]
        [Display(Name = "ID Number")]
        public string IDNo { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Race")]
        public string Race { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }


    public async Task OnGetAsync(string returnUrl = null)
    {
        ReturnUrl = returnUrl;
        ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
    }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
        try
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var exists = _userManager.Users.Where(u => u.IDNo.Contains(Input.IDNo)).FirstOrDefault();

                if (exists != null)
                {
                    ModelState.AddModelError(string.Empty, "ID Number Already Exists!");
                }

                var user = CreateUser();

                user = new CustomIdentity
                {
                    Name = Input.Name,
                    Surname = Input.Surname,
                    COB = Input.COB,
                    IDNo = Input.IDNo,
                    Gender = Input.Gender,
                    Race = Input.Race
                };

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Verify your email address.",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");


                    return RedirectToPage("/Account/Login");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return Page();
        }
    }

    private CustomIdentity CreateUser()
    {
        try
        {
            return Activator.CreateInstance<CustomIdentity>();
        }
        catch
        {
            throw new InvalidOperationException($"Can't create an instance of '{nameof(CustomIdentity)}'. " +
                $"Ensure that '{nameof(CustomIdentity)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
        }
    }

    private IUserEmailStore<CustomIdentity> GetEmailStore()
    {
        if (!_userManager.SupportsUserEmail)
        {
            throw new NotSupportedException("The default UI requires a user store with email support.");
        }
        return (IUserEmailStore<CustomIdentity>)_userStore;
    }
}
