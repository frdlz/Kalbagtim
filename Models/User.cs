using System.ComponentModel.DataAnnotations;

namespace ProjectAlpha.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }
        [Required]
        [Display(Name = "Nama")]
        public string Name { get; set; }
        [Required]
            
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Display(Name = "NIP")]
        public string NIP { get; set; }
        [Required]
        [Display(Name = "Nomor HP")]
        public string Phone { get; set; }

        
      
    }
}
