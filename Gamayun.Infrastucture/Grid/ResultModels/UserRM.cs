namespace Gamayun.Infrastucture.Grid.ResultModels
{
    public class UserRM : IGridResultModel
    {
        public int Id { get; set; }
        [PropertyTitle("First Name")]
        public string FirstName { get; set; }
        [PropertyTitle("Last Name")]
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
