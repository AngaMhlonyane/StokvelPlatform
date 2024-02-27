using Microsoft.AspNetCore.Mvc;

namespace StokvelPlatform.Controllers;

public class FundManagerController : Controller,IDisposable
{
    private readonly ApplicationDbContext _context;

    public FundManagerController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Create()
    {
        ViewBag.RequestStatus = "";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(InvestementPackage package)
    {
        if(ModelState.IsValid)
        {
            try
            {
                await _context.AddAsync(package);
                await _context.SaveChangesAsync();
                ViewBag.RequestStatus = "Successful";
                return View();
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty,"Unable to submit request, please try again later.");
                return View();
            }
        }
        ModelState.AddModelError(string.Empty, "Unable to submit request, Please try again later!");
        return View();
    }
}
