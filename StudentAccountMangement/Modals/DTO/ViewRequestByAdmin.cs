namespace StudentAccountMangement.Modals.DTO
{
    public class ViewRequestByAdmin
    {
        public int Request_id { get; set; }
        public string StudentName { get; set; }
        public string Description { get; set; }
        public DateTime Request_date { get; set; }
        public decimal Amount { get; set; }
    }
}
