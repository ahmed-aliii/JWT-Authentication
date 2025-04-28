using Microsoft.AspNetCore.Identity;

namespace WebAPIAssginment.Models
{
    public class ApplicationUser :IdentityUser
    {
        public string? FullName { get; set; }        
    }
}
