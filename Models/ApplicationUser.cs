using Microsoft.AspNetCore.Identity;

namespace TequliasResturant.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Order>? Orders { get; set; }
    }
}
