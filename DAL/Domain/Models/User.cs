namespace DAL.Domain.Models
{
    public class User : EntityBase
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int TrophyId { get; set; }
        public virtual Trophy? Trophy { get; set; }
    }
}
