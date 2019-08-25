namespace Gamayun.UI.Areas.Admin.Models
{
    public class UserVm
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsObsolete { get; set; }
    }
}
