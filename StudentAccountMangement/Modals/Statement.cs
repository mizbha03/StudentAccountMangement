using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentAccountMangement.Modals
{
    public class Statement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int T_id { get; set; }

        [ForeignKey("User_id")]
        public int User_id { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }
        public decimal Amount{ get; set; }

        [Required]
        [StringLength(50)]
        public string Mode { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [ForeignKey("User_id")]
        public int Admin_id { get; set; }
        public DateTime TDate { get; set; }
    }
}