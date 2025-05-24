using StudentAccountMangement.Enums;
using System.ComponentModel.DataAnnotations;

namespace StudentAccountMangement.Modals.DTO
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
