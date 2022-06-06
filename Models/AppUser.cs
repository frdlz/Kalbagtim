using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProjectAlpha.Models
{
    public class AppUser : IdentityUser
    {
        [Display(Name = "NIP")]
        public string NIP { get; set; }
        [Required]
        [Display(Name = "Nomor HP")]
        public string Phone { get; set; }

        
    }
}
