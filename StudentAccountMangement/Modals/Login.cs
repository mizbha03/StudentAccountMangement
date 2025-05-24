using System.ComponentModel.DataAnnotations;

namespace StudentAccountMangement.Modals
{
    public class Login
    {
        [Key]
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }
    }
}
