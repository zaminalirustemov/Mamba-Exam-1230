using Mamba_ECommerce.Context;
using Mamba_ECommerce.Models;

namespace Mamba_ECommerce.Services;
public class LayoutService
{
    private readonly MambaDbContext _mambaDbContext;

    public LayoutService(MambaDbContext mambaDbContext)
    {
        _mambaDbContext = mambaDbContext;
    }
    public List<Settings> GetSettings()
    {
        return _mambaDbContext.Settings.ToList();
    }
}