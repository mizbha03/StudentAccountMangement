using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentAccountMangement.Modals
{
    public class Request
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Request_id { get; set; }

        [ForeignKey("User_id")]
        public int User_id { get; set; }
        public decimal? Amount { get; set; }

        [StringLength(255)]
        [Required]
        public string Description { get; set; }
        public DateTime Request_date { get; set; }

        [ForeignKey("User_id")]
        public int? Proceeded_by { get; set; }
        public DateTime? Proceeded_date { get; set; }
        public string? Status { get; set; }
        public string? RequestReason { get; set; }
    }
}
