using Microsoft.AspNetCore.Identity;

namespace ProjectAlpha.Models
{
    public class AppUser : IdentityUser
    {
        public string NIP { get; set; }
        public string Jabatan { get; set; }
        public string Penempatan { get; set; }
    }
}
