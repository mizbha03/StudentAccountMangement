namespace StudentAccountMangement.Modals.DTO
{
    public class RequestViewModel
    {
        public int Request_id { get; set; }
        public int User_id { get; set; }
        public string? UserName { get; set; }
        public string ? Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Request_date { get; set; }
        //public int? Proceeded_by { get; set; } 
        //public DateTime? Proceeded_date { get; set; }
        public string? Status { get; set; }
        //public string? PaymentStatus { get; set; }
    }
}
