using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentAccountMangement.Modals
{
    public class LoginHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Login_Id { get; set; }             
        public int UserId { get; set; }          
        public DateTime LoginTime { get; set; }  
        public string Role { get; set; }
    }
}
