using DAL.Domain.Models;

namespace BLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int TrophyId { get; set; }
        public TrophyDTO? Trophy { get; set; }
    }
}
