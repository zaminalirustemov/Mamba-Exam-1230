using Mamba_ECommerce.Context;
using Mamba_ECommerce.Helpers;
using Mamba_ECommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace Mamba_ECommerce.Areas.Manage.Controllers;
[Area("Manage")]
[Authorize(Roles = "SuperAdmin,Admin,Editor")]

public class SettingsController : Controller
{
    private readonly MambaDbContext _mambaDbContext;

    public SettingsController(MambaDbContext mambaDbContext)
    {
        _mambaDbContext = mambaDbContext;
    }
    public IActionResult Index(int page=1)
    {
        var query = _mambaDbContext.Settings.AsQueryable();
        var paginatedList = new PaginatedList<Settings>(query.Skip((page - 1) * 4).Take(4).ToList(), query.Count(), 4, page);

        return View(paginatedList);
    }
    //Update-------------------------------------------------------------------------------------------------------------
    public IActionResult Update(int id)
    {
        Settings settings = _mambaDbContext.Settings.FirstOrDefault(x => x.Id == id);
        if (settings == null) return View("Error-404");

        return View(settings);
    }
    [HttpPost]
    public IActionResult Update(Settings newSettings)
    {
        Settings existSettings = _mambaDbContext.Settings.FirstOrDefault(x => x.Id == newSettings.Id);
        if (existSettings == null) return View("Error-404");
        if (!ModelState.IsValid) return View(existSettings);

        existSettings.Value = newSettings.Value;
        _mambaDbContext.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}