using System.Globalization;

namespace StudentAccountMangement.Modals.DTO
{
    public class UpdateStudents
    {
        public int User_id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string ProfileImage { get; set; }
    }
}
