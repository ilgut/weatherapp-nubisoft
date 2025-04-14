using Microsoft.EntityFrameworkCore;

namespace weatherapp_nubisoft.Data;

public class FavouriteCity
{
    public int Id { get; set; }
    public string CityName { get; set; }
    
    public string AppUserId { get; set; }
    public ApplicationUser AppUser { get; set; }
}