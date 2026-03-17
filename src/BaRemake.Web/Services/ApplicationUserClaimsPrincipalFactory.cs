using BaRemake.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace BaRemake.Web.Services;

/// <summary>
/// Adds custom claims (FullName, FirstName, LastName, IsAdmin) to the ClaimsPrincipal
/// when the user signs in, so these are available in Blazor components via AuthenticationState.
/// </summary>
public class ApplicationUserClaimsPrincipalFactory
    : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
{
    public ApplicationUserClaimsPrincipalFactory(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<IdentityOptions> options)
        : base(userManager, roleManager, options)
    {
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);

        identity.AddClaim(new Claim("FullName", user.FullName));
        identity.AddClaim(new Claim("FirstName", user.FirstName));
        identity.AddClaim(new Claim("LastName", user.LastName));

        var isAdmin = await UserManager.IsInRoleAsync(user, "Admin");
        identity.AddClaim(new Claim("IsAdmin", isAdmin.ToString().ToLower()));

        if (!string.IsNullOrEmpty(user.Title))
            identity.AddClaim(new Claim("Title", user.Title));

        return identity;
    }
}
