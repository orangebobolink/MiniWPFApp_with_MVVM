namespace DAL.Domain.Models
{
    public class User : EntityBase
    {
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public int TrophyId { get; set; }
        public Trophy? Trophy { get; set; }
    }
}
