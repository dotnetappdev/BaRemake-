using MudBlazor;

namespace BaRemake.Web.Services;

/// <summary>
/// Manages the app theme (BA dark blue, BookIt purple, custom) with light/dark mode.
/// Theme preferences are persisted to localStorage via JS interop.
/// </summary>
public class ThemeService
{
    public event Action? OnThemeChanged;

    public bool IsDarkMode { get; private set; }
    public string ThemePreset { get; private set; } = "BA"; // BA, BookIt, Custom

    private static readonly Dictionary<string, (string Primary, string Secondary, string AppBar)> Presets = new()
    {
        ["BA"]     = ("#002157", "#c6a84b", "#002157"),
        ["BookIt"] = ("#6b21a8", "#a855f7", "#4c1d95"),
        ["Green"]  = ("#065f46", "#34d399", "#064e3b"),
        ["Red"]    = ("#991b1b", "#f87171", "#7f1d1d"),
    };

    public MudTheme CurrentMudTheme => BuildTheme();

    public void SetDarkMode(bool dark)
    {
        IsDarkMode = dark;
        OnThemeChanged?.Invoke();
    }

    public void SetPreset(string preset)
    {
        ThemePreset = preset;
        OnThemeChanged?.Invoke();
    }

    public void ApplyCustomColors(string primary, string secondary)
    {
        ThemePreset = "Custom";
        _customPrimary = primary;
        _customSecondary = secondary;
        OnThemeChanged?.Invoke();
    }

    private string _customPrimary = "#002157";
    private string _customSecondary = "#c6a84b";

    private MudTheme BuildTheme()
    {
        var (primary, secondary, appBar) = ThemePreset == "Custom"
            ? (_customPrimary, _customSecondary, _customPrimary)
            : Presets.GetValueOrDefault(ThemePreset, Presets["BA"]);

        return new MudTheme
        {
            PaletteLight = new PaletteLight
            {
                Primary = primary,
                Secondary = secondary,
                AppbarBackground = appBar,
                AppbarText = Colors.Shades.White,
                DrawerBackground = "#ffffff",
                DrawerText = "#1a202c",
                Background = "#f5f7fa",
                Surface = "#ffffff",
                TextPrimary = "#1a202c",
                TextSecondary = "#4a5568",
                ActionDefault = primary,
                ActionDisabled = "#9ea8b5",
                ActionDisabledBackground = "#e8ecf0",
                Divider = "#e8ecf0",
                Success = "#22c55e",
                Warning = "#f59e0b",
                Error = "#ef4444",
                Info = "#3b82f6"
            },
            PaletteDark = new PaletteDark
            {
                Primary = primary,
                Secondary = secondary,
                AppbarBackground = "#1a1a2e",
                AppbarText = Colors.Shades.White,
                DrawerBackground = "#16213e",
                DrawerText = "#e2e8f0",
                Background = "#0f0f1a",
                Surface = "#1a1a2e",
                TextPrimary = "#e2e8f0",
                TextSecondary = "#94a3b8",
                ActionDefault = primary,
                Divider = "#2d3748",
                Success = "#22c55e",
                Warning = "#f59e0b",
                Error = "#ef4444",
                Info = "#3b82f6"
            },
            Typography = new Typography
            {
                Default = new DefaultTypography { FontFamily = new[] { "Inter", "Helvetica Neue", "Arial", "sans-serif" } },
                H1 = new H1Typography { FontSize = "2.5rem", FontWeight = "800" },
                H2 = new H2Typography { FontSize = "2rem", FontWeight = "700" },
                H3 = new H3Typography { FontSize = "1.5rem", FontWeight = "600" }
            },
            LayoutProperties = new LayoutProperties
            {
                AppbarHeight = "72px",
                DrawerWidthLeft = "260px"
            },
            Shape = new Shape
            {
                BorderRadius = "8px"
            }
        };
    }
}
