using BaRemake.Data;
using BaRemake.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BaRemake.Web.Services;

/// <summary>
/// Caches and serves the BrandingSettings DB row (Id=1) app-wide.
/// Call RefreshAsync() after saving changes in admin.
/// </summary>
public class BrandingService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
    private BrandingSettings _current = new();

    public event Action? OnBrandingChanged;

    public BrandingService(IDbContextFactory<ApplicationDbContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public BrandingSettings Current => _current;

    public async Task InitialiseAsync()
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        var row = await db.BrandingSettings.FirstOrDefaultAsync(b => b.Id == 1);
        if (row is not null)
            _current = row;
    }

    public async Task SaveAsync(BrandingSettings updated)
    {
        updated.Id = 1;
        updated.LastUpdated = DateTime.UtcNow;

        await using var db = await _dbFactory.CreateDbContextAsync();
        var existing = await db.BrandingSettings.FirstOrDefaultAsync(b => b.Id == 1);
        if (existing is null)
        {
            db.BrandingSettings.Add(updated);
        }
        else
        {
            db.Entry(existing).CurrentValues.SetValues(updated);
        }
        await db.SaveChangesAsync();

        _current = updated;
        OnBrandingChanged?.Invoke();
    }
}
