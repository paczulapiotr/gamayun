namespace Gamayun.Infrastucture.Entities
{
    public class Presence : Entity
    {
        public Student Student { get; set; }
        public bool WasPresent { get; set; }
    }
}