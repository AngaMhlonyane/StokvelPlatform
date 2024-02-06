using Microsoft.AspNetCore.Mvc;
using StokvelPlatform.Data;
using StokvelPlatform.Models;

namespace StokvelPlatform.Controllers
{
    public class FundManager : Controller
    {
        private readonly ApplicationDbContext _context;

        public FundManager(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FundManager/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FundManager/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(investmentPackage investmentPackage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(investmentPackage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(investmentPackage);
        }
    }
}
