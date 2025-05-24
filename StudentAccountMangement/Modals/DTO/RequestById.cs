namespace StudentAccountMangement.Modals.DTO
{
    public class RequestById
    {
        public int User_id { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Request_date { get; set; }
        public DateTime? Proceeded_date { get; set; }
        public string? Status { get; set; }
        public string? RejectionReason { get; set; }

    }
}
