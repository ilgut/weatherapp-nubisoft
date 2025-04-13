using Microsoft.AspNetCore.Identity;

namespace weatherapp_nubisoft.Data;

public class ApplicationUser : IdentityUser
{
    public List<FavouriteCity> favouriteCities = new ();
}