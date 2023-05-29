using Microsoft.AspNetCore.Identity;

namespace Project.Models
{
    
    public class ApplcationUser:IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }    
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public string? Image { get; set; }



    }
}
