using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentAccountMangement.Modals
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Acc_id { get; set; }

        [ForeignKey("User_id")]
        public int User_id { get; set; }
        public decimal Balance { get; set; }
    }
}
