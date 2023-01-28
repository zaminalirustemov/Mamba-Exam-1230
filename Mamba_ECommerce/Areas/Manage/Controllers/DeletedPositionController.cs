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
public class DeletedPositionController : Controller
{
    private readonly MambaDbContext _mambaDbContext;

    public DeletedPositionController(MambaDbContext mambaDbContext)
    {
        _mambaDbContext = mambaDbContext;
    }
    //Read---------------------------------------------------------------------------------------------------------------
    public IActionResult Index(int page=1)
    {
        var query = _mambaDbContext.Positions.Where(d => d.isDeleted == true).AsQueryable();
        var paginatedList = new PaginatedList<Position>(query.Skip((page - 1) * 3).Take(3).ToList(), query.Count(), 3, page);
        return View(paginatedList);
    }
    //Restore---------------------------------------------------------------------------------------------------------
    public IActionResult Restore(int id)
    {
        Position position = _mambaDbContext.Positions.FirstOrDefault(x => x.Id == id);
        if (position == null) return View("Error-404");

        position.isDeleted = false;
        _mambaDbContext.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
    //HardDelete---------------------------------------------------------------------------------------------------------
    public IActionResult HardDelete(int id)
    {
        Position position = _mambaDbContext.Positions.FirstOrDefault(x => x.Id == id);
        if (position == null) return View("Error-404");

        _mambaDbContext.Positions.Remove(position);
        _mambaDbContext.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
}
